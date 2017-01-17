using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;

namespace ACHE.Mvc.admin.modulos.administracion {
    public partial class reservas : AdminBasePage {

        protected void Page_Load(object sender, EventArgs e) {

        }

        [System.Web.Services.WebMethod(true)]
        public static DataSourceResult Get(int take, int skip, IEnumerable<Sort> sort, Filter filter, string fDesde, string fHasta) {
            if (HttpContext.Current.Session["AdminUser"] != null) {
                using (var dbContext = new ACHEEntities()) {
                    DateTime fechaDesde, fechaHasta;
                    var result = dbContext.Reservas.Include("Propiedades").Include("Clientes")
                        .OrderBy(x => x.FechaReserva)
                        .Select(x => new ReservasViewModel() {
                            IDReserva = x.IDReserva,
                            IDPropiedad = x.IDPropiedad,
                            IDCliente = x.IDCliente,
                            Propiedad = x.Propiedades.Codigo + " - " + x.Propiedades.Nombre,
                            Cliente = x.Clientes.Apellido + ", " + x.Clientes.Nombre,
                            Estado = x.Estado,
                            FechaReserva = x.FechaReserva,
                            FechaIngreso = x.FechaIngreso,
                            FechaEgreso = x.FechaEgreso,
                            CantMeses = x.CantMeses,
                            TipoReserva = x.TipoReserva,
                        }).AsQueryable();//.ToDataSourceResult(take, skip, sort, filter)

                    if (!string.IsNullOrEmpty(fDesde)) {
                        fechaDesde = DateTime.Parse(fDesde);
                        result = result.Where(x => x.FechaIngreso >= fechaDesde.Date);//.AddDays(1).AddTicks(-1)
                    }

                    if (!string.IsNullOrEmpty(fHasta)) {
                        fechaHasta = DateTime.Parse(fHasta);
                        result = result.Where(x => x.FechaEgreso <= fechaHasta.Date);//.AddDays(1).AddTicks(-1)
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
                    var entity = dbContext.Reservas.Where(x => x.IDReserva == id).FirstOrDefault();
                    if (entity != null) {
                        if (entity.Estado != "Cancelada")
                            entity.Estado = "Cancelada";
                        else
                            dbContext.Reservas.Remove(entity);

                        dbContext.SaveChanges();
                    }
                }
            }
        }
    }
}