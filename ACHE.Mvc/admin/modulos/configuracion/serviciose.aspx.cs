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
    public partial class serviciose : AdminBasePage {
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
                        Response.Redirect("servicios.aspx");
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
                var entity = dbContext.Servicios.Where(x => x.IDServicio == this.idEntidad).FirstOrDefault();
                if (entity != null) {
                    txtNombre.Text = entity.Nombre;

                    if (entity.Imagen != null) {
                        imgImagen.ImageUrl = "/files/servicios/" + entity.Imagen;
                        imgImagen.Visible = true;
                        btnEliminarImagen.Visible = true;
                        pImagen.Visible = false;
                    }
                }
                else
                    throw new Exception("Entidad inexistente");
            }
        }

        private void CreateEntity() {
            using (var dbContext = new ACHEEntities()) {
                Servicios entity = null;
                if (this.idEntidad > 0)
                    entity = dbContext.Servicios.Where(x => x.IDServicio == this.idEntidad).FirstOrDefault();
                else
                    entity = new Servicios();

                entity.Nombre = txtNombre.Text.Trim();

                if (flpImagen.HasFile) {
                    string ext = System.IO.Path.GetExtension(flpImagen.FileName);
                    string uniqueName = "imagen_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ext;
                    string path = System.IO.Path.Combine(Server.MapPath("~/files/servicios/"), uniqueName);
                    //save the file to our local path
                    flpImagen.SaveAs(path);
                    entity.Imagen = uniqueName;
                }

                if (this.idEntidad > 0)
                    dbContext.SaveChanges();
                else {
                    dbContext.Servicios.Add(entity);
                    dbContext.SaveChanges();
                }
            }
        }

        protected void btnAceptar_OnClick(object sender, EventArgs e) {
            if (IsValid == true) {
                Response.Redirect("servicios.aspx");
            }
        }

        protected void btnCancelar_OnClick(object sender, EventArgs e) {
            Response.Redirect("servicios.aspx");
        }

        private void ShowError(string msg) {
            litError.Text = msg;
            pnlError.Visible = true;
        }

        protected void btnEliminarImagen_Click(object sender, EventArgs e) {
            using (var dbContext = new ACHEEntities()) {
                try {
                    var entity = dbContext.Servicios.Where(x => x.IDServicio == this.idEntidad).FirstOrDefault();
                    if (entity != null) {
                        entity.Imagen = null;

                        dbContext.SaveChanges();

                        imgImagen.ImageUrl = null;
                        imgImagen.Visible = false;
                        pImagen.Visible = true;
                        btnEliminarImagen.Visible = false;
                    }
                }
                catch (Exception ex) {
                    ShowError(ex.Message);
                }
            }
        }
    }
}