using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Diagnostics;
using System.Web.Services.Description;
using System.Data.Common;
namespace CozyComfort
{
    /// <summary>
    /// Summary description for OrderRequestsWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class OrderRequestsWebService : System.Web.Services.WebService
    {

        // connecting to database
        SqlConnection con = null;

        // method to estbalish connection : NOT A WEB METHOD
        public SqlConnection getConnection()
        {
            try
            {
                // estbalishing connection
                con = new SqlConnection
                    ("data source= // your data source; database=CozyComfort_DB; user  // your user; password= // your password");
                con.Open();
            }

            catch (Exception ex)
            {
                Console.WriteLine("There was an error connecting to the database: " + ex);
            }
            return con;
        }

        // these are services related to ORDERS and SELLERS managing them
        // generating auto order id
        [WebMethod]
        public string autoOrderID()
        {
            string orderID = null;
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Select OrderID from OrderTable", con);
                SqlDataReader dataReader = cmd.ExecuteReader();
                string id = "";
                bool records = dataReader.HasRows;
                if (records)
                {
                    while (dataReader.Read())
                    {
                        id = dataReader[0].ToString();
                    }
                    string idString = id.Substring(3);
                    int CTR = Int32.Parse(idString); // taking the value only
                    if (CTR >= 1 && CTR < 9)
                    {
                        CTR = CTR + 1;
                        orderID = "O00" + CTR;  // adding value
                    }
                    else if (CTR >= 9 && CTR < 99)
                    {
                        CTR = CTR + 1;
                        orderID = "O00" + CTR;
                    }
                    else if (CTR >= 99)
                    {
                        CTR = CTR + 1;
                        orderID = "O00" + CTR;
                    }
                }
                else
                {
                    orderID = "O001";
                }
                dataReader.Close();
            }

            catch (Exception ex)
            {
                orderID = ex.ToString();
            }
            return orderID;
        }

           
        // generating auto request id
        [WebMethod]
        public string autoRequestToDistributorID()
        {
            string requestID = null;
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Select RequestID from DistributorRequestTable", con);
                SqlDataReader dataReader = cmd.ExecuteReader();
                string id = "";
                bool records = dataReader.HasRows;
                if (records)
                {
                    while (dataReader.Read())
                    {
                        id = dataReader[0].ToString();
                    }
                    string idString = id.Substring(4);
                    int CTR = Int32.Parse(idString); // taking the value only
                    if (CTR >= 1 && CTR < 9)
                    {
                        CTR = CTR + 1;
                        requestID = "RD00" + CTR;  // adding value
                    }
                    else if (CTR >= 9 && CTR < 99)
                    {
                        CTR = CTR + 1;
                        requestID = "RD00" + CTR;
                    }
                    else if (CTR >= 99)
                    {
                        CTR = CTR + 1;
                        requestID = "RD00" + CTR;
                    }
                }
                else
                {
                    requestID = "RD001";
                }
                dataReader.Close();
            }

