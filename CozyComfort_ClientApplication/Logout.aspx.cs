using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CozyComfort_ClientApplication
{
    public partial class Logout : System.Web.UI.Page
    {
        UserWebService.UserWebServiceSoapClient obj = new UserWebService.UserWebServiceSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            string result = obj.logout(); 
            if (result == "LoggedOut")
            {
                Response.Redirect("index.html");
            }
        }
    }
}