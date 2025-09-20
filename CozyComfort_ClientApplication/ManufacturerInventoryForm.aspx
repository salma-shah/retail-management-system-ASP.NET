<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManufacturerInventoryForm.aspx.cs" Inherits="CozyComfort_ClientApplication.SearchAndUpdateForm" %>

   <asp:Content ID="Content1"  ContentPlaceHolderID="MainContent" runat="server">
       <style>
    .gridview {
       width:max-content;
    }
</style>

    <div class="navbar">
    <div class="nav-logo">CozyComfort Manufacturer Inventory</div>
    <div class="nav-links">
         <a href="ManufacturerDashboard.aspx">Dashboard</a>
        <a href="ManufacturerBlanketForm.aspx">Blankets</a>
        <a href="CategoryAndMaterialForm.aspx">Models & Materials</a>
        <a href="ManufacturerRequestsForm.aspx">Requests</a>
        <a href="ManufacturerInventoryForm.aspx">Inventory</a>
        <a href="Logout.aspx">Logout</a>
    </div>
</div> 
       <br />
       <br />
       <div class="form-container">
        <div>
                     <h1>Manage Inventory</h1>
            <table>
    <tr>
        <td>Blanket ID: </td>
        <td>
            <asp:TextBox ID="txtBlanketID" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Blanket Name: </td>
        <td>
            <asp:TextBox ID="txtBlanketName" runat="server"></asp:TextBox></td>
    </tr>
     <tr>
         <td>Stock: </td>
         <td>
             <asp:TextBox ID="txtStock" runat="server"></asp:TextBox></td>
     </tr>           
    <tr>
        <td>Size: </td>
        <td>
            <asp:DropDownList ID="dropListSizes" runat="server">
                 <asp:ListItem>Queen</asp:ListItem>
                <asp:ListItem>King</asp:ListItem>
                <asp:ListItem>Double</asp:ListItem>
                <asp:ListItem>Lapghan</asp:ListItem>
                <asp:ListItem>Twin</asp:ListItem>
                <asp:ListItem>Lovey</asp:ListItem>
                <asp:ListItem>Crib</asp:ListItem>
                <asp:ListItem>Throw</asp:ListItem>
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td>Material: </td>
        <td>
            <asp:DropDownList ID="dropListMaterial" runat="server"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>Category: </td>
        <td>
            <asp:DropDownList ID="dropListCategory" runat="server"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>Price: </td>
        <td>
            <asp:TextBox ID="txtPrice" runat="server"></asp:TextBox></td>
    </tr>
     <tr>
         <td>Colour: </td>
         <td>
             <asp:TextBox ID="txtColour" runat="server"></asp:TextBox></td>
     </tr> 
     <tr>
         <td>Description: </td>
         <td>
             <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"></asp:TextBox></td>
     </tr>           
</table>
            <div class="buttons">
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn-link" />
                <asp:Button ID="btnAddStock" runat="server" Text="Add Stock" CssClass="btn-link" OnClick="btnAddStock_Click"/>
                <asp:Button ID="btnDecreaseStock" runat="server" CssClass="btn-link" Text="Reduce Stock" OnClick="btnDecreaseStock_Click" />
                </div>
<br />

<asp:Label ID="lblText" runat="server" Text=""></asp:Label>
<asp:GridView ID="gvBlankets" runat="server" AutoGenerateColumns="true" CssClass="gridview"></asp:GridView>
        </div>
           </div>
     </asp:Content>

