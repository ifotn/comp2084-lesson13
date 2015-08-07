using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//reference the db model
using comp2084_lesson13.Models;

namespace comp2084_lesson13
{
    public partial class courses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetCourses();
            }
        }

        protected void GetCourses()
        {
            using (DefaultConnection db = new DefaultConnection())
            {
                var Courses = from c in db.Courses
                              select c;

                grdCourses.DataSource = Courses.ToList();
                grdCourses.DataBind();
            }
        }
    }
}