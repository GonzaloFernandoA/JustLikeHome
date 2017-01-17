using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace ACHE.Model {
	public class WebBasePage : System.Web.UI.Page {

		public string TipoPagina = "front";
		
		public WebUser CurrentUser {
			get { return (Session["CurrentUser"] != null) ? (WebUser)Session["CurrentUser"] : null; }
			set { Session["CurrentUser"] = value; }
		}

        //public WebNegocio CurrentNegocio {
        //    get { return (Session["CurrentNegocio"] != null) ? (WebNegocio)Session["CurrentNegocio"] : null; }
        //    set { Session["CurrentNegocio"] = value; }
        //}

		public int IDZonaActual {
			get { return (Session["IDZonaActual"] != null) ? (int)Session["IDZonaActual"] : 0; }
			set { Session["IDZonaActual"] = value; }
		}

		protected override void OnPreInit(EventArgs e) {
			//ValidateUser();
			SetConfigurations();
		}

		protected virtual void SetConfigurations() {
			//MaintainScrollPositionOnPostBack = true;
		}
	}
}
