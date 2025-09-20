<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="DistributorManufacturerRequestsForm.aspx.cs" Inherits="CozyComfort_ClientApplication.DistributorManufacturerRequestsForm" %>
 <asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="navbar">
    <div class="nav-logo">CozyComfort Distributor Requests</div>
    <div class="nav-links">
        <a href="DistributorDashboard.aspx">Dashboard</a>
        <a href="DistributorSellerRequestsForm.aspx">Requests from Sellers</a>
        <a href="DistributorManufacturerRequestsForm.aspx">Requests to Manufacturer</a>
        <a href="DistributorInventoryForm.aspx">Inventory</a>
        <a href="Logout.aspx">Logout</a>
    </div>
</div> 

     <div>
         <h2>Overview of Requests sent to Manufacturer</h2>
         <h3>View the requests sent to Cozy Comfort and their responses.</h3>
         <br />
         <br />
         <asp:GridView ID="gvRequests" runat="server" CssClass="gridview">
                
            </asp:GridView>
     </div>
     </asp:Content>
