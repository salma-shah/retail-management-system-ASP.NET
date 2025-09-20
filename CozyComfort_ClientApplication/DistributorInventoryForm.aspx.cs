using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CozyComfort_ClientApplication
{
    public partial class DistributorInventoryForm : System.Web.UI.Page
    {
        BlanketWebService.BlanketWebServiceSoapClient blanketObj = new BlanketWebService.BlanketWebServiceSoapClient();
        InventoryWebService.InventoryWebServiceSoapClient inventoryObj = new InventoryWebService.InventoryWebServiceSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)

            {
                string distributorID = Session["userID"]?.ToString();
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
            string distributorID = Session["userID"]?.ToString();
            DataSet dataSetAll = blanketObj.getAllBlanketDetailsForDistributor(distributorID);
            gvBlankets.DataSource = dataSetAll;
            gvBlankets.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string selectedMaterial = blanketObj.getMaterialID(dropListMaterial.Text);
            string selectedCategory = blanketObj.getCategoryID(dropListCategory.Text);

            string distributorID = Session["userID"]?.ToString();

            // intitialzing dataset
            DataSet dataSet;
            // since we have two methods, one for all string crteria and another for price, we check if price has been entered first

            // if price is entered , we search by price
            if (!string.IsNullOrEmpty(txtPrice.Text))
            {
                dataSet = blanketObj.searchBlanketByPriceDistributor(distributorID, Convert.ToDecimal(txtPrice.Text));
            }

            // else just search by other crtieria
            else { dataSet = blanketObj.searchBlanketByCriteriaDistributor(distributorID, txtBlanketName.Text, txtBlanketID.Text, selectedMaterial, selectedCategory, dropListSizes.Text.ToString(), txtColour.Text); }

            gvBlankets.DataSource = dataSet;
            gvBlankets.DataBind();
        }

        protected void btnAddStock_Click(object sender, EventArgs e)
        {
            string distributorID = Session["userID"]?.ToString();  // session user id because we need to update the stock of only current seller
            inventoryObj.addStockForDistributor(distributorID, txtBlanketID.Text, Convert.ToInt32(txtStock.Text));
            loadGrid();

        }

        protected void btnDecreaseStock_Click(object sender, EventArgs e)
        {
            string distributorID = Session["userID"]?.ToString();  // session user id because we need to update the stock of only current seller
            inventoryObj.reduceStockForDistributor(distributorID, txtBlanketID.Text, Convert.ToInt32(txtStock.Text));
            loadGrid();
        }
    }
}