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
    public partial class zonase : AdminBasePage {

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
                        Response.Redirect("zonas.aspx");
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
                var entity = dbContext.Zonas.Where(x => x.IDZona == this.idEntidad).FirstOrDefault();
                if (entity != null)
                    txtNombre.Text = entity.Nombre;
                else
                    throw new Exception("Entidad inexistente");
            }
        }

        private void CreateEntity() {
            using (var dbContext = new ACHEEntities()) {
                Zonas entity = null;
                if (this.idEntidad > 0)
                    entity = dbContext.Zonas.Where(x => x.IDZona == this.idEntidad).FirstOrDefault();
                else {
                    bool existeZona = dbContext.Zonas.Any(x => x.Nombre.ToLower() == txtNombre.Text.ToLower().Trim());
                    if (existeZona)
                        throw new Exception("Ya existe una zona con ese nombre, por favor ingrese otro.");
                    else
                        entity = new Zonas();   
                }

                entity.Nombre = txtNombre.Text.Trim();

                if (this.idEntidad > 0)
                    dbContext.SaveChanges();
                else {
                    dbContext.Zonas.Add(entity);
                    dbContext.SaveChanges();
                }
            }
        }

        protected void btnAceptar_OnClick(object sender, EventArgs e) {
            if (IsValid == true) {
                Response.Redirect("zonas.aspx");
            }
        }

        protected void btnCancelar_OnClick(object sender, EventArgs e) {
            Response.Redirect("zonas.aspx");
        }

        private void ShowError(string msg) {
            litError.Text = msg;
            pnlError.Visible = true;
        }
    }
}