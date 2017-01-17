using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;
using System.Configuration;

namespace ACHE.Mvc.admin
{
    public partial class admin_default : AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CurrentUser = null;
        }

        protected void Login(object sender, EventArgs e)
        {
            if (txtUsuario.Text == ConfigurationManager.AppSettings["Admin.User"] && txtPwd.Text == ConfigurationManager.AppSettings["Admin.Pwd"])
            {
                CurrentUser = new WebUser("admin", "admin", "", 1, true, "A");

                var returnUrl = Request.QueryString["returnUrl"];
                if (string.IsNullOrEmpty(returnUrl))
                    Response.Redirect("home.aspx");
                else
                    Response.Redirect(returnUrl);
            }
            else
                pnlLoginError.Visible = true;
        }
    }
}