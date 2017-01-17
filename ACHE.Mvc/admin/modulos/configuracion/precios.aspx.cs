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
    public partial class precios : AdminBasePage {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack)
                cargarCombos();
        }

        private void cargarCombos() {
            using (var dbContext = new ACHEEntities()) {
                #region listas de precios
                var listas = dbContext.ListasPrecio.ToList();
                if (listas != null) {
                    cmbListaPrecios.DataSource = listas;
                    cmbListaPrecios.DataValueField = "IDListaPrecio";
                    cmbListaPrecios.DataTextField = "Nombre";
                    cmbListaPrecios.DataBind();

                    cmbListaPrecios.Items.Insert(0, new ListItem("Todas las listas", ""));
                }
                #endregion
            }
        }

        [System.Web.Services.WebMethod(true)]
        public static DataSourceResult Get(int take, int skip, IEnumerable<Sort> sort, Filter filter, string fDesde, string fHasta) {
            if (HttpContext.Current.Session["AdminUser"] != null) {
                using (var dbContext = new ACHEEntities()) {
                    DateTime fechaDesde, fechaHasta;
                    var result = dbContext.Precios.Include("ListasPrecio")
                        .OrderBy(x => x.Nombre).ThenBy(x => x.ValorDia)
                        .Select(x => new PreciosViewModel() {
                            IDPrecio = x.IDPrecio,
                            IDListaPrecio = x.IDListaPrecio,
                            Nombre = x.Nombre,
                            ListaPrecio = x.ListasPrecio.Nombre,
                            ValorDia = x.ValorDia,
                            ValorSemana = x.ValorSemana,
                            ValorQuincena = x.ValorQuincena,
                            ValorMes = x.ValorMes,
                            VigenciaDesde = x.VigenciaDesde,
                            VigenciaHasta = x.VigenciaHasta,
                        }).AsQueryable();

                    if (!string.IsNullOrEmpty(fDesde)) {
                        fechaDesde = DateTime.Parse(fDesde);
                        result = result.Where(x => x.VigenciaDesde >= fechaDesde.Date);
                    }

                    if (!string.IsNullOrEmpty(fHasta)) {
                        fechaHasta = DateTime.Parse(fHasta);
                        result = result.Where(x => x.VigenciaHasta <= fechaHasta.Date);
                    }

                    return result.ToDataSourceResult(take, skip, sort, filter);
                }
            }
            else
                return null;
        }

        [System.Web.Services.WebMethod(true)]
        public static void Delete(int id) {
            if (HttpContext.Current.Session["AdminUser"] != null) {
                using (var dbContext = new ACHEEntities()) {
                    var entity = dbContext.Precios.Where(x => x.IDPrecio == id).FirstOrDefault();
                    if (entity != null) {
                        dbContext.Precios.Remove(entity);
                        dbContext.SaveChanges();
                    }
                }
            }
        }
    }
}