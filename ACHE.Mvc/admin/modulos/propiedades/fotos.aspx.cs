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
    public partial class fotos : AdminBasePage
    {
        #region Properties

        private int idEntidad
        {
            get
            {
                if (ViewState["idEntidad"] != null)
                    return (int)ViewState["idEntidad"];
                else
                    return 0;
            }
            set { ViewState["idEntidad"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            string query = Request.QueryString["Id"].ToString();
            if (query != null)
            {
                int id;
                bool validId = int.TryParse(query, out id);
                if (validId)
                {
                    this.idEntidad = id;
                    LoadFotos(id);
                }
            }
        }

        private void LoadFotos(int id)
        {
            using (var dbContext = new ACHEEntities())
            {
                string prop = dbContext.Propiedades.Where(x => x.IDPropiedad == id).FirstOrDefault().Nombre;
                if (!string.IsNullOrEmpty(prop))
                    litNombreProp.Text = prop;

                List<FotosViewModel> fotos = dbContext.Fotos.Where(x => x.IDPropiedad == id).OrderBy(x => x.Orden)
                .Select(x => new FotosViewModel()
                {
                    IDFoto = x.IDFoto,
                    Foto = x.Foto,
                }).ToList();

                if (fotos != null && fotos.Count() > 0)
                {
                    rptFotos.DataSource = fotos;
                    rptFotos.DataBind();
                }
            }
        }

        protected void btnSubirFoto_Click(object sender, EventArgs e)
        {
            if (IsValid == true)
            {
                string ext = System.IO.Path.GetExtension(flpFoto.FileName).ToLower();
                if (ext == ".jpg" || ext == ".gif" || ext == ".png")
                {
                    string uniqueName = "prop" + this.idEntidad + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ext;
                    string path = System.IO.Path.Combine(Server.MapPath("~/files/propiedades/"), uniqueName);
                    //save the file to our local path
                    flpFoto.SaveAs(path);
                    using (var dbContext = new ACHEEntities())
                    {
                        Fotos foto = new Fotos();
                        foto.IDPropiedad = this.idEntidad;
                        foto.Foto = uniqueName;

                        dbContext.Fotos.Add(foto);
                        dbContext.SaveChanges();

                        rptFotos.DataSource = dbContext.Fotos.Where(x => x.IDPropiedad == this.idEntidad).ToList();
                        rptFotos.DataBind();
                    }
                }
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("propiedadese.aspx?Mode=E&Id=" + this.idEntidad);
        }

        [System.Web.Services.WebMethod]
        public static void Delete(int id)
        {
            using (var dbContext = new ACHEEntities())
            {
                var entity = dbContext.Fotos.Where(x => x.IDFoto == id).FirstOrDefault();
                if (entity != null)
                {
                    dbContext.Fotos.Remove(entity);
                    dbContext.SaveChanges();
                }
            }
        }

        [System.Web.Services.WebMethod]
        public static void UpdateOrder(string fotos)
        {
            using (var dbContext = new ACHEEntities())
            {
                string[] aux = fotos.Split('&');
                int id = 0;
                int order = 0;
                if (aux.Length > 0)
                {
                    foreach (var s in aux)
                    {
                        if (s != string.Empty)
                        {
                            id = int.Parse(s.Split('=')[0]);
                            order = int.Parse(s.Split('=')[1]);

                            dbContext.Database.ExecuteSqlCommand("update Fotos set Orden=" + order + " where IDFoto=" + id, new object[] { });
                        }
                    }
                }
            }
        }
    }
}