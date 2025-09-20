using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CozyComfort_ClientApplication
{
    public partial class Category_MaterialForm : System.Web.UI.Page
    {
        BlanketWebService.BlanketWebServiceSoapClient obj = new BlanketWebService.BlanketWebServiceSoapClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadGrids();

                // putting id in labels so they cant be altered
                lblCategoryID.Text = obj.autoCategoryID();
                lblMaterialID.Text = obj.autoMaterialID();
            }
        }

        protected void loadGrids()
        {
            // loading grid views
            // category
            DataSet dataSetCategory = obj.getAllCategories();
            gvCategories.DataSource = dataSetCategory;
            gvCategories.DataBind();

            // material
            DataSet dataSetMaterial = obj.getAllMaterials();
            gvMaterials.DataSource = dataSetMaterial;
            gvMaterials.DataBind();
        }

        protected void btnAddCategory_Click(object sender, EventArgs e)
        {
           
            string result = obj.insertCategory(lblCategoryID.Text, txtCategoryName.Text);

            int noRows = Convert.ToInt32(result);
            if (noRows > 0 ) 
            {
                lblTextCategory.Text = "The category was added successfully!";
            }
            else
            {
                lblTextCategory.Text = "There was an error adding the category.";
            }

            loadGrids();
        }

        protected void btnAddMaterial_Click(object sender, EventArgs e)
        {
             
            string result = obj.insertMaterial(lblMaterialID.Text, txtMaterialName.Text);

            int noRows = Convert.ToInt32(result);
            if (noRows > 0)
            {
                lblTextCategory.Text = "The material was added successfully!";
            }
            else
            {
                lblTextCategory.Text = "There was an error adding the material.";
            }

            loadGrids();
        }

        protected void btnSearchCategory_Click(object sender, EventArgs e)
        {
            DataSet dataSet;

            // if searching by name
            if (!string.IsNullOrEmpty(txtCategoryName.Text))
            {
                dataSet = obj.searchCategoryByName(txtCategoryName.Text);

            }
            else
            {
                // search by id
                dataSet = obj.searchCategoryByID(lblCategoryID.Text);
            }

            gvCategories.DataSource = dataSet;
            gvCategories.DataBind();
        }

        protected void btnSearchMaterial_Click(object sender, EventArgs e)
        {
            DataSet dataSet;

            // if searching by name
            if (!string.IsNullOrEmpty(txtMaterialName.Text))
            {
                dataSet = obj.searchMaterialByName(txtMaterialName.Text);

            }
            else
            {
                // search by id
                dataSet = obj.searchMaterialByID(lblMaterialID.Text);
            }

            gvMaterials.DataSource = dataSet;
            gvMaterials.DataBind();
        }
    }
}