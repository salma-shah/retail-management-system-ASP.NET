using System;
using System.Data;
using System.Web.UI.WebControls;

namespace CozyComfort_ClientApplication
{
    public partial class SellerInventoryForm : System.Web.UI.Page
    {
        BlanketWebService.BlanketWebServiceSoapClient blanketObj = new BlanketWebService.BlanketWebServiceSoapClient();
        InventoryWebService.InventoryWebServiceSoapClient inventoryObj = new InventoryWebService.InventoryWebServiceSoapClient();

        string sellerID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                sellerID = Session["userID"]?.ToString();  // user id from sessiom
                loadGrid();
                // material
                DataSet dataSetMaterial = blanketObj.getMaterialName();
                dropListMaterial.DataSource = dataSetMaterial;
                dropListMaterial.DataValueField = "MaterialName";
                dropListMaterial.DataBind();
                dropListMaterial.Items.Insert(0, new ListItem("Select Material", ""));

                // category
                DataSet dataSetCategory = blanketObj.getCategoryName();
                dropListCategory.DataSource = dataSetCategory;
                dropListCategory.DataValueField = "CategoryName";
                dropListCategory.DataBind();
                dropListCategory.Items.Insert(0, new ListItem("Select Category", ""));
            }
        }

        protected void loadGrid()
        {
            // loading grid view with details
            DataSet dataSetAll = blanketObj.getAllBlanketDetailsForSeller(sellerID);
            gvBlankets.DataSource = dataSetAll;
            gvBlankets.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            sellerID = Session["userID"]?.ToString();
            string selectedMaterial = blanketObj.getMaterialID(dropListMaterial.Text);
            string selectedCategory = blanketObj.getCategoryID(dropListCategory.Text);

            // intitialzing dataset
            DataSet dataSet;
            // since we have two methods, one for all string crteria and another for price, we check if price has been entered first
            if (!string.IsNullOrEmpty(txtPrice.Text))
            // if price is entered , we search by price
            {
                dataSet = blanketObj.searchBlanketByPriceSeller(sellerID, Convert.ToDecimal(txtPrice.Text));
            }
            // else we search by other criteria
            else
            {
                dataSet = blanketObj.searchBlanketByCriteriaSeller(sellerID, txtBlanketName.Text, txtBlanketID.Text, selectedMaterial, selectedCategory, dropListSizes.Text.ToString(), txtColour.Text);
            }

            gvBlankets.DataSource = dataSet;
            gvBlankets.DataBind();

        }

        protected void btnAddStock_Click(object sender, EventArgs e)
        {
            sellerID = Session["userID"]?.ToString();  // session user id because we need to update the stock of only current seller
            inventoryObj.addStockForSeller(sellerID, txtBlanketID.Text, Convert.ToInt32(txtStock.Text));
            loadGrid();
        }

        protected void btnDecreaseStock_Click(object sender, EventArgs e)
        {
            sellerID = Session["userID"]?.ToString();  // session user id because we need to update the stock of only current seller
            inventoryObj.reduceStockForSeller(sellerID, txtBlanketID.Text, Convert.ToInt32(txtStock.Text));
            loadGrid();
        }

    }
}