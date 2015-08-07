using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//reference our db model
using comp2084_lesson13.Models;

namespace comp2084_lesson13
{
    public partial class course_details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if loading for the first time, fill the department dropdown
                GetDepartments();

                //check for a courseID; if found, populate the selected course
                if (!String.IsNullOrEmpty(Request.QueryString["CourseID"]))
                {
                    GetCourse();
                    pnlEnrollments.Visible = true;
                }
                else
                {
                    //adding a new course, so hide the enrollments as we don't have any yet
                    pnlEnrollments.Visible = false;
                }
            }
        }

        protected void GetCourse()
        {
            //connect
            using (DefaultConnection db = new DefaultConnection())
            {
                //Get the selected courseID from the url
                Int32 CourseID = Convert.ToInt32(Request.QueryString["CourseID"]);

                //query the db
                Course objC = (from c in db.Courses
                               where c.CourseID == CourseID
                               select c).FirstOrDefault();

                //populate the form
                txtTitle.Text = objC.Title;
                txtCredits.Text = objC.Credits.ToString();
                ddlDepartment.SelectedValue = objC.DepartmentID.ToString();

                //populate student enrollments grid
                var Enrollments = from en in db.Enrollments
                                  where en.CourseID == CourseID
                                  orderby en.Student.LastName, en.Student.FirstMidName
                                  select en;

                //bind to the grid
                grdEnrollments.DataSource = Enrollments.ToList();
                grdEnrollments.DataBind();

            }
        }

        protected void GetDepartments()
        {
            //connect
            using (DefaultConnection db = new DefaultConnection())
            {
                //get the department list
                var Departments = from d in db.Departments
                                  orderby d.Name
                                  select d;

                //bind to the dropdown list
                ddlDepartment.DataSource = Departments.ToList();
                ddlDepartment.DataBind();

                //add a default option to the dropdown after we fill it
                ListItem DefaultItem = new ListItem("-Select-", "0");
                ddlDepartment.Items.Insert(0, DefaultItem);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //connect
            using (DefaultConnection db = new DefaultConnection())
            {
                //create a new course and fill the properties
                Course objC = new Course();

                objC.Title = txtTitle.Text;
                objC.Credits = Convert.ToInt32(txtCredits.Text);
                objC.DepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);

                //save
                db.Courses.Add(objC);
                db.SaveChanges();

                //redirect
                Response.Redirect("courses.aspx");

            }
        }
    }
}