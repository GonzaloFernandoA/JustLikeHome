using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;

namespace ACHE.Mvc.admin.modulos.administracion {
    public partial class clientes : WebBasePage {

        protected void Page_Load(object sender, EventArgs e) {

        }

        [System.Web.Services.WebMethod(true)]
        public static DataSourceResult Get(int take, int skip, IEnumerable<Sort> sort, Filter filter) {
            if (HttpContext.Current.Session["AdminUser"] != null) {
                using (var dbContext = new ACHEEntities()) {
                    return dbContext.Clientes
                        .OrderBy(x => x.Apellido).ThenBy(x => x.Nombre)
                        .Select(x => new ClientesViewModel() {
                            IDCliente = x.IDCliente,
                            Nombre = x.Apellido + ", " + x.Nombre,
                            Email = x.Email,
                            Telefono = x.Telefono,
                            Pais = x.Pais,
                            NroDocumento = x.NroDocumento,
                        }).ToDataSourceResult(take, skip, sort, filter);//.ToList();
                }
            }
            else
                return null;
        }

        [System.Web.Services.WebMethod(true)]
        public static void Delete(int id) {
            if (HttpContext.Current.Session["AdminUser"] != null) {
                using (var dbContext = new ACHEEntities()) {
                    if (dbContext.Reservas.Any(x => x.IDCliente == id))
                        throw new Exception("El cliente esta asociado a una o más reservas");
                    else {
                        var entity = dbContext.Clientes.Where(x => x.IDCliente == id).FirstOrDefault();
                        if (entity != null) {
                            dbContext.Clientes.Remove(entity);
                            dbContext.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}