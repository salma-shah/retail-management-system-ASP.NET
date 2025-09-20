using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CozyComfort_ClientApplication
{
    public partial class ManufacturerRequestsForm : System.Web.UI.Page
    {
        InventoryWebService.InventoryWebServiceSoapClient inventoryObj = new InventoryWebService.InventoryWebServiceSoapClient();
        OrderRequestWebService.OrderRequestsWebServiceSoapClient orderObj = new OrderRequestWebService.OrderRequestsWebServiceSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string manufacturerID = Session["userID"]?.ToString();  // user id from session
                loadGrid();
            }
        }

        protected void loadGrid()
        {
            string manufacturerID = Session["userID"]?.ToString();  // user id from sessio
            // loading grid view with requests
            DataSet dataSetAll = orderObj.getAllRequestsForManufacturer();
            gvRequests.DataSource = dataSetAll.Tables[0];
            gvRequests.DataBind();
        }

        protected void checkStock(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "checkStock")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvRequests.Rows[rowIndex];

                string blanketID = gvRequests.DataKeys[rowIndex].Values["BlanketID"].ToString();
                int qty = Convert.ToInt32(gvRequests.DataKeys[rowIndex].Values["Quantity"].ToString());
                string indexRequestID = gvRequests.DataKeys[rowIndex].Values["RequestID"].ToString();

                bool isAvailable = inventoryObj.isBlanketAvailableForManufacturer(blanketID, qty);
                if (isAvailable)
                {
                    lblText.Text = "The blanket is available and in stock.";
                    string result = orderObj.updateStatusForManufacturerRequest(indexRequestID, "AvailableStock");
                }
                else
                {
                    // now check the production capacity and lead time
                    // in order to inform how long it will take to produce how much
                    // we get production capacity and lead time information
                    DataSet dataSet = inventoryObj.getProductionCapacityAndLeadTime(blanketID);
                    if(dataSet != null && dataSet.Tables.Count > 0)
                    {
                        // converting the dataset values into variables
                        int productionCapacity = Convert.ToInt32(dataSet.Tables[0].Rows[0]["ProductionCapacity"]);
                        int leadTimeDays = Convert.ToInt32(dataSet.Tables[0].Rows[0]["LeadTimeInDays"]);

                        // information msg 
                        string information = "The blanket is not available in stock. However, it can be produced with a capacity of " + productionCapacity +
                            " units per batch. The estimated amount of time is " + leadTimeDays + " day(s).";
                        // displaying message in label
                        lblText.Text = information;

                        // updating the status and sending information back to the distributor
                        string result = orderObj.updateStatusForManufacturerRequest(indexRequestID, "CurrentlyUnavailable");
                        orderObj.sendInformationToDistributor(indexRequestID, information);
                        
                    }
                    
                }
                loadGrid();
            }
        }



    }
}