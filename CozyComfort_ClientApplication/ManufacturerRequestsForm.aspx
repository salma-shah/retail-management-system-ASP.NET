<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master"  CodeBehind="ManufacturerRequestsForm.aspx.cs" Inherits="CozyComfort_ClientApplication.ManufacturerRequestsForm" %>

   <asp:Content ID="Content1"  ContentPlaceHolderID="MainContent" runat="server">

    <div class="navbar">
    <div class="nav-logo">CozyComfort Manufacturer Requests</div>
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
           <h2>View Requests from All Distributors</h2><br/>
           <h3>Check production capacity and lead times for each request.</h3>
           <br />
           <br />
            <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
           <br />
           <br />

           <asp:GridView ID="gvRequests" runat="server" CssClass="gridview"  AutoGenerateColumns="false" onRowCommand="checkStock" DataKeyNames="RequestID,BlanketID,Quantity">
               <Columns>
                   <asp:BoundField DataField="RequestID" HeaderText="Request ID" />
                    <asp:BoundField DataField="DistributorID" HeaderText="Distributor ID" />
                   <asp:BoundField DataField="Address" HeaderText="Address" />
                   <asp:BoundField DataField="Email" HeaderText="Email" />
                   <asp:BoundField DataField="ContactNumber" HeaderText="Contact" />
                    <asp:BoundField DataField="BlanketID" HeaderText="Blanket ID" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                    <asp:BoundField DataField="RequestDate" HeaderText="Requested Date" />
                   <asp:TemplateField>
                       <ItemTemplate>
                           <div class="buttons">
                               <asp:Button ID="btnCheck" runat="server" Text="Check" CssClass="btn-link" 
                                   CommandName="checkStock" CommandArgument='<%# Container.DataItemIndex %>' />
                           </div>
                       </ItemTemplate>
                   </asp:TemplateField>
               </Columns>
           </asp:GridView>
           <br />
          
       </div>
 </asp:Content>