            catch (Exception ex)
            {
                requestID = ex.ToString();
            }
            return requestID;
        }

        // generating auto request id
        [WebMethod]
        public string autoRequestToManufacturerID()
        {
            string requestID = null;
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Select RequestID from ManufacturerRequestTable", con);
                SqlDataReader dataReader = cmd.ExecuteReader();
                string id = "";
                bool records = dataReader.HasRows;
                if (records)
                {
                    while (dataReader.Read())
                    {
                        id = dataReader[0].ToString();
                    }
                    string idString = id.Substring(4);
                    int CTR = Int32.Parse(idString); // taking the value only
                    if (CTR >= 1 && CTR < 9)
                    {
                        CTR = CTR + 1;
                        requestID = "RM00" + CTR;  // adding value
                    }
                    else if (CTR >= 9 && CTR < 99)
                    {
                        CTR = CTR + 1;
                        requestID = "RM00" + CTR;
                    }
                    else if (CTR >= 99)
                    {
                        CTR = CTR + 1;
                        requestID = "RM00" + CTR;
                    }
                }
                else
                {
                    requestID = "RM001";
                }
                dataReader.Close();
            }

            catch (Exception ex)
            {
                requestID = ex.ToString();
            }
            return requestID;
        }



        // making an order
        [WebMethod]
        public string makeOrder(string orderID, string sellerID, string blanketID, int requestQty, decimal totalCost, DateTime dateOfOrder, string defaultStatus)
        {
            string status = defaultStatus;
            string message = "";

            // first, we create an instance of inventory service because we call the method from there
            InventoryWebService inventoryWebService = new InventoryWebService();

            // we will check if the blanket is available
            bool isAvailable = inventoryWebService.isBlanketAvailableForSeller(blanketID, requestQty);
            
            // if there is no stock available, we send a request to the assigned distributor to check their stock
            if (!isAvailable)
            {
                status = "Pending";
                message = "No stock for the specific blanket is available! A request has been sent to the distributor to check for stock.";
                string requestID = autoRequestToDistributorID(); // assigning the request id
                DateTime dateOfRequest = DateTime.Now;
                
                UserWebService userWebService = new UserWebService(); // also calling user web service so we can use the function to get distrubor id
                string distributorID = userWebService.getDistributorIDForSellerID(sellerID);
                
                // sending the request to distributor
                sendRequestToDistributor(requestID, sellerID, distributorID, blanketID, requestQty, "Pending", dateOfRequest);    
            }

            // if available
            // reduce stock lvl
            if (isAvailable)

            {
                bool reducedStockLevel = inventoryWebService.reduceStockForSeller(sellerID, blanketID, requestQty);
                status = "Accepted";
                message = "Your order was placed successfully!";
            }

            // then make the order
            int noRows = 0;
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Insert into OrderTable values('" + orderID + "','" + sellerID + "' , '" + blanketID + "', " + requestQty + ", " + totalCost + " , '" + dateOfOrder + "' , '"+status+"')", con);

                noRows = cmd.ExecuteNonQuery();   // executing the query and how many rows affected
                if (noRows > 0)
                {
                    return message;
                }
                else
                {
                    return "Your order was not placed.";
                }

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
          
        }

        // calculating total cost
        [WebMethod]
        public decimal calculateTotalCost(decimal price, int quantity)
        {
            return (price * quantity);
        }

        // getting all orders for that specfic seller
        [WebMethod]
        public DataSet getAllOrderDetailsForSeller(string sellerID)
        {
            DataSet dataSet = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand
                    ("Select * From OrderTable where SellerID = '" + sellerID+"'", con);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);  // filling the data adapter with the data from table

                dataAdapter.Fill(dataSet, "OrderTable");    // filling dataset with data adapter data 
            }

            catch (Exception ex)
            {
                Console.WriteLine("There was an error: " + ex);
            }
            return dataSet;
        }

        // update and accept a pending order
        [WebMethod]
        public string acceptPendingOrder(string orderID)
        {
            int noRows = 0;
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Update OrderTable Set Status = 'Accepted' where OrderID = '"+orderID+"'", con);
                 noRows = cmd.ExecuteNonQuery();
                if (noRows > 0)
                {
                    return "The order was accepted.";
                }
                else
                {
                    return "The order was not accepted.";
                }
            }
            catch(Exception ex)
            { 
                return "There was an error: " + ex.Message;
            }
        }

        // request related methods
        // methods to send DISTRIBUTOR the requests
        [WebMethod]
        public string sendRequestToDistributor(string requestID, string sellerID, string distributorID, string blanketID, int qty, string status, DateTime requestedTime)
        {
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Insert into DistributorRequestTable (RequestID, SellerID, DistributorID, BlanketID, Quantity, Status, RequestDate) values ('" + requestID + "','" + sellerID + "', '" + distributorID + "','" + blanketID + "', " + qty + ", '" + status + "', '" + requestedTime + "' )", con);
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    return "The request was made to the distributor successfully!";
                }
                else { return "The request could not be made."; }
            }
            catch (Exception ex)
            {
                return "There was an error: " + ex;
            }
        }

        // SELLER viewing the requests they sent
        [WebMethod]
        public DataSet getAllRequestsSentToDistributor(string sellerID, string distributorID)
        {
            DataSet dataSet = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Select r.RequestID, r.BlanketID, r.Quantity,r.Status, r.RequestDate, r.AdditionalInfo, b.BlanketName " +
                    "From DistributorRequestTable r " +
                    "INNER JOIN BlanketTable b ON r.BlanketID = b.BlanketID where DistributorID = '" + distributorID + "' and SellerID = '"+sellerID+"'", con);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);  // filling the data adapter with the data from table

                dataAdapter.Fill(dataSet, "DistributorRequestTable.");    // filling dataset with data adapter data 
            }

            catch (Exception ex)
            {
                Console.WriteLine("There was an error: " + ex);
            }
            return dataSet;
        }


        // DISTRIBUTOR viewing the requests
        [WebMethod]
        public DataSet getAllRequestsForDistributor(string distributorID)
        {
            DataSet dataSet = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Select r.RequestID, r.SellerID, r.BlanketID, r.Quantity,r.Status, r.RequestDate, r.AdditionalInfo, b.BlanketName, u.Address, u.Email, u.ContactNumber " +
                    "From DistributorRequestTable r " +
                    "Inner Join BlanketTable b ON r.BlanketID = b.BlanketID " +
                    "Inner Join UserTable u ON r.SellerID = u.UserID " +
                    "where DistributorID = '" + distributorID + "'", con);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);  // filling the data adapter with the data from table

                dataAdapter.Fill(dataSet, "DistributorRequestTable");    // filling dataset with data adapter data 
            }

            catch (Exception ex)
            {
                Console.WriteLine("There was an error: " + ex);
            }
            return dataSet;
        }

        // updating status of request for DISTRIBUTOR
        [WebMethod]
        public string updateStatusForDistributorRequest(string requestID, string newStatus)
        {
            getConnection();
            SqlCommand cmd = new SqlCommand("Update DistributorRequestTable SET Status = '"+newStatus+"' WHERE RequestID = '" + requestID + "'", con);

            int noRows = cmd.ExecuteNonQuery();   // executing the query and how many rows affected
            if (noRows > 0)
            {
                Console.WriteLine("Status was updated in the table successfully.");
            }
            else
            {
                Console.WriteLine("Status was not updated in the table.");
            }
            return noRows.ToString();
        }


        // these are methods related to sending requests to MANUFACTURERS
        // sending the request
        [WebMethod]
        public string sendRequestToManufacturer(string requestID, string distributorID, string blanketID, int qty, string status, DateTime requestedTime)
        {
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Insert into ManufacturerRequestTable (RequestID, DistributorID, BlanketID, Quantity, Status, RequestDate) values ('" + requestID + "', '" + distributorID + "','" + blanketID + "', " + qty + ", '" + status + "', '" + requestedTime + "' )", con);
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    return "The request was made to the manufacturer successfully!";
                }
                else { return "The request could not be made."; }
            }
            catch (Exception ex)
            {
                return "There was an error: " + ex;
            }
        }

        // viewing the request
        [WebMethod]
        public DataSet getAllRequestsForManufacturer()
        {
            DataSet dataSet = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Select m.RequestID, m.DistributorID, m.BlanketID, m.Quantity,m.Status, m.RequestDate,  b.BlanketName, u.UserID, u.Address, u.Email,u.ContactNumber " +
                    "From ManufacturerRequestTable m " +
                    "Inner Join UserTable u ON m.DistributorID = u.UserID " +
                    "INNER JOIN BlanketTable b ON m.BlanketID = b.BlanketID", con);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);  // filling the data adapter with the data from table

                dataAdapter.Fill(dataSet, "ManufacturerRequestTable");    // filling dataset with data adapter data 
            }

            catch (Exception ex)
            {
                Console.WriteLine("There was an error: " + ex);
            }
            return dataSet;
        }


        // viewing the request
        [WebMethod]
        public DataSet getAllRequestsSentToManufacturer(string distributorID)
        {
            DataSet dataSet = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Select m.RequestID, m.BlanketID, m.Quantity,m.Status, m.RequestDate, m.AdditionalInfo, b.BlanketName " +
                    "From ManufacturerRequestTable m " +
                    "INNER JOIN BlanketTable b ON m.BlanketID = b.BlanketID where DistributorID = '"+distributorID+"'", con);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);  // filling the data adapter with the data from table

                dataAdapter.Fill(dataSet, "ManufacturerRequestTable");    // filling dataset with data adapter data 
            }

            catch (Exception ex)
            {
                Console.WriteLine("There was an error: " + ex);
            }
            return dataSet;
        }

        // updating the status
        [WebMethod]
        public string updateStatusForManufacturerRequest(string requestID, string newStatus)
        {
            getConnection();
            SqlCommand cmd = new SqlCommand("Update ManufacturerRequestTable SET Status = '" + newStatus + "' WHERE RequestID = '" + requestID + "'", con);

            int noRows = cmd.ExecuteNonQuery();   // executing the query and how many rows affected
            if (noRows > 0)
            {
                Console.WriteLine("Status was updated in the table successfully.");
            }
            else
            {
                Console.WriteLine("Status was not updated in the table.");
            }
            return noRows.ToString();
        }

        // now methods related to sending ADDITIONAL INFORMATION
        [WebMethod]
        public string sendInformationToDistributor(string requestID, string information)
        {
            getConnection();
            SqlCommand cmd = new SqlCommand("Update ManufacturerRequestTable Set AdditionalInfo = '"+information+"' where RequestID = '"+requestID+"'", con);
            int noRows = cmd.ExecuteNonQuery();   // executing the query and how many rows affected
            if (noRows > 0)
            {
                return "Information was sent successfully.";
            }
            else
            {
                return "Information was not sent.";
            }          
        }

        // now methods related to sending ADDITIONAL INFORMATION
        [WebMethod]
        public string sendInformationToSeller(string requestID, string information)
        {
            getConnection();
            SqlCommand cmd = new SqlCommand("Update DistributorRequestTable Set AdditionalInfo = '" + information + "' where RequestID = '" + requestID + "'", con);
            int noRows = cmd.ExecuteNonQuery();   // executing the query and how many rows affected
            if (noRows > 0)
            {
                return "Information was sent successfully.";
            }
            else
            {
                return "Information was not sent.";
            }
        }   

    }

}
