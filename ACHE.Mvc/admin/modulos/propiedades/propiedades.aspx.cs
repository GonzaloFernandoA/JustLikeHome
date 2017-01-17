using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;

namespace ACHE.Mvc.admin.modulos.propiedades
{
    public partial class propiedades : AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                cargarCombos();
        }

        private void cargarCombos()
        {
            using (var dbContext = new ACHEEntities())
            {
                #region zonas
                var zonas = dbContext.Zonas.OrderBy(x => x.Nombre).ToList();
                if (zonas != null)
                {
                    cmbZona.DataSource = zonas;
                    cmbZona.DataValueField = "IDZona";
                    cmbZona.DataTextField = "Nombre";
                    cmbZona.DataBind();

                    cmbZona.Items.Insert(0, new ListItem("Todas las zonas", ""));
                }
                #endregion
            }
        }

        [System.Web.Services.WebMethod(true)]
        public static DataSourceResult Get(int take, int skip, IEnumerable<Sort> sort, Filter filter)
        {
            if (HttpContext.Current.Session["AdminUser"] != null)
            {
                using (var dbContext = new ACHEEntities())
                {
                    return dbContext.Propiedades
                        .OrderBy(x => x.Nombre)
                        .Select(x => new PropiedadesViewModel()
                        {
                            IDPropiedad = x.IDPropiedad,
                            IDZona = x.IDZona,
                            Zona = x.Zonas.Nombre,
                            IDListaPrecio = x.IDListaPrecio,
                            ListaPrecio = x.ListasPrecio.Nombre,
                            Codigo = x.Codigo,
                            Nombre = x.Nombre,
                            Direccion = x.Direccion,
                            Tipo = x.Tipo == "D" ? "Departamento" : "Casa",
                            CantAmbientes = x.CantAmbientes,
                            CantHuespedes = x.CantHuespedes,
                            CantCamas = x.CantCamas,
                            Activo = x.Activo ? "Si" : "No",
                            Destacado = x.Destacado ? "Si" : "No",
                        }).ToDataSourceResult(take, skip, sort, filter);//.ToList();
                }
            }
            else
                return null;
        }

        [System.Web.Services.WebMethod(true)]
        public static void Delete(int id)
        {
            if (HttpContext.Current.Session["AdminUser"] != null)
            {
                using (var dbContext = new ACHEEntities())
                {
                    if (dbContext.Propiedades.Any(x => x.Reservas.Any(p => p.IDPropiedad == id)))
                        throw new Exception("La propiedad está asociada a una o más reservas");
                    else if (dbContext.Propiedades.Any(x => x.Contactos.Any(p => p.IDPropiedad == id)))
                        throw new Exception("La propiedad está asociada a una o más contactos");
                    else
                    {
                        var entity = dbContext.Propiedades.Where(x => x.IDPropiedad == id).FirstOrDefault();
                        if (entity != null)
                        {
                            dbContext.Database.ExecuteSqlCommand("DELETE FROM Fotos WHERE IDPropiedad = " + id, new object[] { });
                            dbContext.Database.ExecuteSqlCommand("DELETE FROM PropiedadesServicios WHERE IDPropiedad = " + id, new object[] { });

                            dbContext.Propiedades.Remove(entity);
                            dbContext.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}