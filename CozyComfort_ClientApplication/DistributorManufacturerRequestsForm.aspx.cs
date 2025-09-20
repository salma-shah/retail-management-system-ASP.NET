using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CozyComfort_ClientApplication
{
    public partial class DistributorManufacturerRequestsForm : System.Web.UI.Page
    {
        OrderRequestWebService.OrderRequestsWebServiceSoapClient orderObj = new OrderRequestWebService.OrderRequestsWebServiceSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            string distributorID = Session["userID"]?.ToString();

            if (!IsPostBack)
            {
                DataSet dataset = orderObj.getAllRequestsSentToManufacturer(distributorID);
                gvRequests.DataSource = dataset;
                gvRequests.DataBind();
            }
        }
    }
}