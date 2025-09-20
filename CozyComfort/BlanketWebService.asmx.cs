using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Drawing;
using System.Data;
using System.Collections;
namespace CozyComfort
{
    /// <summary>
    /// Summary description for BlanketWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class BlanketWebService : System.Web.Services.WebService
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

        // material realted methods
        // generating auto id for materials
        [WebMethod]
        public String autoMaterialID()
        {
            string materialID = "M001";

            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Select MaterialID from MaterialTable order by MaterialID Desc", con);
                SqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.Read())
                {
                    string latestID = dataReader["MaterialID"].ToString();
                  
                    if (latestID.Length >= 4 && int.TryParse(latestID.Substring(1), out int numPart))
                    {
                        numPart++;
                        materialID = "M" + numPart.ToString("D3"); // formats with leading zeros
                    }
                }
                dataReader.Close();
            }

            catch (Exception ex)
            {
                materialID = ex.ToString();
            }
            return materialID;
        }


        // inserting a material
        [WebMethod]
        public String insertMaterial(string materialID, string materialName)
        {
            int noRows = 0;
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("insert into MaterialTable values('" + materialID + "','" + materialName + "')", con);

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

       
        // category related methods
        // generating auto id
        [WebMethod]
        public String autoCategoryID()
        {
            string categoryID = "C001";

            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Select CategoryID from CategoryTable order by CategoryID Desc", con);  // getting the id from table in descending order
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    string latestID = dataReader["CategoryID"].ToString();

                    if (latestID.Length >= 4 && int.TryParse(latestID.Substring(1), out int numPart))
                    {
                        numPart++;
                        categoryID = "C" + numPart.ToString("D3"); // formating with 3 0's in front
                    }
                }
                dataReader.Close();              
            }

            catch (Exception ex)
            {
                categoryID = ex.ToString();
            }
            return categoryID;
        }


        // inserting a category ; model type
        [WebMethod]
        public string insertCategory(string categoryID, string categoryName)
        {
            int noRows = 0;
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("insert into CategoryTable values('" + categoryID + "','" + categoryName + "')", con);

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


        // blanket realted methods
        // auto generating blanket id
        [WebMethod]
        public String autoBlanketID()
        {
            string blanketID = "B001";

            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Select BlanketID from BlanketTable order by BlanketID desc", con);  // getting the ids from table
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    string latestID = dataReader["BlanketID"].ToString();

                    if (latestID.Length >= 4 && int.TryParse(latestID.Substring(1), out int numPart))
                    {
                        numPart++;
                        blanketID = "B" + numPart.ToString("D3"); // formating with 3 0's in front
                    }
                }
                dataReader.Close();
            }

            catch (Exception ex)
            {
                blanketID = ex.ToString();
            }
            return blanketID;
        }

        // insert blanket
        [WebMethod]
        public String insertBlanket(string blanketID, string blanketName, string categoryID, string materialID, string size, decimal price, string colour, string description)
        {
            int noRows = 0;
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("insert into BlanketTable values('" + blanketID + "','" + blanketName + "', '"+ categoryID +"', '"+materialID+"', '"+size+"' , "+price+", '"+colour+"' , '"+description+"')", con);

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

        // getting category and material names and blanket
        // category
        [WebMethod]
        public DataSet getCategoryName()
        {
            DataSet dataSet = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand
                    ("Select CategoryName from CategoryTable", con);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);  // filling the data adapter with the data from table

                dataAdapter.Fill(dataSet, "CategoryTable");    // filling dataset with data adapter data 
            }

            catch (Exception ex)
            {
                Console.WriteLine("There was an error in selecting category name: " + ex);
            }
            return dataSet;

        }

        [WebMethod]
        public string getCategoryID(string categoryName)
        {
            string categoryID = "";
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Select CategoryID from CategoryTable where CategoryName= '" + categoryName + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();

                Boolean records = dr.HasRows;
                if (records)
                {
                    while (dr.Read())
                    {
                        categoryID = dr[0].ToString();
                    }
                }
                dr.Close();
            }
            catch (Exception exc)
            {
                Console.Write("There was an error: " + exc);
            }
            return categoryID;
        }

        [WebMethod]
        public DataSet getAllCategories()
        {
            DataSet dataSet = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand
                    ("Select * from CategoryTable", con);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);  // filling the data adapter with the data from table

                dataAdapter.Fill(dataSet, "CategoryTable");    // filling dataset with data adapter data 
            }

            catch (Exception ex)
            {
                Console.WriteLine("There was an error in finding the categories: " + ex);
            }
            return dataSet;
        }

        // searching
        // by ID
        [WebMethod]
        public DataSet searchCategoryByID(string categoryID)
        {
            DataSet dataSet = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Select * from CategoryTable where CategoryID = '" + categoryID + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataSet, "CategoryTable");
            }

            catch (Exception exc)
            {
                Console.WriteLine("There was an error searching for the category: " + exc);
            }
            return dataSet;
        }

        // by name
        [WebMethod]
        public DataSet searchCategoryByName(string categoryName)
        {
            DataSet dataSet = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Select * from CategoryTable where CategoryName = '" + categoryName + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataSet, "CategoryTable");
            }

            catch (Exception exc)
            {
                Console.WriteLine("There was an error searching for the category: " + exc);
            }
            return dataSet;
        }



        // material
        [WebMethod]
        public DataSet getMaterialName()
        {
            DataSet dataSet = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand
                    ("Select MaterialName from MaterialTable", con);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);  // filling the data adapter with the data from table

                dataAdapter.Fill(dataSet, "MaterialTable");    // filling dataset with data adapter data 
            }

            catch (Exception ex)
            {
                Console.WriteLine("There was an error in selecting material name: " + ex);
            }
            return dataSet;

        }

        [WebMethod]
        public string getMaterialID(string materialName)
        {
            string materialID = "";
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Select MaterialID from MaterialTable where MaterialName= '" + materialName + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();

                Boolean records = dr.HasRows;
                if (records)
                {
                    while (dr.Read())
                    {
                        materialID = dr[0].ToString();
                    }
                }
                dr.Close();
            }
            catch (Exception exc)
            {
                Console.Write("There was an error: " + exc);
            }
            return materialID;
        }

        [WebMethod]
        public DataSet getAllMaterials()
        {
            DataSet dataSet = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand
                    ("Select * from MaterialTable", con);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);  // filling the data adapter with the data from table

                dataAdapter.Fill(dataSet, "MaterialTable");    // filling dataset with data adapter data 
            }

            catch (Exception ex)
            {
                Console.WriteLine("There was an error in finding the materials: " + ex);
            }
            return dataSet;
        }

        // searching for material
        // by ID
        [WebMethod]
        public DataSet searchMaterialByID(string materialID)
        {
            DataSet dataSet = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Select * from MaterialTable where MaterialID = '" + materialID + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataSet, "MaterialTable");
            }

            catch (Exception exc)
            {
                Console.WriteLine("There was an error searching for the material: " + exc);
            }
            return dataSet;
        }

        // by name
        [WebMethod]
        public DataSet searchMaterialByName(string materialName)
        {
            DataSet dataSet = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Select * from MaterialTable where MaterialName = '" + materialName + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataSet, "MaterialTable");
            }

            catch (Exception exc)
            {
                Console.WriteLine("There was an error searching for the material: " + exc);
            }
            return dataSet;
        }


        // next is blanket
        [WebMethod]
        public DataSet getBlanketName()
        {
            DataSet dataSet = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand
                    ("Select BlanketName from BlanketTable", con);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);  // filling the data adapter with the data from table

                dataAdapter.Fill(dataSet, "BlanketTable");    // filling dataset with data adapter data 
            }

            catch (Exception ex)
            {
                Console.WriteLine("There was an error in selecting the blanket name: " + ex);
            }
            return dataSet;

        }

        // next is blanket id
        [WebMethod]
        public DataSet getBlanketIDs()
        {
            DataSet dataSet = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand
                    ("Select BlanketID from BlanketTable", con);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);  // filling the data adapter with the data from table

                dataAdapter.Fill(dataSet, "BlanketTable");    // filling dataset with data adapter data 
            }

            catch (Exception ex)
            {
                Console.WriteLine("There was an error in selecting the blanket ID: " + ex);
            }
            return dataSet;

        }

        // next is blanket price
        [WebMethod]
        public decimal getBlanketPrice(string blanketID)
        {
            decimal price = 0m;
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand
                    ("Select Price from BlanketTable where BlanketID = '"+blanketID+"'", con);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    price = Convert.ToDecimal(result);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("There was an error in selecting the blanket price: " + ex);
            }
            return price;

        }

        [WebMethod]
        public string getBlanketID(string blanketName)
        {
            string materialID = "";
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Select BlanketID from BlanketTable where BlanketName= '" + blanketName + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();

                Boolean records = dr.HasRows;
                if (records)
                {
                    while (dr.Read())
                    {
                        materialID = dr[0].ToString();
                    }
                }
                dr.Close();
            }
            catch (Exception exc)
            {
                Console.Write("There was an error: " + exc);
            }
            return materialID;
        }

        // this it to get blanket details 

        // for manufacturer to search
        // by ID
        [WebMethod]
        public DataSet searchBlanketByIDManufacturer(string blanketID)
        {
            DataSet dataSet = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Select b.* , m.Quantity, m.ProductionCapacity, m.LeadTimeInDays " +
                    "from BlanketTable b " +
                    "INNER JOIN ManufacturerStockTable m ON b.BlanketID = m.BlanketID where b.BlanketID = '" + blanketID + "'", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataSet, "BlanketTable");

            }

            catch (Exception exc)
            {
                Console.WriteLine("There was an error searching for the blanket: " + exc);
            }
            return dataSet;
        }

        // by price
        [WebMethod]
        public DataSet searchBlanketByPriceManufacturer(decimal price)
        {
            DataSet dataSet = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Select b.* , m.Quantity, m.ProductionCapacity, m.LeadTimeInDays " +
                    "from BlanketTable b " +
                    "INNER JOIN ManufacturerStockTable m ON b.BlanketID = m.BlanketID where b.Price = '" + price + "'", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataSet, "BlanketTable");

            }

            catch (Exception exc)
            {
                Console.WriteLine("There was an error searching for the blanket: " + exc);
            }
            return dataSet;
        }

        // other criteria
        // searching by everything but its optional
        [WebMethod]
        public DataSet searchBlanketByCriteriaManufacturer(string blanketName, string blanketID, string materialID, string categoryID, string size, string colour)
        {
            DataSet dataSet = new DataSet();
            try
            {
                getConnection();
                // we build the query first
                string query = "Select b.*,  m.Quantity, m.ProductionCapacity, m.LeadTimeInDays from BlanketTable b" +
                    " Inner Join ManufacturerStockTable m On b.BlanketID = m.BlanketID where 1=1";

                // now putting the query values if they are present
                if (!string.IsNullOrEmpty(blanketName))
                {
                    query += " AND BlanketName LIKE '" + blanketName + "'";
                }

                if (!string.IsNullOrEmpty(blanketID))
                {
                    query += " AND BlanketID = '" + blanketID + "'";
                }

                if (!string.IsNullOrEmpty(materialID))
                {
                    query += " AND MaterialID = '" + materialID + "'";
                }

                if (!string.IsNullOrEmpty(categoryID))
                {
                    query += " AND CategoryID = '" + categoryID + "'";
                }

                if (!string.IsNullOrEmpty(size))
                {
                    query += " AND Size LIKE '%" + size + "%'";
                }

                if (!string.IsNullOrEmpty(colour))
                {
                    query += " AND Colour = '" + colour + "'";
                }

                // running the command and filling the dataset with values 
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataSet, "BlanketTable");
            }

            catch (Exception ex)
            {
                Console.WriteLine("There was an error in searching for the blanket: " + ex);
            }
            return dataSet;

        }



        // for distributor to search
        // by ID
        [WebMethod]
        public DataSet searchBlanketByIDDistributor(string distributorID, string blanketID)
        {
            DataSet dataSet = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Select b.* , d.Quantity " +
                    "from BlanketTable b " +
                    "INNER JOIN DistributorStockTable d ON b.BlanketID = d.BlanketID where b.BlanketID = '" + blanketID + "' and d.DistributorID = '"+distributorID+"'", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataSet, "DistributorStockTable");

            }

            catch (Exception exc)
            {
                Console.WriteLine("There was an error searching for the blanket: " + exc);
            }
            return dataSet;
        }

        // by price
        [WebMethod]
        public DataSet searchBlanketByPriceDistributor(string distributorID, decimal price)
        {
            DataSet dataSet = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Select b.* , d.Quantity " +
                    "from BlanketTable b " +
                    "INNER JOIN DistributorStockTable d ON b.BlanketID = d.BlanketID where b.Price = '" + price + "' and d.DistributorID = '"+distributorID+"'", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataSet, "BlanketTable");

            }

            catch (Exception exc)
            {
                Console.WriteLine("There was an error searching for the blanket: " + exc);
            }
            return dataSet;
        }

        // other criteria
        [WebMethod]
        public DataSet searchBlanketByCriteriaDistributor(string distributorID, string blanketName, string blanketID, string materialID, string categoryID, string size, string colour)
        {
            DataSet dataSet = new DataSet();
            try
            {
                getConnection();
                // we build the query first
                string query = "Select b.*,  d.Quantity from BlanketTable b" +
                    " Inner Join DistributorStockTable d On b.BlanketID = d.BlanketID where d.DistributorID = '"+distributorID+"' and 1=1";

                // now putting the query values if they are present
                if (!string.IsNullOrEmpty(blanketName))
                {
                    query += " AND BlanketName LIKE '" + blanketName + "'";
                }

                if (!string.IsNullOrEmpty(blanketID))
                {
                    query += " AND BlanketID = '" + blanketID + "'";
                }

                if (!string.IsNullOrEmpty(materialID))
                {
                    query += " AND MaterialID = '" + materialID + "'";
                }

                if (!string.IsNullOrEmpty(categoryID))
                {
                    query += " AND CategoryID = '" + categoryID + "'";
                }

                if (!string.IsNullOrEmpty(size))
                {
                    query += " AND Size LIKE '%" + size + "%'";
                }

                if (!string.IsNullOrEmpty(colour))
                {
                    query += " AND Colour = '" + colour + "'";
                }

                // running the command and filling the dataset with values 
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataSet, "BlanketTable");
            }

            catch (Exception ex)
            {
                Console.WriteLine("There was an error in searching for the blanket: " + ex);
            }
            return dataSet;
        }


        // seller searching
        // by price
        [WebMethod]
        public DataSet searchBlanketByPriceSeller(string sellerID, decimal price)
        {
            DataSet dataSet = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Select b.* , s.Quantity " +
                    "from BlanketTable b " +
                    "INNER JOIN SellerStockTable s ON b.BlanketID = s.BlanketID where b.Price = '" + price + "' and s.SellerID = '"+sellerID+"'", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataSet, "BlanketTable");

            }

            catch (Exception exc)
            {
                Console.WriteLine("There was an error searching for the blanket: " + exc);
            }
            return dataSet;
        }

        // other criteria
        [WebMethod]
        public DataSet searchBlanketByCriteriaSeller(string sellerID, string blanketName, string blanketID, string materialID, string categoryID, string size, string colour)
        {
            DataSet dataSet = new DataSet();
            try
            {
                getConnection();
                // we build the query first
                string query = "Select b.*,  s.Quantity from BlanketTable b" +
                    " Inner Join SellerStockTable s On b.BlanketID = s.BlanketID where s.SellerID = '"+sellerID+"' and 1=1";

                // now putting the query values if they are present
                if (!string.IsNullOrEmpty(blanketName))
                {
                    query += " AND BlanketName LIKE '" + blanketName + "'";
                }

                if (!string.IsNullOrEmpty(blanketID))
                {
                    query += " AND BlanketID = '" + blanketID + "'";
                }

                if (!string.IsNullOrEmpty(materialID))
                {
                    query += " AND MaterialID = '" + materialID + "'";
                }

                if (!string.IsNullOrEmpty(categoryID))
                {
                    query += " AND CategoryID = '" + categoryID + "'";
                }

                if (!string.IsNullOrEmpty(size))
                {
                    query += " AND Size LIKE '%" + size + "%'";
                }

                if (!string.IsNullOrEmpty(colour))
                {
                    query += " AND Colour = '" + colour + "'";
                }

                // running the command and filling the dataset with values 
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataSet, "BlanketTable");
            }

            catch (Exception ex)
            {
                Console.WriteLine("There was an error in searching for the blanket: " + ex);
            }
            return dataSet;
        }

        // to delete a blanket by its ID
        [WebMethod]
        public string deleteBlanket(string blanketID)
        {
            int noRows = 0;
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Delete from BlanketTable where BlanketID= '"+blanketID+"'", con);
                
                noRows = cmd.ExecuteNonQuery();   // executing the query and how many rows affected
                
                if (noRows > 0)
                {

                    Console.WriteLine("Data was deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Data was not deleted.");
                }
            }
            catch (Exception exc)
            {
                Console.Write("There was an error: " + exc);
            }
            return "Deleted: " + noRows.ToString();

        }

        // to update a blanket's details
        // based on critiera user gives
        [WebMethod]
        public String updateBlanket(string blanketID, string blanketName, string categoryID, string materialID, string size, string colour, string description)
        {
            int noRows = 0;
            try
            {
                getConnection();
                
                // we build the query first
                string query = "Update BlanketTable SET ";

                // build the set clause
                List<string> setClause = new List<string>();

                // now putting the query values if they are present
                if (!string.IsNullOrEmpty(blanketName))
                {
                    setClause.Add("BlanketName = '" + blanketName + "'");
                }             

                if (!string.IsNullOrEmpty(materialID))
                {                  
                    setClause.Add("MaterialID = '" + materialID + "'");
                }

                if (!string.IsNullOrEmpty(categoryID))
                {                
                    setClause.Add("CategoryID = '" + categoryID + "'");
                }

                if (!string.IsNullOrEmpty(size))
                {                
                    setClause.Add("Size = '" + size + "'");
                }

                if (!string.IsNullOrEmpty(colour))
                {                  
                    setClause.Add("Colour = '" + colour + "'");
                }              

                if (!string.IsNullOrEmpty(description))
                {
                    setClause.Add("Description = '" + description + "'");
                }

                // if nothing to update
                if (setClause.Count == 0) 
                {
                    return "There were no fields to update!";
                }

                // joining the setClause
                query += string.Join(", ", setClause);

                // adding the where clause based on specfic blanket ID because we update
                // based on the blanket ID
                query += " WHERE BlanketID = '" + blanketID + "'";

                // executing
                SqlCommand cmd = new SqlCommand(query, con);
                noRows = cmd.ExecuteNonQuery();   // executing the query and how many rows affected
                if (noRows > 0)
                {
                    Console.WriteLine("Data was updated in the table successfully.");
                }
                else
                {
                    Console.WriteLine("Data was not updated in the table.");
                }

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return noRows.ToString();
        }

        // seperate methos to update price
        [WebMethod]
        public string updateBlanketPrice( string blanketID, decimal price)
        {
            getConnection();

            SqlCommand cmd = new SqlCommand("Update BlanketTable SET Price = " +price+ " WHERE BlanketID = '" + blanketID + "'", con);
                
            int noRows = cmd.ExecuteNonQuery();   // executing the query and how many rows affected
            if (noRows > 0)
            {
                Console.WriteLine("Data was updated in the table successfully.");
            }
            else
            {
                Console.WriteLine("Data was not updated in the table.");
            }
            return noRows.ToString();
        }

        // updatr stock details
        // update production caapcity
        [WebMethod]
        public string updateProductionCapacity(string blanketID, int productionCapacity)
        { 
            int noRows = 0;
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Update ManufacturerStockTable SET ProductionCapacity = " + productionCapacity + " where BlanketId = '" + blanketID + "'", con);
                noRows = cmd.ExecuteNonQuery();   // executing the query and how many rows affected
                if (noRows > 0)
                {
                    Console.WriteLine("Data was updated in the table successfully.");
                }
                else
                {
                    Console.WriteLine("Data was not updated in the table.");
                }

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return noRows.ToString();
        }

        // update lead time
        [WebMethod]
        public string updateLeadTime(string blanketID, int leadTimeDays)
        {
            int noRows = 0;
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Update ManufacturerStockTable SET LeadTimeInDays = " + leadTimeDays + " where BlanketId = '" + blanketID + "'", con);
                noRows = cmd.ExecuteNonQuery();   // executing the query and how many rows affected
                if (noRows > 0)
                {
                    Console.WriteLine("Data was updated in the table successfully.");
                }
                else
                {
                    Console.WriteLine("Data was not updated in the table.");
                }

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return noRows.ToString();
        }

        // now the methods for displaying information with stock values of blanket
        // all blanket details for manufacturer
        [WebMethod]
        public DataSet getAllBlanketDetailsForManufacturer()
        {
            DataSet dataSet = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand
                    ("SELECT b.*, m.Quantity, m.ProductionCapacity, m.LeadTimeInDays " +
                     "FROM BlanketTable b " +
                     "INNER JOIN ManufacturerStockTable m ON b.BlanketID = m.BlanketID", con);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);  // filling the data adapter with the data from table

                dataAdapter.Fill(dataSet, "BlanketTable");    // filling dataset with data adapter data 
            }

            catch (Exception ex)
            {
                Console.WriteLine("There was an error in selecting the blanket name: " + ex);
            }
            return dataSet;
        }

        // all blanket details for seller
        [WebMethod]
        public DataSet getAllBlanketDetailsForSeller(string sellerID)
        {
            DataSet dataSet = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand
                    ("Select b.*, s.Quantity from BlanketTable b INNER JOIN SellerStockTable s ON b.BlanketID = s.BlanketID where s.SellerID = '"+sellerID+"'", con);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);  // filling the data adapter with the data from table

                dataAdapter.Fill(dataSet, "BlanketTable");    // filling dataset with data adapter data 
            }

            catch (Exception ex)
            {
                Console.WriteLine("There was an error in selecting the blanket name: " + ex);
            }
            return dataSet;
        }

        // all blanket details for distributor
        [WebMethod]
        public DataSet getAllBlanketDetailsForDistributor(string distributorID)
        {
            DataSet dataSet = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand
                    ("Select b.*, d.Quantity from BlanketTable b INNER JOIN DistributorStockTable d ON b.BlanketID = d.BlanketID where d.DistributorID = '"+distributorID+"'", con);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);  // filling the data adapter with the data from table

                dataAdapter.Fill(dataSet, "BlanketTable");    // filling dataset with data adapter data 
            }

            catch (Exception ex)
            {
                Console.WriteLine("There was an error in selecting the blanket name: " + ex);
            }
            return dataSet;
        }
    }

}

