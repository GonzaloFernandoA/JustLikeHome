using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;
using System.Configuration;

namespace ACHE.Mvc.admin.modulos.administracion {
    public partial class reservase : AdminBasePage {

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
                        Response.Redirect("reservas.aspx");
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
                #region clientes
                var clientes = dbContext.Clientes.Select(x => new { IDCliente = x.IDCliente, Nombre = x.Apellido + ", " + x.Nombre, }).ToList();
                if (clientes != null) {
                    cmbCliente.DataSource = clientes;
                    cmbCliente.DataValueField = "IDCliente";
                    cmbCliente.DataTextField = "Nombre";
                    cmbCliente.DataBind();

                    cmbCliente.Items.Insert(0, new ListItem("Seleccione un cliente...", ""));
                }
                #endregion

                #region propiedades
                var propiedades = dbContext.Propiedades.ToList();
                if (propiedades != null) {
                    cmbPropiedad.DataSource = propiedades;
                    cmbPropiedad.DataValueField = "IDPropiedad";
                    cmbPropiedad.DataTextField = "Nombre";
                    cmbPropiedad.DataBind();

                    cmbPropiedad.Items.Insert(0, new ListItem("Seleccione una propiedad...", ""));
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
                var entity = dbContext.Reservas.Where(x => x.IDReserva == this.idEntidad).FirstOrDefault();
                if (entity != null) {
                    cmbPropiedad.SelectedValue = entity.IDPropiedad.ToString();
                    cmbCliente.SelectedValue = entity.IDCliente.ToString();
                    cmbEstado.SelectedValue = entity.Estado;
                    txtPrecioTotal.Text = entity.PrecioTotal.ToString("N2");
                    //cmbTipo.SelectedValue = entity.TipoReserva;
                    txtDesde.Text = entity.FechaIngreso.ToShortDateString();
                    cmbMeses.SelectedValue = entity.CantMeses.ToString();
                    txtObservaciones.Text = entity.Observaciones;
                }
                else
                    throw new Exception("Entidad inexistente");
            }
        }

        private void CreateEntity() {
            using (var dbContext = new ACHEEntities()) {
                Reservas entity = null;
                if (this.idEntidad > 0)
                    entity = dbContext.Reservas.Where(x => x.IDReserva == this.idEntidad).FirstOrDefault();
                else {
                    int idProp = int.Parse(cmbPropiedad.SelectedValue);
                    DateTime fDesde = DateTime.Parse(txtDesde.Text);
                    int meses = int.Parse(cmbMeses.SelectedValue);
                    DateTime fHasta = fDesde.AddMonths(meses);
                    //verifico que no haya reserva en las fechas para la propiedad
                    //var hayReserva = dbContext.Reservas.Where(x => x.IDPropiedad == idProp && x.Estado == "Aceptada").FirstOrDefault();
                    //if (hayReserva != null) {
                    //    if ((fDesde.IsDateBetween(hayReserva.FechaIngreso.Date, hayReserva.FechaEgreso.Date)) || (fHasta.IsDateBetween(hayReserva.FechaIngreso.Date, hayReserva.FechaEgreso.Date)))
                    //        throw new Exception("Ya hay una reserva aceptada entre las fechas " + hayReserva.FechaIngreso.ToShortDateString() + " y " + hayReserva.FechaEgreso.ToShortDateString() + ", por favor seleccione otra fecha");
                    //}
                    var reservas = dbContext.Reservas.Where(x => x.IDPropiedad == idProp && x.Estado == "Aceptada").ToList();
                    if (reservas != null && reservas.Count() > 0) {
                        foreach (var res in reservas) {
                            if (fDesde.IsDateBetween(res.FechaIngreso, res.FechaEgreso) || fDesde.AddMonths(meses).IsDateBetween(res.FechaIngreso, res.FechaEgreso))
                                throw new Exception("La propiedad ya esta reservada entre las fechas ingresadas");
                        }
                    }

                    entity = new Reservas();
                }

                entity.IDPropiedad = int.Parse(cmbPropiedad.SelectedValue);
                if (!string.IsNullOrEmpty(cmbCliente.SelectedValue))
                    entity.IDCliente = int.Parse(cmbCliente.SelectedValue);
                else if (!string.IsNullOrEmpty(hdnIDCliente.Value))
                    entity.IDCliente = int.Parse(hdnIDCliente.Value);

                entity.FechaReserva = DateTime.Now;
                entity.TipoReserva = "";
                entity.Estado = cmbEstado.SelectedValue;
                entity.PrecioTotal = decimal.Parse(txtPrecioTotal.Text.Trim());
                entity.FechaIngreso = DateTime.Parse(txtDesde.Text);
                entity.FechaEgreso = entity.FechaIngreso.AddMonths(int.Parse(cmbMeses.SelectedValue));
                entity.CantMeses = int.Parse(cmbMeses.SelectedValue);
                entity.Observaciones = txtObservaciones.Text.Trim();

                if (this.idEntidad > 0)
                    dbContext.SaveChanges();
                else {
                    dbContext.Reservas.Add(entity);
                    dbContext.SaveChanges();
                }
            }
        }

        protected void btnAceptar_OnClick(object sender, EventArgs e) {
            if (IsValid == true) {
                Response.Redirect("reservas.aspx");
            }
        }

        protected void btnCancelar_OnClick(object sender, EventArgs e) {
            Response.Redirect("reservas.aspx");
        }

        private void ShowError(string msg) {
            litError.Text = msg;
            pnlError.Visible = true;
        }

        [System.Web.Services.WebMethod]
        public static string CrearCliente(string nombre, string apellido, string email, string telefono, string pais, string documento) {
            string idCliente = string.Empty;
            using (var dbContext = new ACHEEntities()) {
                bool existeCliente = dbContext.Clientes.Any(x => x.Email.ToLower() == email);
                if (existeCliente)
                    throw new Exception("Ya existe un cliente con el email: " + email);
                Clientes entity = new Clientes();
                entity.Nombre = nombre;
                entity.Apellido = apellido;
                entity.Email = email;
                entity.Telefono = telefono;
                entity.Pais = pais;
                entity.NroDocumento = documento;
                entity.FechaAlta = DateTime.Now;
                entity.Activo = true;
                dbContext.Clientes.Add(entity);
                dbContext.SaveChanges();

                idCliente = entity.IDCliente.ToString();
            }
            return idCliente;
        }
    }
}