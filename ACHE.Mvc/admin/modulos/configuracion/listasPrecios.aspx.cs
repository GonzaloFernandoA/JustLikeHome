using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;

namespace ACHE.Mvc.admin.modulos.configuracion {
    public partial class listasPrecios : AdminBasePage {
        protected void Page_Load(object sender, EventArgs e) {

        }

        [System.Web.Services.WebMethod(true)]
        public static DataSourceResult Get(int take, int skip, IEnumerable<Sort> sort, Filter filter) {
            if (HttpContext.Current.Session["AdminUser"] != null) {
                using (var dbContext = new ACHEEntities()) {
                    return dbContext.ListasPrecio
                        .OrderBy(x => x.Nombre)
                        .Select(x => new ListasPrecioViewModel() {
                            IDListaPrecio = x.IDListaPrecio,
                            Nombre = x.Nombre,
                            Activo = x.Activo ? "Si" : "No",
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
                    if (dbContext.Precios.Any(x => x.IDListaPrecio == id))
                        throw new Exception("La lista esta asociada a uno o más precios");
                    else if (dbContext.Propiedades.Any(x => x.IDListaPrecio == id))
                        throw new Exception("La lista esta asociada a una o más propiedades");
                    else {
                        var entity = dbContext.ListasPrecio.Where(x => x.IDListaPrecio == id).FirstOrDefault();
                        if (entity != null) {
                            dbContext.ListasPrecio.Remove(entity);
                            dbContext.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}