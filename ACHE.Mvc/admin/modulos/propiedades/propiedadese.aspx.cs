using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;
using System.Configuration;

namespace ACHE.Mvc.admin.modulos.propiedades {
    public partial class propiedadese : AdminBasePage {

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
                        Response.Redirect("propiedades.aspx");
                }

                if (!IsPostBack) {
                    LoadInfo();

                    switch (this.Mode.ToUpper()) {
                        case "A":
                            litModo.Text = litModo2.Text = litModo3.Text = "Creación";
                            btnFotos.Visible = false;
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
                #region zonas
                var zonas = dbContext.Zonas.OrderBy(x => x.Nombre).ToList();
                if (zonas != null) {
                    cmbZona.DataSource = zonas;
                    cmbZona.DataValueField = "IDZona";
                    cmbZona.DataTextField = "Nombre";
                    cmbZona.DataBind();

                    cmbZona.Items.Insert(0, new ListItem("Seleccione una zona", ""));
                }
                #endregion

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

                #region servicios
                var servicios = dbContext.Servicios.ToList();
                if (servicios != null) {
                    foreach (var item in servicios)
                        chkServicios.Items.Add(new ListItem("&nbsp;" + item.Nombre, item.IDServicio.ToString()));
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
                var entity = dbContext.Propiedades.Include("PropiedadesServicios").Where(x => x.IDPropiedad == this.idEntidad).FirstOrDefault();
                if (entity != null) {
                    txtCodigo.Text = entity.Codigo;
                    txtNombre.Text = entity.Nombre;
                    txtDescripcionBreve.Text = entity.DescripcionBreve;
                    cmbZona.SelectedValue = entity.IDZona.ToString();
                    cmbListaPrecios.SelectedValue = entity.IDListaPrecio.ToString();
                    cmbTipo.SelectedValue = entity.Tipo.ToUpper();
                    txtMinCantDias.Text = entity.MinCantDias.ToString();
                    txtDireccion.Text = entity.Direccion;
                    txtDescripcion.Text = entity.Descripcion;
                    chkActivo.Checked = entity.Activo;
                    chkDestacado.Checked = entity.Destacado;
                    txtMetros.Text = entity.Metros.ToString();
                    txtCantAmbientes.Text = entity.CantAmbientes.ToString();
                    txtCantHuespedes.Text = entity.CantHuespedes.ToString();
                    txtCantCamas.Text = entity.CantCamas.ToString();
                    txtVideo.Text = entity.Video;
                    txtLatitud.Text = entity.Latitud != null ? entity.Latitud.ToString() : "";
                    txtLongitud.Text = entity.Longitud != null ? entity.Longitud.ToString() : "";

                    List<PropiedadesServicios> servicios = entity.PropiedadesServicios.ToList();

                    foreach (ListItem item in chkServicios.Items)
                        item.Selected = servicios.Any(x => x.IDServicio == int.Parse(item.Value));
                }
                else
                    throw new Exception("Entidad inexistente");
            }
        }

        private void CreateEntity() {
            using (var dbContext = new ACHEEntities()) {
                Propiedades entity = null;
                if (this.idEntidad > 0)
                    entity = dbContext.Propiedades.Where(x => x.IDPropiedad == this.idEntidad).FirstOrDefault();
                else
                    entity = new Propiedades();

                entity.Codigo = txtCodigo.Text.Trim();
                entity.Nombre = txtNombre.Text.Trim();
                entity.DescripcionBreve = txtDescripcionBreve.Text.Trim();
                entity.IDZona = int.Parse(cmbZona.SelectedValue);
                entity.IDListaPrecio = int.Parse(cmbListaPrecios.SelectedValue);
                entity.Tipo = cmbTipo.SelectedValue;
                entity.Calificacion = 5;
                entity.MinCantDias = int.Parse(txtMinCantDias.Text.Trim());
                entity.Direccion = txtDireccion.Text.Trim();
                entity.Descripcion = txtDescripcion.Text.Trim();
                entity.Activo = chkActivo.Checked;
                entity.Destacado = chkDestacado.Checked;
                entity.Metros = int.Parse(txtMetros.Text.Trim());
                entity.CantAmbientes = int.Parse(txtCantAmbientes.Text.Trim());
                entity.CantHuespedes = int.Parse(txtCantHuespedes.Text.Trim());
                entity.CantCamas = int.Parse(txtCantCamas.Text.Trim());
                entity.Video = txtVideo.Text.Trim();
                entity.Latitud = txtLatitud.Text.Trim();
                entity.Longitud = txtLongitud.Text.Trim();

                if (this.idEntidad > 0)
                    dbContext.Database.ExecuteSqlCommand("DELETE PropiedadesServicios WHERE IDPropiedad=" + this.idEntidad, new object[] { });

                foreach (ListItem item in chkServicios.Items) {
                    int idServicio = int.Parse(item.Value);
                    if (item.Selected) {
                        PropiedadesServicios servicio = new PropiedadesServicios();
                        servicio.IDServicio = idServicio;
                        servicio.IDPropiedad = entity.IDPropiedad;
                        dbContext.PropiedadesServicios.Add(servicio);
                    }
                    //else
                    //{
                    //    var servicio = dbContext.PropiedadesServicios.Where(x => x.IDPropiedad == this.idEntidad && x.IDServicio == idServicio).FirstOrDefault();
                    //    if (servicio != null)
                    //        dbContext.PropiedadesServicios.Remove(servicio);
                    //}
                }

                if (this.idEntidad > 0)
                    dbContext.SaveChanges();
                else {
                    dbContext.Propiedades.Add(entity);
                    dbContext.SaveChanges();
                    this.idEntidad = entity.IDPropiedad;
                }
            }
        }

        protected void btnAceptar_OnClick(object sender, EventArgs e) {
            pnlError.Visible = false;
            pnlOk.Visible = false;
            if (IsValid == true) {
                pnlError.Visible = false;
                pnlOk.Visible = true;
                if (Mode == "A")
                    btnFotos.Visible = true;
            }
        }

        protected void btnCancelar_OnClick(object sender, EventArgs e) {
            Response.Redirect("propiedades.aspx");
        }

        protected void btnFotos_Click(object sender, EventArgs e) {
            Response.Redirect("fotos.aspx?Id=" + this.idEntidad);
        }

        private void ShowError(string msg) {
            litError.Text = msg;
            pnlError.Visible = true;
        }

    }
}