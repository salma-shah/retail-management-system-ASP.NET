<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="SellerDashboard.aspx.cs" Inherits="CozyComfort_ClientApplication.SellerDashboard" %>


<asp:Content ID="Content1"  ContentPlaceHolderID="MainContent" runat="server">
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
    <div class="nav-logo">CozyComfort Seller Dashboard</div>
    <div class="nav-links">
         <a href="SellerDashboard.aspx">Dashboard</a>
         <a href="SellerOrderForm.aspx">Orders</a>
        <a href="SellerRequestsForm.aspx">Requests</a>
        <a href="SellerInventoryForm.aspx">Inventory</a>
        <a href="Logout.aspx">Logout</a>
    </div>
</div> 
    <div>
        <table  class="form-table">
            <tr>
                <td>Seller ID:</td>
                <td>
                    <asp:Label ID="lblSellerID" runat="server" Text="Label" CssClass="label-as-textbox"></asp:Label></td>
            </tr>
            <tr>
                <td>Store Name: </td>
                <td>
                    <asp:Label ID="lblStoreName" runat="server" Text="Label"  CssClass="label-as-textbox"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Address:</td>
                <td>
                    <asp:Label ID="lblAddress" runat="server" Text="Label"  CssClass="label-as-textbox"></asp:Label></td>
            </tr>
            <tr>
                <td>Email: </td>
                <td>
                    <asp:Label ID="lblEmail" runat="server" Text="Label"  CssClass="label-as-textbox"></asp:Label></td>
            </tr>
            <tr>
                <td>Contact Number: </td>
                <td>
                    <asp:Label ID="lblContact" runat="server" Text="Label"  CssClass="label-as-textbox"></asp:Label></td>
            </tr>
        </table>
    </div>


    </asp:Content>