using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace CozyComfort
{
    /// <summary>
    /// Summary description for UserWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class UserWebService : System.Web.Services.WebService
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


        // registering a user
        [WebMethod]
        public string registerUser(string userID, string name, string address, string contactNumber, string email, string password, string role)
            {
                int noRows = 0;
                try
                {
                    getConnection();
          
                SqlCommand cmd = new SqlCommand("insert into UserTable values('" + userID + "', '" + name + "','" + address + "','" + contactNumber + "' , '" + email+ "', '" + password+ "', '"+role+"')", con);
               
                noRows = cmd.ExecuteNonQuery();   // executing the query and how many rows affected
                    if (noRows > 0)
                    {
                        Console.WriteLine("The account has been registered successfully into the database!");
                    }
                    else
                    {
                        Console.WriteLine("There was an error in registering the account into the database. Please try again.");
                    }

                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }

                return noRows.ToString();
            }


        // lets generate auto user id so changes cant be made to it
        [WebMethod]
        public String autoUserID()
        {
            string userID = null;

            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Select UserID from UserTable", con);
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
                        userID = "U00" + CTR;  // adding value
                    }
                    else if (CTR >= 9 && CTR < 99)
                    {
                        CTR = CTR + 1;
                        userID = "U00" + CTR;
                    }
                    else if (CTR >= 99)
                    {
                        CTR = CTR + 1;
                        userID = "U00" + CTR;
                    }
                }
                else
                {
                    userID = "U001";
                }
                dataReader.Close();
            }

            catch (Exception ex)
            {
                userID = ex.ToString();
            }
            return userID;
        }


        // user login
        [WebMethod]
        public string login(string email, string password)
        {
            
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("SELECT UserID, Role FROM UserTable WHERE Email = '"+email+"' AND Password= '"+password+"'", con);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string userID = reader["UserID"].ToString();
                    string role = reader["Role"].ToString();
                    return $"Successful,{userID},{role}"; 
                }
                else
                {
                    return "Invalid credentials!";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }

        // logging out function
        [WebMethod(EnableSession = true)]
        public string logout()
        {
            HttpContext.Current.Session.Clear();     // clearing the session and destroying it
            HttpContext.Current.Session.Abandon();
            return "LoggedOut";
        }


        // assinging a seller to a distributor
        [WebMethod]
        public string assignDistributorToSeller(string sellerID, string distributorID)
        {
            int noRows = 0;
            try 
            { 
            getConnection();
            SqlCommand cmd = new SqlCommand("Insert into SellerDistributorAssignmentTable (SellerID,DistributorID) values ('" + sellerID + "','" + distributorID + "')", con);
               
                noRows = cmd.ExecuteNonQuery();   // executing the query and how many rows affected
                if (noRows > 0)
                {
                    Console.WriteLine("The distributor was assigned successfully to the seller!");
                }
                else
                {
                    Console.WriteLine("There was an error. Please try again.");
                }

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return noRows.ToString();

        }


        // getting the distributor id for seller id
        [WebMethod]
        public string getDistributorIDForSellerID(string sellerID)
        {
            string distributorID = "";

            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Select DistributorID from SellerDistributorAssignmentTable Where SellerID = '" + sellerID + "' ", con);
                SqlDataReader dr = cmd.ExecuteReader();
                Boolean records = dr.HasRows;
                if (records)
                {
                    while (dr.Read())
                    {
                        distributorID = dr[0].ToString();
                    }
                }
                dr.Close();
            }
            catch (Exception ex) 
            {
                return ex.ToString();
            }

            return distributorID;
        }


        // getting the role's details, like address and contacts

        [WebMethod]
        public DataSet getUserDetails(string userID)
        {
            DataSet ds = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("Select UserID, StoreName, Address, Email, ContactNumber from UserTable where UserID = '" + userID + "'", con);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                dataAdapter.Fill(ds, "UserTable");
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an erroe in retrieving the data: " + ex);
            }
            return ds;
        }


        

    }
}
