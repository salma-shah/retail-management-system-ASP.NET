using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace CozyComfort_ClientApplication
{
    public partial class LoginForm : System.Web.UI.Page
    {
        // calling the web service reference
        UserWebService.UserWebServiceSoapClient obj = new UserWebService.UserWebServiceSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            // now we hash the password to make sure it matches the db one
            string hashedPassword = SecurityHelper.hashPassword(password);
            string result = obj.login(email, hashedPassword);

            if (result.StartsWith("Successful"))
            {
                // now lets creatr a string array
                // split it by '.'
                string[] parts = result.Split(',');

                if (parts.Length == 3)
                {
                    // seperate user id and role
                    string userID = parts[1];
                    string role = parts[2];

                    // pass throughs sessions
                    Session["userID"] = userID;
                    Session["role"] = role;

                    {

                        // switch case, for each role, different paths
                        switch (role)
                        {
                            case "Seller":
                                Response.Redirect("SellerDashboard.aspx");
                                break;
                            case "Distributor":
                                Response.Redirect("DistributorDashboard.aspx");
                                break;
                            case "Manufacturer":
                                Response.Redirect("ManufacturerDashboard.aspx");
                                break;
                            default:
                                lblText.Text = "Unknown role.";
                                break;
                        }
                    }
                }

            }
            else
            {
                lblText.Text = "Invalid credentials.";
                lblText.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}