using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace comp2084_lesson13
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
        }

        //application error here
        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    // An error has occured on a .Net page.
        //    var serverError = Server.GetLastError() as HttpException;

        //    if (null != serverError)
        //    {
        //        int errorCode = serverError.GetHttpCode();

        //        if (404 == errorCode)
        //        {
        //            Server.ClearError();
        //            Server.Transfer("/404.aspx");
        //        }
        //    }
        //}
    }
}