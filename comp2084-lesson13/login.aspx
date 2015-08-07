<%@ Page Title="Login" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="comp2084_lesson13.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Log In</h1>

    <div>
        <asp:label id="lblError" runat="server" cssclass="label label-danger" />
    </div>
     <fieldset>
        <label for="txtUsername" class="col-sm-2">Username:</label>
        <asp:textbox id="txtUsername" runat="server" required maxlength="50" />
    </fieldset>
    <fieldset>
        <label for="txtPassword" class="col-sm-2">Password:</label>
        <asp:textbox id="txtPassword" runat="server" required maxlength="50" textmode="password" />
    </fieldset>
    <div class="col-sm-2">
        <asp:button id="btnLogin" runat="server" text="Login" cssclass="btn btn-primary" 
            OnClick="btnLogin_Click" />
    </div>

</asp:Content>
