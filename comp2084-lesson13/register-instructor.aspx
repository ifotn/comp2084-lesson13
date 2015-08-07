<%@ Page Title="Instructor Registration" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="register-instructor.aspx.cs" Inherits="comp2084_lesson13.register_instructor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Instructor Registration</h1>
    <h5>All fields are required</h5>

    <fieldset>
        <label for="txtFirstName" class="col-sm-2">First Name:</label>
        <asp:textbox id="txtFirstName" runat="server" required maxlength="50" />
    </fieldset>
    <fieldset>
        <label for="txtLastName" class="col-sm-2">Last Name:</label>
        <asp:textbox id="txtLastName" runat="server" required maxlength="50" />
    </fieldset>
    <fieldset>
        <label for="txtUsername" class="col-sm-2">Username:</label>
        <asp:textbox id="txtUsername" runat="server" required maxlength="50" />
    </fieldset>
    <fieldset>
        <label for="txtPassword" class="col-sm-2">Password:</label>
        <asp:textbox id="txtPassword" runat="server" required maxlength="50" textmode="password" />
    </fieldset>
    <fieldset>
        <label for="txtConfirm" class="col-sm-2">Confirm:</label>
        <asp:textbox id="txtConfirm" runat="server" required maxlength="50" textmode="password" />
        <asp:comparevalidator runat="server" controltovalidate="txtPassword" controltocompare="txtConfirm"
            cssclass="label label-danger" errormessage="Passwords must match" operator="Equal" />
    </fieldset>
    <fieldset>
        <label for="ddlDepartment" class="col-sm-2">Department:</label>
        <asp:dropdownlist id="ddlDepartment" runat="server" datatextfield="Name" 
            datavaluefield="DepartmentID" />
    </fieldset>
    <div class="col-sm-offset-2">
        <asp:button id="btnRegister" runat="server" text="Register" cssclass="btn btn-primary" 
            OnClick="btnRegister_Click" />
    </div>
</asp:Content>
