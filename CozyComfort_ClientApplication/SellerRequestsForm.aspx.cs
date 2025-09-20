using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace CozyComfort_ClientApplication
{
    public partial class SellerRequestsForm : System.Web.UI.Page
    {
        UserWebService.UserWebServiceSoapClient userObj = new UserWebService.UserWebServiceSoapClient();
        OrderRequestWebService.OrderRequestsWebServiceSoapClient orderObj = new OrderRequestWebService.OrderRequestsWebServiceSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sellerID = Session["userID"]?.ToString();  // user id from session;
                string distributorID = userObj.getDistributorIDForSellerID(sellerID);
                // lblText.Text = $"Seller ID: {sellerID}, Distributor ID: {distributorID}";

                // all requests grid view       
                DataSet dataSetAll = orderObj.getAllRequestsSentToDistributor(sellerID, distributorID);
                if (dataSetAll != null)
                {
                    gvRequests.DataSource = dataSetAll;
                    gvRequests.DataBind();
                }
                else
                {
                    lblText.Text = "You have not sent any requests to the distributor yet.";
                }                 
                
            }
        }
    }
}