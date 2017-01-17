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
    public partial class zonas : AdminBasePage {
        protected void Page_Load(object sender, EventArgs e) {

        }

        [System.Web.Services.WebMethod(true)]
        public static DataSourceResult Get(int take, int skip, IEnumerable<Sort> sort, Filter filter) {
            if (HttpContext.Current.Session["AdminUser"] != null) {
                using (var dbContext = new ACHEEntities()) {
                    return dbContext.Zonas
                        .OrderBy(x => x.Nombre)
                        .Select(x => new ZonasViewModel() {
                            IDZona = x.IDZona,
                            Nombre = x.Nombre,
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
                    if (dbContext.Propiedades.Any(x => x.IDZona == id))
                        throw new Exception("La zona está asociada a una o más propiedades");
                    else {
                        var entity = dbContext.Zonas.Where(x => x.IDZona == id).FirstOrDefault();
                        if (entity != null) {
                            dbContext.Zonas.Remove(entity);
                            dbContext.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}