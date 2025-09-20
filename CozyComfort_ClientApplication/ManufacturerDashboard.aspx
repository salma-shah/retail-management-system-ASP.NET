<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ManufacturerDashboard.aspx.cs" Inherits="CozyComfort_ClientApplication.ManufacturerDashboard1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .label-as-textbox {
    display: inline-block;
    width: 250px;             
    padding: 8px 10px;
    border: 1px solid #ccc;
    border-radius: 5px;
    background-color: #fafafa;
    font-size: 14px;
    font-family: 'Poppins', sans-serif;
    color: #333;
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

<div>
    <table class="form-table">
        <tr>
            <td>Manufacturer ID: </td>
            <td>
                <asp:Label ID="lblManufacturerID" runat="server" Text="" CssClass="label-as-textbox"></asp:Label></td>
        </tr>
        <tr>
            <td>Organization Name: </td>
            <td>
                <asp:Label ID="lblOrgName" runat="server" Text="" CssClass="label-as-textbox"></asp:Label></td>
        </tr>
        <tr>
            <td>Address: </td>
            <td>
                <asp:Label ID="lblAddress" runat="server" Text="" CssClass="label-as-textbox"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Email: </td>
            <td>
                <asp:Label ID="lblEmail" runat="server" Text="" CssClass="label-as-textbox"></asp:Label></td>
        </tr>
        <tr>
            <td>Contact Number: </td>
            <td>
                <asp:Label ID="lblContact" runat="server" Text="" CssClass="label-as-textbox"></asp:Label></td>
        </tr>
    </table>
</div>
</asp:Content>