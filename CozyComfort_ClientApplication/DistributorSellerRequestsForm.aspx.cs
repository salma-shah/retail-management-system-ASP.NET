using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CozyComfort_ClientApplication
{
    public partial class DistributorSellerRequestsForm : System.Web.UI.Page
    {
        BlanketWebService.BlanketWebServiceSoapClient blanketObj = new BlanketWebService.BlanketWebServiceSoapClient();
        OrderRequestWebService.OrderRequestsWebServiceSoapClient orderObj = new OrderRequestWebService.OrderRequestsWebServiceSoapClient();
        InventoryWebService.InventoryWebServiceSoapClient inventoryObj = new InventoryWebService.InventoryWebServiceSoapClient();

        string requestID;   // request id variable
        string distributorID;  // distributor id variable
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string distributorID = Session["userID"]?.ToString();  // user id from session              
                string requestID = orderObj.autoRequestToManufacturerID();   // id for the request
                loadGrid();

                // blankets are being populated in the dropdown
                DataSet dataSet = blanketObj.getBlanketIDs();
                dropListBlanket.DataSource = dataSet;
                dropListBlanket.DataBind();
                dropListBlanket.DataValueField = "BlanketID";
                dropListBlanket.DataBind();

                // order id
                lblRequestID.Text = orderObj.autoRequestToManufacturerID();
                             
            }
        }

        protected void loadGrid()
        {
            string distributorID = Session["userID"]?.ToString();
            // all orders grid view
            DataSet dataSetAll = orderObj.getAllRequestsForDistributor(distributorID);
            gvRequests.DataSource = dataSetAll.Tables[0];
            gvRequests.DataBind();
        }

        protected void btnRequest_Click(object sender, EventArgs e)
        {
            string distributorID = Session["userID"]?.ToString();
            DateTime requestedTime = DateTime.Now;
            // making the request
            string result = orderObj.sendRequestToManufacturer(lblRequestID.Text, distributorID, dropListBlanket.Text, Convert.ToInt32(txtQty.Text), "Pending", requestedTime);
            
            if (result.EndsWith("successfully!"))
            {
                lblText.Text = result;
            }
            else
            {
                lblText.Text = "There was an error in sending the request.";
            }

            loadGrid();
        }

        protected void gvRequests_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvRequests.Rows[rowIndex];

            string blanketID = gvRequests.DataKeys[rowIndex].Values["BlanketID"].ToString();
            int qty = Convert.ToInt32(gvRequests.DataKeys[rowIndex].Values["Quantity"].ToString());
            string indexRequestID = gvRequests.DataKeys[rowIndex].Values["RequestID"].ToString();
            TextBox txtInfo = (TextBox)row.FindControl("txtInfo");
            string information = txtInfo.Text;

            if (e.CommandName == "checkStock")
            {
                bool isAvailable = inventoryObj.isBlanketAvailableForDistributor(blanketID, qty);

                if (isAvailable)
                {
                    lblText.Text = "The blanket is available and in stock.";
                    loadGrid();
                   // string result = orderObj.updateStatusForDistributorRequest(indexRequestID, "AvailableStock");
                    // inventoryObj.red
                }
                else
                {
                    lblText.Text = "The blanket is not available in stock. Please send a request to the manufacturer for more information.";
                }

            }
            else if (e.CommandName == "markAvailable")
            {
                string result = orderObj.updateStatusForDistributorRequest(indexRequestID, "AvailableStock");
                lblText.Text = "The seller was informed.";
                loadGrid();
            }
            else if (e.CommandName == "markUnavailable")
            {
                // updating status
                string result = orderObj.updateStatusForDistributorRequest(indexRequestID, "CurrentlyUnavailable");
                // sending the extra information message to seller
                string infoResult = orderObj.sendInformationToSeller(indexRequestID, information);

                lblText.Text = "The seller was informed.";
                loadGrid();
            }

        }

        }

    
}