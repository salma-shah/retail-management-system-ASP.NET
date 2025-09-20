<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SellerOrderForm.aspx.cs" Inherits="CozyComfort_ClientApplication.SellerOrderForm" %>

   <asp:Content ID="Content1"  ContentPlaceHolderID="MainContent" runat="server">
       <style>
           .gridview{
               width:max-content;
           }
       </style>
    <div class="navbar">
    <div class="nav-logo">CozyComfort Seller Orders</div>
    <div class="nav-links">
        <a href="SellerDashboard.aspx">Dashboard</a>
        <a href="SellerOrderForm.aspx">Orders</a>
        <a href="SellerRequestsForm.aspx">Requests</a>
        <a href="SellerInventoryForm.aspx">Inventory</a>
        <a href="Logout.aspx">Logout</a>
    </div>
</div> 

       <br />
       <br />
       <br />
        <div class="form-container"> 
            <h2>Place orders and check availability.</h2>
            <br>
            <br />

            <table>
                <tr>
                    <td>Order ID: </td>
                    <td>
                        <asp:Label ID="lblOrderID" runat="server" Text="" CssClass="label-input"></asp:Label></td>
                    
                </tr>                
                <tr>
                    <td>Blanket: </td>
                    <td>
                        <asp:DropDownList ID="dropListBlanket" runat="server"></asp:DropDownList></td>
                    
                </tr>
                <tr>
                    <td>Quantity: </td>
                    <td>
                        <asp:TextBox ID="txtQty" runat="server" Width="75px"></asp:TextBox></td>
                                <td>
    <asp:RequiredFieldValidator ID="rfvQty" runat="server" ErrorMessage="Quantity must be specified." ControlToValidate="txtQty" ForeColor="#CC0000"></asp:RequiredFieldValidator></td>
                </tr>
                  </table>
            <br />
                <div class="buttons">    
                        <asp:Button ID="btnOrder" runat="server" Text="Make Order" OnClick="btnOrder_Click" CssClass="btn-link" />
                    </div>  
            <br />
            <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
            <asp:GridView ID="gvOrders" runat="server" CssClass="gridview" AutoGenerateColumns="false" onRowCommand="gvOrders_RowCommand" DataKeyNames="OrderID">
                <Columns>
                    <asp:BoundField DataField="OrderID" HeaderText="Order ID" />                  
                    <asp:BoundField DataField="BlanketID" HeaderText="Blanket ID" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                    <asp:BoundField DataField="TotalCost" HeaderText="Total Cost" />
                    <asp:BoundField DataField="OrderDate" HeaderText="Order Date" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <div class="buttons">
                                <asp:Button ID="btnAcceptOrder" runat="server" Text="Accept Order" CssClass="btn-link" CommandName="acceptOrder" CommandArgument='<%# Container.DataItemIndex %>'/>
                                                         </div>
                        </ItemTemplate>
                    </asp:TemplateField>                      
                </Columns>
                </asp:GridView>
        </div>
   </asp:Content>
