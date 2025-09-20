<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master"  CodeBehind="DistributorSellerRequestsForm.aspx.cs" Inherits="CozyComfort_ClientApplication.DistributorSellerRequestsForm" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <style>
         .gridview{
             width:max-content;
         }
     </style>
    <div class="navbar">
    <div class="nav-logo">CozyComfort Distributor Dashboard</div>
    <div class="nav-links">
         <a href="DistributorDashboard.aspx">Dashboard</a>
         <a href="DistributorSellerRequestsForm.aspx">Requests from Sellers</a>
        <a href="DistributorManufacturerRequestsForm.aspx">Requests to Manufacturer</a>
        <a href="DistributorInventoryForm.aspx">Inventory</a>
        <a href="Logout.aspx">Logout</a>
    </div>
</div> 
     <br />
     <br />
        <div class="form-container">         
            <table>
                <tr>
                    <td>ID: </td>
                    <td>
                        <asp:Label ID="lblRequestID" runat="server" Text="" CssClass="label-input"></asp:Label></td>
                </tr>                
                <tr>
                    <td>Blanket ID: </td>
                    <td>
                        <asp:DropDownList ID="dropListBlanket" runat="server"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>Quantity: </td>
                    <td>
                        <asp:TextBox ID="txtQty" runat="server" Width="75px"></asp:TextBox></td>
   <td><asp:RequiredFieldValidator ID="rfvQty" runat="server" ErrorMessage="Quantity must be specified." ControlToValidate="txtQty" ForeColor="#CC0000"></asp:RequiredFieldValidator></td>
                   
                </tr>
                  </table>
            <br />
                <div class="buttons">    
                        <asp:Button ID="btnRequest" runat="server" Text="Send Request to Cozy Comfort"  CssClass="btn-link" OnClick="btnRequest_Click" />
                    </div>  
            <br />
            <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
            <asp:GridView ID="gvRequests" runat="server" CssClass="gridview" AutoGenerateColumns="false" onRowCommand="gvRequests_RowCommand" DataKeyNames="RequestID, BlanketID,Quantity,AdditionalInfo">
                <Columns>
                    <asp:BoundField DataField="RequestID" HeaderText="Request ID" />
                    <asp:BoundField DataField="SellerID" HeaderText="Seller ID" />
                    <asp:BoundField DataField="Address" HeaderText="Address" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="ContactNumber" HeaderText="Contact" />
                    <asp:BoundField DataField="BlanketID" HeaderText="Blanket ID" />
                    <asp:BoundField DataField="BlanketName" HeaderText="Blanket Name" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                    <asp:BoundField DataField="RequestDate" HeaderText="Requested Date" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <div class="buttons">
                                <asp:Button ID="btnCheckStock" runat="server" Text="Check Stock" CssClass="btn-link" CommandName="checkStock" CommandArgument='<%# Container.DataItemIndex %>'/>
                                <asp:Button ID="btnAvailable" runat="server" Text="Mark as Available" CssClass="btn-link" CommandName="markAvailable" CommandArgument='<%# Container.DataItemIndex %>'/>
                                 <asp:Button ID="btnUnavailable" runat="server" Text="Mark as Unavailable" CssClass="btn-link" CommandName="markUnavailable" CommandArgument='<%# Container.DataItemIndex %>'/>
                            </div>
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txtInfo" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>                      
                </Columns>
            </asp:GridView>
        </div>
   </asp:Content>
