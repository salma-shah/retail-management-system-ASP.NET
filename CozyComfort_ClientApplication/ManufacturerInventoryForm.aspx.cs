using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Xml;

namespace CozyComfort_ClientApplication
{
    public partial class SearchAndUpdateForm : System.Web.UI.Page
    {
        BlanketWebService.BlanketWebServiceSoapClient blanketObj = new BlanketWebService.BlanketWebServiceSoapClient();
        InventoryWebService.InventoryWebServiceSoapClient inventoryObj = new InventoryWebService.InventoryWebServiceSoapClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)

            {
                loadGrid();
                // material
                DataSet dataSetMaterial = blanketObj.getMaterialName();
                dropListMaterial.DataSource = dataSetMaterial;
                dropListMaterial.DataValueField = "MaterialName";
                dropListMaterial.DataBind();
                dropListMaterial.Items.Insert(0, new ListItem("Select Material", ""));

                // category
                DataSet dataSetCategory= blanketObj.getCategoryName();
                dropListCategory.DataSource = dataSetCategory;
                dropListCategory.DataValueField = "CategoryName";
                dropListCategory.DataBind();
                dropListCategory.Items.Insert(0, new ListItem("Select Category", ""));
               
            }
        }

        protected void loadGrid()
        {
            // loading grid view with details
            DataSet dataSetAll = blanketObj.getAllBlanketDetailsForManufacturer();
            gvBlankets.DataSource = dataSetAll;
            gvBlankets.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string selectedMaterial = blanketObj.getMaterialID(dropListMaterial.Text);
            string selectedCategory = blanketObj.getCategoryID(dropListCategory.Text);

            // intitialzing dataset
            DataSet dataSet;
            // since we have a few methods, one for all string crteria and another for price, check if price has been entered first

            // if price is entered , we search by price
            if (!string.IsNullOrEmpty(txtPrice.Text))
            {
               dataSet= blanketObj.searchBlanketByPriceManufacturer(Convert.ToDecimal(txtPrice.Text));
            }

            else if(!string.IsNullOrEmpty(txtBlanketID.Text))
            {
                dataSet = blanketObj.searchBlanketByIDManufacturer(txtBlanketID.Text);
            }

            // else just search by other crtieria
            else { dataSet= blanketObj.searchBlanketByCriteriaManufacturer(txtBlanketName.Text, txtBlanketID.Text, selectedMaterial, selectedCategory, dropListSizes.Text.ToString(), txtColour.Text); }
            
            gvBlankets.DataSource = dataSet;
            gvBlankets.DataBind();

        }

        protected void btnAddStock_Click(object sender, EventArgs e)
        {
            inventoryObj.addStockForManufacturer(txtBlanketID.Text, Convert.ToInt32(txtStock.Text));
            gvBlankets.DataSource = blanketObj.getAllBlanketDetailsForManufacturer();
            gvBlankets.DataBind();
        }

        protected void btnDecreaseStock_Click(object sender, EventArgs e)
        {
            inventoryObj.reduceStockForManufacturer(txtBlanketID.Text, Convert.ToInt32(txtStock.Text));
            gvBlankets.DataSource = blanketObj.getAllBlanketDetailsForManufacturer();
            gvBlankets.DataBind();
        }
    }
}