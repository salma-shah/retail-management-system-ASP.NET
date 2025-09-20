<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LoginForm.aspx.cs" Inherits="CozyComfort_ClientApplication.LoginForm" %>

    
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

        <div class="login-container">
            <h2>Login into your account.</h2>
            <table class="login-table">
                <tr>
                    <td>Email: </td>
                <td><asp:TextBox ID="txtEmail" runat="server" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        Password: 
                    </td>
                    <td> <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                </tr>
            </table>
            <div class="buttons">
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="btn-link" />
            </div><br />
            <asp:Label ID="lblText" runat="server" Text="" CssClass="error-text"></asp:Label>
        </div>
        </asp:Content>
        
   
