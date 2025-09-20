<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManufacturerBlanketForm.aspx.cs" Inherits="CozyComfort_ClientApplication.ManufacturerBlanketForm" %>

   <asp:Content ID="Content1"  ContentPlaceHolderID="MainContent" runat="server">

    <div class="navbar">
    <div class="nav-logo">CozyComfort Manufacturer Blankets</div>
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
       <br />
        <div>
            <table>
                <tr>
                    <td>Blanket ID: </td>
                    <td>
                      <asp:TextBox ID="txtBlanketID" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Blanket Name: </td>
                    <td>
                        <asp:TextBox ID="txtBlanketName" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Description: </td>
                    <td>
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Category: </td>
                    <td>
                        <asp:DropDownList ID="dropListCategory" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Material: </td>
                    <td>
                        <asp:DropDownList ID="dropListMaterial" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Size: </td>
                    <td>
                        <asp:DropDownList ID="dropListSize" runat="server">
                            <asp:ListItem>Queen</asp:ListItem>
                            <asp:ListItem>King</asp:ListItem>
                            <asp:ListItem>Double</asp:ListItem>
                            <asp:ListItem>Lapghan</asp:ListItem>
                            <asp:ListItem>Twin</asp:ListItem>
                            <asp:ListItem>Lovey</asp:ListItem>
                            <asp:ListItem>Cradle</asp:ListItem>
                            <asp:ListItem>Crib</asp:ListItem>
                            <asp:ListItem>Throw</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Colour: </td>
                    <td>
                        <asp:TextBox ID="txtColour" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Price: </td>
                    <td>
                        <asp:TextBox ID="txtPrice" runat="server" Width="103px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Quantity: </td>
                    <td>
                        <asp:TextBox ID="txtQty" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Production Capacity: </td>
                    <td>
                        <asp:TextBox ID="txtProductionCapacity" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Lead Time: </td>
                    <td>
                        <asp:TextBox ID="txtLeadTime" runat="server"></asp:TextBox></td>
                </tr>
            </table>
            <br>
            <div class="buttons">
                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" CssClass="btn-link"/>
                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn-link" OnClick="btnUpdate_Click"/>
                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn-link" OnClick="btnDelete_Click" />
                </div>
            <br />
            <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
        </div>
  </asp:Content>
