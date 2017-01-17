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
    public partial class preciose : AdminBasePage {
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
                        Response.Redirect("precios.aspx");
                }

                if (!IsPostBack) {
                    LoadInfo();

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

        private void LoadInfo() {
            using (var dbContext = new ACHEEntities()) {
                #region listas de precios
                var listas = dbContext.ListasPrecio.Where(x => x.Activo).ToList();
                if (listas != null) {
                    cmbListaPrecios.DataSource = listas;
                    cmbListaPrecios.DataValueField = "IDListaPrecio";
                    cmbListaPrecios.DataTextField = "Nombre";
                    cmbListaPrecios.DataBind();

                    cmbListaPrecios.Items.Insert(0, new ListItem("Seleccione una lista de precios", ""));
                }
                #endregion
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
                var entity = dbContext.Precios.Where(x => x.IDPrecio == this.idEntidad).FirstOrDefault();
                if (entity != null) {
                    txtNombre.Text = entity.Nombre;
                    cmbListaPrecios.SelectedValue = entity.IDListaPrecio.ToString();
                    txtValor1.Text = entity.ValorDia.ToString("N2");
                    txtValor2.Text = entity.ValorSemana.ToString("N2");
                    txtValor3.Text = entity.ValorQuincena.ToString("N2");
                    txtValor4.Text = entity.ValorMes.ToString("N2");
                    txtDesde.Text = entity.VigenciaDesde.ToShortDateString();
                    txtHasta.Text = entity.VigenciaHasta.ToShortDateString();
                }
                else
                    throw new Exception("Entidad inexistente");
            }
        }

        private void CreateEntity() {
            using (var dbContext = new ACHEEntities()) {
                Precios entity = null;
                if (this.idEntidad > 0)
                    entity = dbContext.Precios.Where(x => x.IDPrecio == this.idEntidad).FirstOrDefault();
                else
                    entity = new Precios();

                entity.Nombre = txtNombre.Text.Trim();
                entity.IDListaPrecio = int.Parse(cmbListaPrecios.SelectedValue);
                entity.ValorDia = decimal.Parse(txtValor1.Text);
                entity.ValorSemana = decimal.Parse(txtValor2.Text);
                entity.ValorQuincena = decimal.Parse(txtValor3.Text);
                entity.ValorMes = decimal.Parse(txtValor4.Text);

                entity.VigenciaDesde = DateTime.Parse(txtDesde.Text);
                entity.VigenciaHasta = DateTime.Parse(txtHasta.Text);

                if (this.idEntidad > 0)
                    dbContext.SaveChanges();
                else {
                    dbContext.Precios.Add(entity);
                    dbContext.SaveChanges();
                }
            }
        }

        protected void btnAceptar_OnClick(object sender, EventArgs e) {
            if (IsValid == true) {
                Response.Redirect("precios.aspx");
            }
        }

        protected void btnCancelar_OnClick(object sender, EventArgs e) {
            Response.Redirect("precios.aspx");
        }

        private void ShowError(string msg) {
            litError.Text = msg;
            pnlError.Visible = true;
        }
    }
}