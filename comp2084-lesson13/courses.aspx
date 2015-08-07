<%@ Page Title="Courses" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="courses.aspx.cs" Inherits="comp2084_lesson13.courses" %>

<%@ Register Src="~/auth.ascx" TagPrefix="uc1" TagName="auth" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:auth runat="server" ID="auth" />
    <h1>Courses</h1>

    <a href="course-details.aspx">Add Course</a>

    <asp:GridView ID="grdCourses" runat="server" AutoGenerateColumns="false" 
        CssClass="table table-striped table-hover">
        <Columns>
            <asp:BoundField DataField="CourseID" HeaderText="Course ID" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="Credits" HeaderText="Credits" />
            <asp:BoundField DataField="Department.Name" HeaderText="Department" />
            <asp:HyperLinkField HeaderText="Edit" NavigateUrl="course-details.aspx" Text="Edit"
             DataNavigateUrlFormatString="course-details.aspx?CourseID={0}" DataNavigateUrlFields="CourseID"/>
        </Columns>
    </asp:GridView>
</asp:Content>
