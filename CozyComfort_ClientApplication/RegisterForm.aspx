<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterForm.aspx.cs" Inherits="CozyComfort_ClientApplication.RegisterForm" %>


  <asp:Content ID="Content1"  ContentPlaceHolderID="MainContent" runat="server">
  <style>
    body {
        display: flex;
        justify-content: center;
        align-items: center;
        height: 100vh;
        background-color: #f9f0f0;
    }
</style>
       <div class="reg-container">
            <h2>Register for Cozy Comfort now!</h2>
            <table class="reg-table">
                <tr>
                    <td> User ID: </td>
                   <td> <asp:Label ID="lblUserID" runat="server" Text=""></asp:Label></td>
                </tr>
               <tr>
                   <td> Store name: </td>
                   <td>
                       <asp:TextBox ID="txtStoreName" runat="server" Width="150px"></asp:TextBox>
                   </td>
                   <td>
                       <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="You must enter the name of your store." ControlToValidate="txtStoreName" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                   </td>
               </tr>
                <tr>
                    <td> Contact: </td>
                    <td>
                        <asp:TextBox ID="txtContact" runat="server" Width="150px"></asp:TextBox>
                    </td>
                     <td>
                         <asp:RequiredFieldValidator ID="rfvContact" runat="server" ErrorMessage="You must enter your contact number." ControlToValidate="txtContact" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                     </td>
                </tr>
                <tr>
                    <td>Address: </td>
                    <td>
                        <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                    <td>  <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ErrorMessage="You must enter the address." ControlToValidate="txtAddress" ForeColor="#CC0000"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td> Email: </td>
                    <td>
                         <asp:TextBox ID="txtEmail" runat="server" Width="150px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="You must enter your email." ControlToValidate="txtEmail" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ForeColor="#CC0000" ErrorMessage="Please enter a valid email." ValidationExpression="[\w\.]+@([\w-]+\.)+[\w-]{2,4}$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr> 
                    <td> Password:     </td>
                    <td> 
                        <asp:TextBox ID="txtPassword" runat="server" Width="150px" TextMode="Password"></asp:TextBox>
                    </td>
                     <td>
                         <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="You must enter a password." ControlToValidate="txtPassword" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                     </td>
                    <td>
                        <asp:RegularExpressionValidator ID="revPassword" runat="server" ControlToValidate="txtPassword" ForeColor="#CC0000" ErrorMessage="Password needs to be at least 8 characters long." ValidationExpression="^[\s\S]{8,}$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>Role: </td>
                    <td><asp:RadioButton ID="radioBtnSeller" runat="server"  Text="Seller" GroupName="role"/></td>
                    <td><asp:RadioButton ID="radioBtnDistributor" runat="server"  Text="Distributor" GroupName="role" /></td>
                    <td><asp:RadioButton ID="radioBtnManufacturer" runat="server"  Text="Manufacturer" GroupName="role"/></td>
                    <td>
                        <asp:CustomValidator ID="cvRole" runat="server" ErrorMessage="You need to select a role." OnServerValidate="cvRole_serverValidate" Display="Dynamic" ForeColor="#CC0000"></asp:CustomValidator>
                    </td>
                </tr>
                </table>
             <div class="buttons">
            <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" CssClass="btn-link"/></div>
           <br />
            <asp:Label ID="lblText" runat="server" Text="" ></asp:Label>
        </div>
 
        </asp:Content>
