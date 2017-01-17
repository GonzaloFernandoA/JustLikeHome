using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace ACHE.Model {
	public class AdminBasePage : System.Web.UI.Page {

		public static class RoleNames {
			public static readonly string Admin = "Admin";
			public static readonly string Usuario = "Usuario";
		}
		
		public string TipoPagina = "admin";

		public bool KeepScrollPosition {
			get {
				return (ViewState["KeepScrollPosition"] != null) ? (bool)ViewState["KeepScrollPosition"] : true;
			}

			set {
				ViewState["KeepScrollPosition"] = value;
			}
		}

		public WebUser CurrentUser {
            get { return (Session["AdminUser"] != null) ? (WebUser)Session["AdminUser"] : null; }
            set { Session["AdminUser"] = value; }
		}

		protected virtual void SetConfigurations() {
			MaintainScrollPositionOnPostBack = KeepScrollPosition;
		}

        //private bool UserHasAcces()
        //{
        //    if (CurrentUser.Tipo =="U")
        //        return true;
        //    else
        //        return false;
        //}

		private void ValidateUser() {
			string pageName = Request.FilePath.Substring(Request.FilePath.LastIndexOf(@"/") + 1).ToLower();
            if (pageName != "default.aspx" && pageName != "recuperar-pass.aspx")
            {
				if (CurrentUser == null) {
                    Response.Redirect("~/admin/default.aspx");
				}
                //else if (!UserHasAcces())
                //    Response.Redirect("~/login-usuarios.aspx");
			}
		}

		protected override void OnPreInit(EventArgs e) {
			ValidateUser();
			SetConfigurations();
		}
	}
}
