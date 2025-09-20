using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CozyComfort_ClientApplication
{
    public partial class ManufacturerDashboard1 : System.Web.UI.Page
    {
        UserWebService.UserWebServiceSoapClient userObj = new UserWebService.UserWebServiceSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            string distributorID = Session["userID"]?.ToString();
            loadDetails();
        }

        protected void loadDetails()
        {
            string distributorID = Session["userID"]?.ToString();
            DataSet dataSet = new DataSet();
            dataSet = userObj.getUserDetails(distributorID);

            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                DataRow row = dataSet.Tables[0].Rows[0];

                lblManufacturerID.Text = row["UserID"].ToString();
                lblOrgName.Text = row["StoreName"].ToString();
                lblAddress.Text = row["Address"].ToString();
                lblEmail.Text = row["Email"].ToString();
                lblContact.Text = row["ContactNumber"].ToString();
            }
        }
    }
}