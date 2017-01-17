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
    public partial class servicios : AdminBasePage {
        protected void Page_Load(object sender, EventArgs e) {

        }

        [System.Web.Services.WebMethod(true)]
        public static DataSourceResult Get(int take, int skip, IEnumerable<Sort> sort, Filter filter) {
            if (HttpContext.Current.Session["AdminUser"] != null) {
                using (var dbContext = new ACHEEntities()) {
                    return dbContext.Servicios
                        .OrderBy(x => x.Nombre)
                        .Select(x => new ServiciosViewModel() {
                            IDServicio = x.IDServicio,
                            Nombre = x.Nombre,
                            TieneImagen = x.Imagen != null ? "Si" : "No",
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
                    if (dbContext.PropiedadesServicios.Any(x => x.IDServicio == id))
                        throw new Exception("El servicio esta asociado a una o más propiedades");
                    else {
                        var entity = dbContext.Servicios.Where(x => x.IDServicio == id).FirstOrDefault();
                        if (entity != null) {
                            dbContext.Servicios.Remove(entity);
                            dbContext.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}