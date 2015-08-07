using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//reference db model so we can connect to sql server
using comp2084_lesson13.Models;

namespace comp2084_lesson13
{
    public partial class department_details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if loading for the first time (not posting back), check for a url
            if (!IsPostBack)
            {
                //if we have an ID in the url, look up the selected record
                if (!String.IsNullOrEmpty(Request.QueryString["DepartmentID"]))
                {
                    GetDepartment();
                }
            }
        }

        protected void GetDepartment()
        {
            //look up the selected department and fill the form
            using (DefaultConnection db = new DefaultConnection())
            {
                //store the id from the url in a variable
                Int32 DepartmentID = Convert.ToInt32(Request.QueryString["DepartmentID"]);

                //look up the department
                Department dep = (from d in db.Departments
                                  where d.DepartmentID == DepartmentID
                                  select d).FirstOrDefault();

                //pre-populate the form fields
                txtName.Text = dep.Name;
                txtBudget.Text = dep.Budget.ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //connect
            using (DefaultConnection db = new DefaultConnection())
            {
                //create a new department in memory
                Department dep = new Department();

                Int32 DepartmentID = 0;

                //check for a url
                if (!String.IsNullOrEmpty(Request.QueryString["DepartmentID"]))
                {
                    //get the id from the url
                    DepartmentID = Convert.ToInt32(Request.QueryString["DepartmentID"]);

                    //look up the department
                    dep = (from d in db.Departments
                           where d.DepartmentID == DepartmentID
                           select d).FirstOrDefault();
                }

                //fill the properties of the new department
                dep.Name = txtName.Text;
                dep.Budget = Convert.ToDecimal(txtBudget.Text);

                //add if we have no id in the url
                if (DepartmentID == 0)
                {
                    db.Departments.Add(dep);
                }

                //save the new department
                db.SaveChanges();

                //redirect to the departments list page
                Response.Redirect("departments.aspx");
            }
        }
    }
}