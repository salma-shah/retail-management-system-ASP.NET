<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="SellerRequestsForm.aspx.cs" Inherits="CozyComfort_ClientApplication.SellerRequestsForm" %>

   <asp:Content ID="Content1"  ContentPlaceHolderID="MainContent" runat="server">

    <div class="navbar">
    <div class="nav-logo">CozyComfort Seller</div>
    <div class="nav-links">
        <a href="SellerDashboard.aspx">Dashboard</a>
        <a href="SellerOrderForm.aspx">Orders</a>
        <a href="SellerRequestsForm.aspx">Requests</a>
        <a href="SellerInventoryForm.aspx">Inventory</a>
        <a href="Logout.aspx">Logout</a>
    </div>
</div> 

       <h2>View All Requests Sent to Distributor</h2>
       <div>
           <asp:GridView ID="gvRequests" runat="server" CssClass="gridview"/>   
           <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
       </div>
   </asp:Content>