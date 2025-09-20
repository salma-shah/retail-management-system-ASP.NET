using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Diagnostics;
namespace CozyComfort
{
    /// <summary>
    /// Summary description for InventoryWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class InventoryWebService : System.Web.Services.WebService
    {
        // connecting to database
        SqlConnection con = null;

        // method to estbalosh connection : NOT A WEB METHOD
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

        // MANUFACTURER role functions:

        // setting production capacity levels and production capacity for blanket model
        [WebMethod]
        public String setStock(string blanketID, int quantity, int capacity, int leadTimeDays, DateTime lastUpdated)
        {
            int noRows = 0;
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Insert into ManufacturerStockTable values('" + blanketID + "', " + quantity + ", " + capacity + ", "+leadTimeDays+", '"+lastUpdated+"')", con);

                noRows = cmd.ExecuteNonQuery();   // executing the query and how many rows affected
                if (noRows > 0)
                {
                    Console.WriteLine("Data was inserted into the table successfully.");
                }
                else
                {
                    Console.WriteLine("Data was not inserted into the table.");
                }

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return noRows.ToString();
        }

        // adding stock to qty for specific blanket
        [WebMethod]
        public bool addStockForManufacturer(string blanketID, int newStock)
        {
            bool records = false;
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Update ManufacturerStockTable set Quantity= Quantity +" + newStock + "where BlanketID ='" + blanketID + "'", con);
                int rowsAffected = cmd.ExecuteNonQuery();
                records = rowsAffected > 0;
            }
            catch (Exception exc)
            {
                Console.WriteLine("The stock level was not updated because: " + exc);
            }
            return records;
        }

        // removing stock to qty for specific blanket
        [WebMethod]
        public bool reduceStockForManufacturer(string blanketID, int amount)
        {
            bool records = false;
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Update ManufacturerStockTable set Quantity= Quantity -" + amount + "where BlanketID ='" + blanketID + "'", con);
                int rowsAffected = cmd.ExecuteNonQuery();
                records = rowsAffected > 0;
            }
            catch (Exception exc)
            {
                Console.WriteLine("The stock level was not updated because: " + exc);
            }
            return records;
        }

        // method to check stock for blanket
        [WebMethod]
        public bool isBlanketAvailableForManufacturer(string blanketID, int requestQuantity)
        {
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand
                    ("Select Quantity from ManufacturerStockTable where BlanketID = '" + blanketID + "'", con);
                object result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int stock))
                {

                    return stock >= requestQuantity;   // return it
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error fetching the data: " + ex);
            }
            return false;
        }

        // get production capacity and lead times information on a blanket
        [WebMethod]
        public DataSet getProductionCapacityAndLeadTime(string blanketID)
        {
            DataSet dataSet = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand
                    ("Select ProductionCapacity, LeadTimeInDays from ManufacturerStockTable Where BlanketID = '"+blanketID+"'", con);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);  // filling the data adapter with the data from table

                dataAdapter.Fill(dataSet, "ManufacturerStockTable");    // filling dataset with data adapter data 
            }

            catch (Exception ex)
            {
                Console.WriteLine("There was an error in finding the data: " + ex);
            }
            return dataSet;
        }

        // now functions for DISTRIBUTOR
        // method to check stock for blanket
        [WebMethod]
        public bool isBlanketAvailableForDistributor(string blanketID, int requestQuantity)
        {
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand
                    ("Select Quantity from DistributorStockTable where BlanketID = '" + blanketID + "'", con);
                object result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int stock))
                {
                    // once we get quantity for the blanket
                    return stock >= requestQuantity;   // return it
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error fetching the data: " + ex);
            }
            return false;
        }

        // setting initila stock level
        [WebMethod]
        public String setStockDistributor(string distributorID, string blanketID, int amount, DateTime lastUpdated)
        {
            int noRows = 0;
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Insert into DistributorStockTable values('" + distributorID + "', '" + blanketID + "', " + amount + ", '" + lastUpdated + "')", con);

                noRows = cmd.ExecuteNonQuery();   // executing the query and how many rows affected
                if (noRows > 0)
                {
                    Console.WriteLine("Data was inserted into the table successfully.");
                }
                else
                {
                    Console.WriteLine("Data was not inserted into the table.");
                }

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return noRows.ToString();
        }

        // adding stock to qty for specific blanket
        [WebMethod]
        public bool addStockForDistributor(string distributorID, string blanketID, int amount)
        {
            bool records = false;
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Update DistributorStockTable set Quantity= Quantity + " + amount + "where BlanketID ='" + blanketID + "' and DistributorID = '"+distributorID+"'", con);
                int rowsAffected = cmd.ExecuteNonQuery();
                records = rowsAffected > 0;
            }
            catch (Exception exc)
            {
                Console.WriteLine("The stock level was not updated because: " + exc);
            }
            return records;
        }

        // removing stock to qty for specific blanket
        [WebMethod]
        public bool reduceStockForDistributor(string distributorID, string blanketID, int amount)
        {
            bool records = false;
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Update DistributorStockTable set Quantity= Quantity -" + amount + "where BlanketID ='" + blanketID + "' and DistributorID = '"+distributorID+"'", con);
                int rowsAffected = cmd.ExecuteNonQuery();
                records = rowsAffected > 0;
            }
            catch (Exception exc)
            {
                Console.WriteLine("The stock level was not updated because: " + exc);
            }
            return records;
        }


        // now SELLER functions

        // method to check stock for blanket
        [WebMethod]
        public bool isBlanketAvailableForSeller(string blanketID, int requestQuantity)
        {
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand
                    ("Select Quantity from SellerStockTable where BlanketID = '" + blanketID + "'", con);
                object result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int stock))
                {
                    // once we get quantity for the blanket, we convert the result to a stock variable

                    return stock >= requestQuantity;   // return it
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error fetching the data: " + ex);
            }
            return false;
        }

        // adding stock to qty for specific blanket
        [WebMethod]
        public bool addStockForSeller(string sellerID, string blanketID, int amount)
        {
            bool records = false;
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Update SellerStockTable Set Quantity= Quantity +" + amount + "where BlanketID ='" + blanketID + "' and SellerID = '"+sellerID+"' ", con);
                int rowsAffected = cmd.ExecuteNonQuery();
                records = rowsAffected > 0;
            }
            catch (Exception exc)
            {
                Console.WriteLine("The stock level was not updated because: " + exc);
            }
            return records;
        }

        // removing stock to qty for specific blanket
        [WebMethod]
        public bool reduceStockForSeller(string sellerID, string blanketID, int amount)
        {
            bool records = false;
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Update SellerStockTable set Quantity= Quantity -" + amount + "where BlanketID ='" + blanketID + "' and SellerID = '"+sellerID+"'", con);
                int rowsAffected = cmd.ExecuteNonQuery();
                records = rowsAffected > 0;
            }
            catch (Exception exc)
            {
                Console.WriteLine("The stock level was not updated because: " + exc);
            }
            return records;
        }

        // setting initila stock level
        [WebMethod]
        public String setStockSeller(string sellerID, string blanketID, int amount, DateTime lastUpdated)
        {
            int noRows = 0;
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Insert into SellerStockTable values('" + sellerID + "', '" + blanketID + "', " + amount + ", '" + lastUpdated + "')", con);

                noRows = cmd.ExecuteNonQuery();   // executing the query and how many rows affected
                if (noRows > 0)
                {
                    Console.WriteLine("Data was inserted into the table successfully.");
                }
                else
                {
                    Console.WriteLine("Data was not inserted into the table.");
                }

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return noRows.ToString();
        }

    }
}
