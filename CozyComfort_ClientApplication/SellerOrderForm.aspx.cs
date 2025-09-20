using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace CozyComfort_ClientApplication
{
    public partial class SellerOrderForm : System.Web.UI.Page
    {
        // web services that are used
        BlanketWebService.BlanketWebServiceSoapClient blanketObj = new BlanketWebService.BlanketWebServiceSoapClient();
        OrderRequestWebService.OrderRequestsWebServiceSoapClient orderObj = new OrderRequestWebService.OrderRequestsWebServiceSoapClient();

        string sellerID;
        protected void Page_Load(object sender, EventArgs e)
        {
            sellerID = Session["userID"]?.ToString();  // user id from session
            if (!IsPostBack)
            {                            
               // loading grid
                loadGrid();
                // blankets are being populated in the dropdown
                DataSet dataSet = blanketObj.getBlanketName();
                dropListBlanket.DataSource = dataSet;
                dropListBlanket.DataBind();
                dropListBlanket.DataValueField = "BlanketName";
                dropListBlanket.DataBind();

                // order id
                lblOrderID.Text = orderObj.autoOrderID();
            
            }
        }

        protected void btnOrder_Click(object sender, EventArgs e)
        {
            string sellerID = Session["userID"]?.ToString();  // user id from session
            string blanketID = blanketObj.getBlanketID(dropListBlanket.Text);
            DateTime orderDate = DateTime.Now; 
           
            // total cost
           // first lets get the price of the blanket
            decimal price = blanketObj.getBlanketPrice(blanketID);
            // now we calculate
            decimal totalCost = orderObj.calculateTotalCost( price , Convert.ToInt32(txtQty.Text));

            string result = orderObj.makeOrder(lblOrderID.Text, sellerID, blanketID, Convert.ToInt32(txtQty.Text), totalCost, orderDate, "Pending");
            
            if (result.StartsWith("No stock"))
            {
                lblText.Text = result; 
                
            } 
            else if(result.EndsWith("successfully!"))
            {
                lblText.Text = "Your order was placed successfully!";
            }
            else
            {
                lblText.Text = "There was an error!";
               
            }
            loadGrid();

        }

        protected void gvOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvOrders.Rows[rowIndex];

            string orderID = gvOrders.DataKeys[rowIndex].Values["OrderID"].ToString();

            if (e.CommandName == "acceptOrder")
            {
                string result = orderObj.acceptPendingOrder(orderID);
                lblText.Text = result;
                loadGrid();
            }
        }

        private void loadGrid()
        {
            // all orders grid view
            DataSet dataSetAll = orderObj.getAllOrderDetailsForSeller(sellerID);
            gvOrders.DataSource = dataSetAll.Tables[0];
            gvOrders.DataBind();
        }
    }
}