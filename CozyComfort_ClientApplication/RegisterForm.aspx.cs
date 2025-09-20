using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using System.Xml;
using System.Security.Policy;

namespace CozyComfort_ClientApplication
{
    public partial class RegisterForm : System.Web.UI.Page
    {
        UserWebService.UserWebServiceSoapClient obj = new UserWebService.UserWebServiceSoapClient();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                lblUserID.Text = obj.autoUserID();
            }


        }

        protected void cvRole_serverValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = radioBtnSeller.Checked || radioBtnDistributor.Checked || radioBtnManufacturer.Checked;
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            // assigning variables to the text field values
            string userID = obj.autoUserID();
            string storeName = txtStoreName.Text;
            string address = txtAddress.Text;
            string contactNumber = txtContact.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            string role = "";

            // lets hash the password also for security measures
            string hashedPassword = SecurityHelper.hashPassword(password);

            if (radioBtnDistributor.Checked)
            {
                role = "Distributor";
            }

            if (radioBtnSeller.Checked)
            {
                role = "Seller";
            }

            if (radioBtnManufacturer.Checked)
            {
                role = "Manufacturer";
            }

            // now that we have gotten all the values, we can enter the data into the table
            string result = obj.registerUser(userID, storeName, address, contactNumber, email, hashedPassword, role);
            int noRows = Int32.Parse(result);
            if (noRows >= 1)
            {
                lblText.Text = "The account has been registered successfully!";
            }
            else
            {
                lblText.Text = "There was an error registering the account.";
            }

        }
    }
}