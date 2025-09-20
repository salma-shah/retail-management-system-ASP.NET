<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master"  CodeBehind="CategoryAndMaterialForm.aspx.cs" Inherits="CozyComfort_ClientApplication.Category_MaterialForm" %>

   <asp:Content ID="Content1"  ContentPlaceHolderID="MainContent" runat="server">

    <div class="navbar">
    <div class="nav-logo">CozyComfort Manufacturer Models & Materials</div>
    <div class="nav-links">
         <a href="ManufacturerDashboard.aspx">Dashboard</a>
        <a href="ManufacturerBlanketForm.aspx">Blankets</a>
        <a href="Category&MaterialForm.aspx">Models & Materials</a>
          <a href="ManufacturerRequestsForm.aspx">Requests</a>
        <a href="ManufacturerInventoryForm.aspx">Inventory</a>
        <a href="Logout.aspx">Logout</a>
    </div>
</div> 
       <div class="form-container-cat-mat">

       <div class="form-box">

            <h2>Add a Category Model!</h2>
           <table class="form-table">
     <tr>
         <td>Category ID:</td>
         <td>
             <asp:Label ID="lblCategoryID" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
         <td>Category Name: </td>
         <td>
             <asp:TextBox ID="txtCategoryName" runat="server"></asp:TextBox></td>
     </tr>
 </table>
           <div class="buttons">
              <asp:Button ID="btnAddCategory" runat="server" Text="Add" CssClass="btn-link" OnClick="btnAddCategory_Click" />
              <asp:Button ID="btnSearchCategory" runat="server" Text="Search" CssClass="btn-link" OnClick="btnSearchCategory_Click" /></div>
              <asp:Label ID="lblTextCategory" runat="server" Text=""></asp:Label>
              <asp:GridView ID="gvCategories" runat="server" CssClass="gridview"></asp:GridView>  
           </div>
           <br />
            <br />
           <div class="form-box">
     <h2>Add a Material!</h2>
           
     <table class="form-table">
         <tr>
             <td>Material ID:</td>
             <td>
                 <asp:Label ID="lblMaterialID" runat="server" Text="Label"></asp:Label></td>
         </tr>
         <tr>
             <td>Material Name: </td>
             <td>
                 <asp:TextBox ID="txtMaterialName" runat="server"></asp:TextBox></td>
         </tr>
     </table>
           <div class="buttons">
     <asp:Button ID="btnAddMaterial" runat="server" Text="Add" CssClass="btn-link" OnClick="btnAddMaterial_Click" />
      <asp:Button ID="btnSearchMaterial" runat="server" Text="Search" CssClass="btn-link" OnClick="btnSearchMaterial_Click" /></div>
     <asp:Label ID="lblTextMaterial" runat="server" Text=""></asp:Label>
     <asp:GridView ID="gvMaterials" runat="server"  CssClass="gridview"></asp:GridView>
 </div>
      </div>
         
       </asp:Content>