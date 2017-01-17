using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;
using System.Configuration;

namespace ACHE.Mvc.admin.modulos.configuracion {
    public partial class listasPreciose : AdminBasePage {
        #region Properties

        private string Mode = string.Empty;

        private int idEntidad {
            get {
                if (ViewState["idEntidad"] != null)
                    return (int)ViewState["idEntidad"];
                else
                    return 0;
            }
            set { ViewState["idEntidad"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e) {
            try {
                this.Mode = Request.QueryString["Mode"];
                if (this.Mode != "A") {
                    if (Request.QueryString["Id"] != null)
                        this.idEntidad = int.Parse(Request.QueryString["Id"]);
                    else
                        Response.Redirect("listasPrecios.aspx");
                }

                if (!IsPostBack) {
                    switch (this.Mode.ToUpper()) {
                        case "A":
                            litModo.Text = litModo2.Text = litModo3.Text = "Creación";
                            break;
                        case "E":
                            litModo.Text = litModo2.Text = litModo3.Text = "Edición";
                            LoadEntity();
                            break;
                        default:
                            throw new Exception("Parametros incorrectos");
                    }
                }
            }
            catch (Exception ex) {
                Response.Redirect("/admin/error_500.aspx");
            }
        }

        protected void ServerValidate(object sender, ServerValidateEventArgs args) {
            args.IsValid = Process();
        }

        private bool Process() {
            bool value = false;

            if (IsValid) {
                try {
                    CreateEntity();
                    value = true;
                }
                catch (Exception e) {
                    value = false;
                    var msg = e.InnerException != null ? e.InnerException.Message : e.Message;
                    BasicLog.AppendToFile(Server.MapPath(ConfigurationManager.AppSettings["BasicLogError"]), msg, e.ToString());
                    ShowError(e.Message);
                }
            }
            return value;
        }

        private void LoadEntity() {
            using (var dbContext = new ACHEEntities()) {
                var entity = dbContext.ListasPrecio.Where(x => x.IDListaPrecio == this.idEntidad).FirstOrDefault();
                if (entity != null) {
                    txtNombre.Text = entity.Nombre;
                    chkActivo.Checked = entity.Activo;
                }
                else
                    throw new Exception("Entidad inexistente");
            }
        }

        private void CreateEntity() {
            using (var dbContext = new ACHEEntities()) {
                ListasPrecio entity = null;
                if (this.idEntidad > 0)
                    entity = dbContext.ListasPrecio.Where(x => x.IDListaPrecio == this.idEntidad).FirstOrDefault();
                else
                    entity = new ListasPrecio();

                entity.Nombre = txtNombre.Text.Trim();
                entity.Activo = chkActivo.Checked;

                if (this.idEntidad > 0)
                    dbContext.SaveChanges();
                else {
                    dbContext.ListasPrecio.Add(entity);
                    dbContext.SaveChanges();
                }
            }
        }

        protected void btnAceptar_OnClick(object sender, EventArgs e) {
            if (IsValid == true) {
                Response.Redirect("listasPrecios.aspx");
            }
        }

        protected void btnCancelar_OnClick(object sender, EventArgs e) {
            Response.Redirect("listasPrecios.aspx");
        }

        private void ShowError(string msg) {
            litError.Text = msg;
            pnlError.Visible = true;
        }
    }
}