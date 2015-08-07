using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//model references for EF
using comp2084_lesson13.Models;
using System.Web.ModelBinding;

namespace comp2007_lesson6_wed
{
    public partial class student : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if save wasn't clicked AND we have a StudentID in the url
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                GetStudent();

                //fill the department dropdown
                GetDepartments();
            }
        }

        protected void GetStudent()
        {
            try
            {
                //populate form with existing student record
                Int32 StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);

                //connect to db via EF
                using (DefaultConnection db = new DefaultConnection())
                {
                    //populate a student instance with the StudentID from the URL parameter
                    Student s = (from objS in db.Students
                                 where objS.StudentID == StudentID
                                 select objS).FirstOrDefault();

                    //map the student properties to the form controls if we found a match
                    if (s != null)
                    {
                        txtLastName.Text = s.LastName;
                        txtFirstMidName.Text = s.FirstMidName;
                        txtEnrollmentDate.Text = s.EnrollmentDate.ToString("yyyy-MM-dd");
                    }
                }

                //fill enrollments grid
                GetEnrollments();
            }
            catch (Exception e)
            {
                //redirect
            }
        }

        protected void GetEnrollments()
        {
            //connect
            using (DefaultConnection db = new DefaultConnection())
            {

                //get student id from url
                Int32 StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);

                //enrollments - this code goes in the same method that populates the student form but below the existing code that's already in GetStudent()             
                var objE = (from en in db.Enrollments
                            join c in db.Courses on en.CourseID equals c.CourseID
                            join d in db.Departments on c.DepartmentID equals d.DepartmentID
                            where en.StudentID == StudentID
                            select new { en.EnrollmentID, en.Grade, c.Title, d.Name });

                grdCourses.DataSource = objE.ToList();
                grdCourses.DataBind();
            }

        }

        protected void grdCourses_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //get selected record id
            Int32 EnrollmentID = Convert.ToInt32(grdCourses.DataKeys[e.RowIndex].Values["EnrollmentID"]);

            using (DefaultConnection db = new DefaultConnection())
            {
                //get selected record
                Enrollment objE = (from en in db.Enrollments
                                   where en.EnrollmentID == EnrollmentID
                                   select en).FirstOrDefault();

                //delete
                db.Enrollments.Remove(objE);
                db.SaveChanges();

                //refresh the data on the page
                GetStudent();

                //reattach js delete confirmation
            }
        }

        protected void GetDepartments()
        {
            //connect
            using (DefaultConnection db = new DefaultConnection())
            {
                var deps = from d in db.Departments
                           orderby d.Name
                           select d;

                //fill dropdown
                ddlDepartment.DataSource = deps.ToList();
                ddlDepartment.DataBind();

                //add a default option to each dropdown
                ListItem newItem = new ListItem("-Select-", "0");
                ddlDepartment.Items.Insert(0, newItem);
                ddlCourse.Items.Insert(0, newItem);
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //use EF to connect to SQL Server
                using (DefaultConnection db = new DefaultConnection())
                {

                    //use the Student model to save the new record
                    Student s = new Student();
                    Int32 StudentID = 0;

                    //check the querystring for an id so we can determine add / update
                    if (Request.QueryString["StudentID"] != null)
                    {
                        //get the id from the url
                        StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);

                        //get the current student from EF
                        s = (from objS in db.Students
                             where objS.StudentID == StudentID
                             select objS).FirstOrDefault();
                    }

                    s.LastName = txtLastName.Text;
                    s.FirstMidName = txtFirstMidName.Text;
                    s.EnrollmentDate = Convert.ToDateTime(txtEnrollmentDate.Text);

                    //call add only if we have no student ID
                    if (StudentID == 0)
                    {
                        db.Students.Add(s);
                    }

                    //run the update or insert
                    db.SaveChanges();

                    //redirect to the updated students page
                    Response.Redirect("students.aspx");
                }
            }
            catch (Exception ex)
            {
                //redirect
            }

        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            //get the selected DepartmentID
            Int32 DepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);

            //connect
            using (DefaultConnection db = new DefaultConnection())
            {
                var objC = from c in db.Courses
                           where c.DepartmentID == DepartmentID
                           orderby c.Title
                           select c;

                //bind query result to the course dropdown
                ddlCourse.DataSource = objC.ToList();
                ddlCourse.DataBind();

                //put the Select option back at the top of the list
                ListItem newItem = new ListItem("-Select-", "0");
                ddlCourse.Items.Insert(0, newItem);
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //connect
            using (DefaultConnection db = new DefaultConnection())
            {
                //instantiate a new enrollment
                Enrollment objE = new Enrollment();

                objE.StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);
                objE.CourseID = Convert.ToInt32(ddlCourse.SelectedValue);
                
                //save
                db.Enrollments.Add(objE);
                db.SaveChanges();

                //refresh courses grid
                GetEnrollments();

                //reset the dropdowns
                ddlDepartment.ClearSelection();
                ddlCourse.ClearSelection();

            }
        }
    }
}