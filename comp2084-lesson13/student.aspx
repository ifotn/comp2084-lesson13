<%@ Page Title="Student Details" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="student.aspx.cs" Inherits="comp2007_lesson6_wed.student" %>

<%@ Register Src="~/auth.ascx" TagPrefix="uc1" TagName="auth" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:auth runat="server" ID="auth" />
    <h1>Student Details</h1>
    <h5>All fields are required</h5>

    <fieldset>
        <label for="txtLastName" class="col-sm-2">Last Name:</label>
        <asp:TextBox ID="txtLastName" runat="server" required MaxLength="50" />
    </fieldset>
    <fieldset>
        <label for="txtFirstMidName" class="col-sm-2">First Name:</label>
        <asp:TextBox ID="txtFirstMidName" runat="server" required MaxLength="50" />
    </fieldset>
    <fieldset>
        <label for="txtEnrollmentDate" class="col-sm-2">Enrollment Date:</label>
        <asp:TextBox ID="txtEnrollmentDate" runat="server" required TextMode="Date" />
        <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Must be a Date"
            ControlToValidate="txtEnrollmentDate" CssClass="alert alert-danger"
            Type="Date" MinimumValue="2000-01-01" MaximumValue="12/31/2999"></asp:RangeValidator>
    </fieldset>

    <div class="col-sm-offset-2">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"
             OnClick="btnSave_Click" />
    </div>

   <h2>Courses</h2>

    <asp:UpdatePanel ID="updGrid" runat="server">
        <ContentTemplate>
            <asp:gridview ID="grdCourses" runat="server" AutoGenerateColumns="false"
                    DataKeyNames="EnrollmentID" OnRowDeleting="grdCourses_RowDeleting"
                    CssClass="table table-striped table-hover">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Department" />
                    <asp:BoundField DataField="Title" HeaderText="Course" />
                    <asp:BoundField DataField="Grade" HeaderText="Grade" />
                    <asp:CommandField ShowDeleteButton="true" DeleteText="Delete" HeaderText="Delete" />
                </Columns>
            </asp:gridview>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="updCourse" runat="server">
        <ContentTemplate>
            <table class="table table-striped table-hover">
                <thead>
                    <th>Department</th>
                    <th>Course</th>
                    <th>Add</th>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlDepartment" runat="server"
                                DataTextField="Name" DataValueField="DepartmentID"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RangeValidator ControlToValidate="ddlDepartment" runat="server"
                                Type="Integer" MinimumValue="1" MaximumValue="999999"
                                ErrorMessage="Required" CssClass="label label-danger" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCourse" runat="server"
                                DataTextField="Title" DataValueField="CourseID">
                            </asp:DropDownList>
                            <asp:RangeValidator ControlToValidate="ddlCourse" runat="server"
                                Type="Integer" MinimumValue="1" MaximumValue="999999"
                                ErrorMessage="Required" CssClass="label label-danger" />
                        </td>

                        <td>
                            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary"
                                 OnClick="btnAdd_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
