using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
namespace CozyComfort_ClientApplication
{
    public partial class ManufacturerBlanketForm : System.Web.UI.Page
    {
        // web services
        BlanketWebService.BlanketWebServiceSoapClient blanketObj = new BlanketWebService.BlanketWebServiceSoapClient();    
        InventoryWebService.InventoryWebServiceSoapClient inventoryObj = new InventoryWebService.InventoryWebServiceSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            //  filling the dropdown with material and category values
            if (!IsPostBack)
            {

                // material
                DataSet dataSet = blanketObj.getMaterialName();
                dropListMaterial.DataSource = dataSet;
                dropListMaterial.DataBind();
                dropListMaterial.DataValueField = "MaterialName";
                dropListMaterial.DataBind();
                dropListMaterial.Items.Insert(0, new ListItem("Select Material", ""));

                // category
                DataSet ds = blanketObj.getCategoryName();
                dropListCategory.DataSource = ds;
                dropListCategory.DataBind();
                dropListCategory.DataValueField = "CategoryName";
                dropListCategory.DataBind();
                dropListCategory.Items.Insert(0, new ListItem("Select Category", ""));

                // auto generating the blanket id into a label so it cannot be altered
                txtBlanketID.Text = blanketObj.autoBlanketID();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            // assigning
            string blanketID = blanketObj.autoBlanketID();
            string categoryID = blanketObj.getCategoryID(dropListCategory.Text);
            string materialID = blanketObj.getMaterialID(dropListMaterial.Text);
            string size = dropListSize.SelectedItem.ToString();
            DateTime lastUpdated = DateTime.Now;

            // before calling the object and methods, we check validations
            if (Convert.ToInt32(txtQty.Text) > Convert.ToInt32(txtProductionCapacity.Text))
            {
                lblText.Text = "The quantity of the blanket cannot be greater than its production capacity.";
            }
            else
            {
                // calling the services methods
                string result = blanketObj.insertBlanket(blanketID, txtBlanketName.Text, categoryID, materialID, size, Convert.ToDecimal(txtPrice.Text), txtColour.Text, txtDescription.Text);

                string qtyResult = inventoryObj.setStock(blanketID, Convert.ToInt32(txtQty.Text), Convert.ToInt32(txtProductionCapacity.Text), Convert.ToInt32(txtLeadTime.Text), lastUpdated);

                int noRows = Int32.Parse(result) + Int32.Parse(qtyResult);
                if (noRows >= 1)
                {
                    lblText.Text = "Blanket was added successfully to the database.";
                }
                else
                {
                    lblText.Text = "Blanket was not added to the database. There was an error.";
                }
            }

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string selectedMaterial = blanketObj.getMaterialID(dropListMaterial.Text);
            string selectedCategory = blanketObj.getCategoryID(dropListCategory.Text);

            int noRows = 0, noRowsStock = 0, noRowsLeadTime=0, noRowsPrice = 0 ;

            if (!string.IsNullOrEmpty(txtPrice.Text))
            {
                // this method is called just to call the update PRICE since it is a decimal value. we convert it from text to decimal first
                string priceUpdateResult = blanketObj.updateBlanketPrice(txtBlanketID.Text, Convert.ToDecimal(txtPrice.Text));
                noRowsPrice = Int32.Parse(priceUpdateResult);
            }
             if (!string.IsNullOrEmpty(txtProductionCapacity.Text))
            {
                // this method is to update stock values
                string stockCapacityResult = blanketObj.updateProductionCapacity(txtBlanketID.Text, Convert.ToInt32(txtProductionCapacity.Text));
                noRowsPrice = Int32.Parse(stockCapacityResult);
            }

            if (!string.IsNullOrEmpty(txtLeadTime.Text))
            {
                // this method is to update lead time in days 
                string leadTimeResult = blanketObj.updateLeadTime(txtBlanketID.Text, Convert.ToInt32(txtLeadTime.Text));
                noRowsLeadTime = Int32.Parse(leadTimeResult);
            }

            if (!string.IsNullOrEmpty(txtBlanketName.Text) || !string.IsNullOrEmpty(dropListSize.Text) || !string.IsNullOrEmpty(txtColour.Text) || !string.IsNullOrEmpty(txtDescription.Text))
            {
                // this it to update by all the STRING categories
                string result = blanketObj.updateBlanket(txtBlanketID.Text, txtBlanketName.Text, selectedCategory, selectedMaterial, dropListSize.Text.ToString(), txtColour.Text, txtDescription.Text);
                noRows = Int32.Parse(result);
            }

            // this is the noRows affected 
            if (noRows >= 1 || noRowsPrice > 0 || noRowsStock >0 || noRowsLeadTime >0)
            {
                lblText.Text = "The details for blanket ID: " +txtBlanketID.Text + " were updated successfully to the database.";
            }
            else
            {
                lblText.Text = "Blanket details were not updated in the database. There was an error.";
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string result = blanketObj.deleteBlanket(txtBlanketID.Text);
            int noRows = Int32.Parse(result);

            // checking how many rows were deleted
            if (noRows >= 1)
            {
                lblText.Text = "The blanket with blanket ID:" +txtBlanketID.Text + " was deleted from the database.";
            }
            else
            {
                lblText.Text = "Blanket was not deleted from the database. There was an error.";
            }
        }
    }
}