using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace comp2084_lesson13
{
    public partial class auth : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["InstructorID"] == null)
            {
                Response.Redirect("logout.aspx");
            }
        }
    }
}