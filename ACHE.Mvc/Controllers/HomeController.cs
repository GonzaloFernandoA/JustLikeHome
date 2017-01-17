using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACHE.Model;
using ACHE.Models;
using ACHE.Extensions;
using System.Collections.Specialized;
using System.Configuration;
using PagedList;

namespace ACHE.Web.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            IndexViewModel model = new IndexViewModel();
            using (var dbContext = new ACHEEntities())
            {
                #region zonas

                var zonas = GetAllZonas(dbContext);
                if (zonas != null)
                {
                    model.Zonas = zonas;
                    ViewBag.Zonas = zonas;
                }

                #endregion

                #region destacadas

                var destacadas = dbContext.Propiedades
                    .Include("Fotos")
                    .Where(x => x.Activo && x.Destacado && x.Fotos.Any())
                    .OrderBy(x => x.Nombre)
                    .Take(2)
                    .ToList()
                    .Select(x => new PropiedadesViewModel()
                    {
                        IDPropiedad = x.IDPropiedad,
                        Nombre = x.Nombre,
                        NombreFriendly = x.Nombre.ToFriendlyUrl(),
                        CantAmbientes = x.CantAmbientes,
                        MinCantDias = x.MinCantDias,
                        Zona = x.Zonas.Nombre,
                        IDListaPrecio = x.IDListaPrecio,
                        Fotos = x.Fotos.Select(y => new FotosViewModel() { IDFoto = y.IDFoto, Foto = y.Foto }).OrderBy(y => y.Orden).Take(3).ToList(),
                    }).ToList();

                if (destacadas != null)
                {
                    DateTime fechaHoy = DateTime.Now.Date;
                    foreach (var prop in destacadas)
                    {
                        //prop.Fotos = prop.Fotos.Select(y => new FotosViewModel() { IDFoto = y.IDFoto, Foto = y.Foto }).OrderBy(y => y.Orden).Take(3).ToList();

                        var precios = dbContext.Precios.Where(x => x.IDListaPrecio == prop.IDListaPrecio).ToList();
                        foreach (var pre in precios)
                        {
                            if (fechaHoy.IsDateBetween(pre.VigenciaDesde, pre.VigenciaHasta))
                                prop.Precio = pre.ValorMes;
                        }
                    }
                    model.Destacadas = destacadas;
                }

                #endregion
            }
            return View(model);
        }

        public ActionResult Listado(int? page, DateTime? desde, int? cantMeses, int? idZona, int? cantAmbientes)
        {
            if (desde != null || cantMeses != null || idZona != null || cantAmbientes != null)
            {
                IndexViewModel a = new IndexViewModel();
                a.Desde = desde ?? DateTime.MinValue;
                a.CantMeses = cantMeses ?? 0;
                a.IDZona = idZona ?? 0;
                a.CantAmbientes = cantAmbientes ?? 0;
                return Listado(a, page);
            }
            IndexViewModel model = new IndexViewModel();
            model.Orden = 1;
            using (var dbContext = new ACHEEntities())
            {
                #region propiedades
                var listado = GetAllPropiedades(dbContext);
                if (listado != null)
                {
                    List<MarkerViewModel> matriz = new List<MarkerViewModel>();
                    DateTime fechaHoy = DateTime.Now.Date;
                    int i = 1;
                    foreach (var prop in listado)
                    {
                        if (!string.IsNullOrEmpty(prop.Latitud) && !string.IsNullOrEmpty(prop.Longitud))
                        {
                            MarkerViewModel aux = new MarkerViewModel();
                            aux.Nombre = prop.Nombre;
                            aux.Descripcion = prop.Descripcion;
                            aux.Link = prop.NombreFriendly + "/" + prop.IDPropiedad;
                            if (!string.IsNullOrEmpty(prop.Latitud))
                                aux.Latitud = decimal.Parse(prop.Latitud.Replace(".", ","));
                            if (!string.IsNullOrEmpty(prop.Longitud))
                                aux.Longitud = decimal.Parse(prop.Longitud.Replace(".", ","));
                            aux.Orden = i;
                            aux.Foto = prop.Fotos.Any() ? prop.Fotos[0].Foto : "";
                            matriz.Add(aux);
                        }
                        var precios = dbContext.Precios.Where(x => x.IDListaPrecio == prop.IDListaPrecio).ToList();
                        foreach (var pre in precios)
                        {
                            if (fechaHoy.IsDateBetween(pre.VigenciaDesde, pre.VigenciaHasta))
                                prop.Precio = pre.ValorMes;
                        }
                        i++;
                    }
                    Session["Lista"] = matriz;
                    Session["Propiedades"] = model.Propiedades;
                    if (model.Orden == 1)
                        listado = listado.OrderBy(x => x.Precio).ToList();
                    else
                        listado = listado.OrderByDescending(x => x.Precio).ToList();

                    var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
                    var onePageOfProducts = listado.ToPagedList(pageNumber, 3); // will only contain 25 products max because of the pageSize
                    ViewBag.OnePageOfProducts = onePageOfProducts;
                    ViewBag.Cantidad = onePageOfProducts.Count();
                    model.Propiedades = listado;
                }

                if (model.IDZona > 0)
                    ViewBag.ZonaActual = ", " + dbContext.Zonas.Where(x => x.IDZona == model.IDZona).First().Nombre;
                else
                    ViewBag.ZonaActual = "";

                #endregion

                #region zonas

                var zonas = GetAllZonas(dbContext);
                if (zonas != null)
                {
                    model.Zonas = zonas;
                    ViewBag.Zonas = zonas;
                }

                #endregion
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Listado(IndexViewModel model, int? page)
        {
            ViewBag.OnePageOfProducts = null;
            if (model.Orden == 0)
                model.Orden = 1;
            using (var dbContext = new ACHEEntities())
            {

                #region propiedades
                var listado = GetAllPropiedades(dbContext);
                if (listado != null)
                {
                    DateTime fechaHoy = DateTime.Now.Date;
                    if (model != null)
                    {
                        if (model.IDZona > 0)
                            listado = listado.Where(x => x.IDZona == model.IDZona).ToList();
                        if (model.CantAmbientes > 0)
                            listado = listado.Where(x => x.CantAmbientes == model.CantAmbientes).ToList();
                        if (model.CantHuespedes > 0)
                            listado = listado.Where(x => x.CantHuespedes == model.CantHuespedes).ToList();
                        if (model.Desde != DateTime.MinValue)
                        {
                            var reservas = dbContext.Reservas.Where(x => x.FechaIngreso <= model.Desde && x.FechaEgreso >= model.Desde).ToList();
                            if (reservas != null)
                            {
                                foreach (var res in reservas)
                                {
                                    var reservadas = listado.Where(x => x.IDPropiedad == res.IDPropiedad).ToList();
                                    foreach (var item in reservadas)
                                        listado.Remove(item);
                                }
                            }
                        }
                    }
                    List<MarkerViewModel> matriz = new List<MarkerViewModel>();
                    int i = 1;
                    foreach (var prop in listado)
                    {
                        if (!string.IsNullOrEmpty(prop.Latitud) && !string.IsNullOrEmpty(prop.Longitud))
                        {
                            MarkerViewModel aux = new MarkerViewModel();
                            aux.Nombre = prop.Nombre;
                            aux.Descripcion = prop.Descripcion;
                            aux.Link = prop.NombreFriendly + "/" + prop.IDPropiedad;
                            if (!string.IsNullOrEmpty(prop.Latitud))
                                aux.Latitud = decimal.Parse(prop.Latitud.Replace(".", ","));
                            if (!string.IsNullOrEmpty(prop.Longitud))
                                aux.Longitud = decimal.Parse(prop.Longitud.Replace(".", ","));
                            aux.Orden = i;
                            aux.Foto = prop.Fotos.Any() ? prop.Fotos[0].Foto : "";
                            matriz.Add(aux);
                        }
                        var precios = dbContext.Precios.Where(x => x.IDListaPrecio == prop.IDListaPrecio).ToList();
                        foreach (var pre in precios)
                        {
                            if (fechaHoy.IsDateBetween(pre.VigenciaDesde, pre.VigenciaHasta))
                                prop.Precio = pre.ValorMes;
                        }
                        i++;
                    }
                    Session["Lista"] = matriz;
                    Session["Propiedades"] = model.Propiedades;
                    if (model.Orden == 1)
                        listado = listado.OrderBy(x => x.Precio).ToList();
                    else
                        listado = listado.OrderByDescending(x => x.Precio).ToList();

                    var pageNumber = page ?? 1;
                    var onePageOfProducts = listado.ToPagedList(pageNumber, 3);
                    ViewBag.OnePageOfProducts = onePageOfProducts;
                    ViewBag.Cantidad = onePageOfProducts.Count();
                    model.Propiedades = listado;
                }

                if (model.IDZona > 0)
                    ViewBag.ZonaActual = ", " + dbContext.Zonas.Where(x => x.IDZona == model.IDZona).First().Nombre;
                else
                    ViewBag.ZonaActual = "";

                #endregion

                #region zonas

                var zonas = GetAllZonas(dbContext);
                if (zonas != null)
                {
                    model.Zonas = zonas;
                    ViewBag.Zonas = zonas;
                }

                #endregion
            }
            return View(model);
        }

        public ActionResult Detalle(int id)
        {
            DetalleViewModel model = new DetalleViewModel();
            if (id > 0)
            {
                using (var dbContext = new ACHEEntities())
                {
                    #region propiedad
                    DateTime fechaHoy = DateTime.Now.Date;
                    var propiedad = dbContext.Propiedades
                        .Include("Fotos")
                        .Include("PropiedadesServicios")
                        .Include("ListasPrecio")
                        .Where(x => x.IDPropiedad == id && x.Activo)
                        .ToList()
                        .Select(x => new DetalleViewModel()
                        {
                            IDPropiedad = x.IDPropiedad,
                            IDZona = x.IDZona,
                            Nombre = x.Nombre,
                            Codigo = x.Codigo,
                            Descripcion = x.Descripcion,
                            Tipo = x.Tipo == "d" ? "Departamento" : "Casa",
                            Zona = x.Zonas.Nombre,
                            CantAmbientes = x.CantAmbientes,
                            CantCamas = x.CantCamas,
                            CantHuespedes = x.CantHuespedes,
                            PrecioMes = x.ListasPrecio.Precios.Any(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy) ? x.ListasPrecio.Precios.Where(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy).FirstOrDefault().ValorMes : 0,
                            PrecioQuincena = x.ListasPrecio.Precios.Any(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy) ? x.ListasPrecio.Precios.Where(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy).FirstOrDefault().ValorQuincena : 0,
                            PrecioSemana = x.ListasPrecio.Precios.Any(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy) ? x.ListasPrecio.Precios.Where(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy).FirstOrDefault().ValorSemana : 0,
                            PrecioDia = x.ListasPrecio.Precios.Any(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy) ? x.ListasPrecio.Precios.Where(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy).FirstOrDefault().ValorDia : 0,
                            Servicios = x.PropiedadesServicios.Select(y => y.Servicios).ToList(),
                            Fotos = x.Fotos.OrderBy(y => y.Orden).Select(y => new FotosViewModel() { IDFoto = y.IDFoto, Foto = y.Foto + ".ashx?w=891&h=415" }).ToList(),
                            Video = x.Video,
                            Latitud = x.Latitud,
                            Longitud = x.Longitud,
                        }).FirstOrDefault();
                    if (propiedad != null)
                    {
                        model = propiedad;

                        #region similares
                        var similares = dbContext.Propiedades
                            .Include("ListasPrecio")
                            .Include("Zonas")
                            .Where(x => x.CantAmbientes == propiedad.CantAmbientes && x.IDPropiedad != propiedad.IDPropiedad && x.Activo)
                            .ToList()
                            .Select(x => new SimilaresViewModel()
                            {
                                IDPropiedad = x.IDPropiedad,
                                Nombre = x.Nombre,
                                NombreFriendly = x.Nombre.ToFriendlyUrl(),
                                Precio = x.ListasPrecio.Precios.Any(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy) ? x.ListasPrecio.Precios.Where(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy).FirstOrDefault().ValorMes : 0,
                                Zona = x.Zonas.Nombre,
                                Direccion = x.Direccion,
                                Foto = x.Fotos.Select(y => new { Foto = y.Foto + ".ashx?w=585&h=300" }).FirstOrDefault().Foto,
                            }).ToList();

                        if (similares != null)
                            model.Similares = similares;

                        #endregion

                        #region calendario
                        DateTime date = DateTime.Now.Date;
                        model.Calendario1Desde = new DateTime(date.Year, date.Month, 1);
                        model.Calendario1Hasta = model.Calendario1Desde.AddMonths(1).AddDays(-1);
                        model.Calendario2Desde = model.Calendario1Desde.AddMonths(1);
                        model.Calendario2Hasta = model.Calendario2Desde.AddMonths(1).AddDays(-1);
                        #endregion

                        #region reservas
                        List<FechaCalendario> listaReservadas = new List<FechaCalendario>();
                        var reservas = dbContext.Reservas.Where(x => x.IDPropiedad == id && x.Estado.ToLower() == "aceptada").ToList();
                        if (reservas != null)
                        {
                            foreach (var res in reservas)
                            {
                                FechaCalendario reserva = new FechaCalendario();
                                reserva.Desde = res.FechaIngreso.Day;
                                reserva.Hasta = res.FechaEgreso.Day;
                                if (res.FechaIngreso < model.Calendario1Desde)
                                {
                                    //pertence algún mes anterior que no se muestra calendario
                                    reserva.DesdeMes = 0;
                                    int difMeses = res.FechaEgreso.Month - res.FechaIngreso.Month;
                                    reserva.HastaMes = reserva.DesdeMes + difMeses;
                                }
                                else if (res.FechaIngreso.IsDateBetween(model.Calendario1Desde, model.Calendario1Hasta))
                                {
                                    //pertenece al primer mes de los que se muestran
                                    reserva.DesdeMes = 1;
                                    int difMeses = res.FechaEgreso.Month - res.FechaIngreso.Month;
                                    reserva.HastaMes = reserva.DesdeMes + difMeses;
                                }
                                else if (res.FechaIngreso.IsDateBetween(model.Calendario2Desde, model.Calendario2Hasta))
                                {
                                    //pertenece al segundo mes de los que se muestran
                                    reserva.DesdeMes = 2;
                                    int difMeses = res.FechaEgreso.Month - res.FechaIngreso.Month;
                                    reserva.HastaMes = reserva.DesdeMes + difMeses;
                                }
                                listaReservadas.Add(reserva);
                            }
                            Session["ListaReservas"] = listaReservadas;
                        }
                        #endregion
                    }
                    #endregion
                }
            }
            else
                Response.Redirect("/Home/Index");
            return View(model);
        }

        public ActionResult Reserva(int id)
        {
            ReservaFrontViewModel model = new ReservaFrontViewModel();
            using (var dbContext = new ACHEEntities())
            {
                DateTime fechaHoy = DateTime.Now.Date;
                var aux = dbContext.Propiedades.Where(x => x.IDPropiedad == id).FirstOrDefault();

                var detalle = new DetalleViewModel
                {
                    IDPropiedad = aux.IDPropiedad,
                    IDZona = aux.IDZona,
                    Nombre = aux.Nombre,
                    Codigo = aux.Codigo,
                    NombreFriendly = aux.Nombre.ToFriendlyUrl(),
                    MinCantDias = aux.MinCantDias,
                    Descripcion = aux.DescripcionBreve,
                    Tipo = aux.Tipo == "d" ? "Departamento" : "Casa",
                    Zona = aux.Zonas.Nombre,
                    CantAmbientes = aux.CantAmbientes,
                    CantCamas = aux.CantCamas,
                    CantHuespedes = aux.CantHuespedes,
                    PrecioMes = aux.ListasPrecio.Precios.Any(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy) ? aux.ListasPrecio.Precios.Where(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy).FirstOrDefault().ValorMes : 0,
                    PrecioQuincena = aux.ListasPrecio.Precios.Any(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy) ? aux.ListasPrecio.Precios.Where(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy).FirstOrDefault().ValorQuincena : 0,
                    PrecioSemana = aux.ListasPrecio.Precios.Any(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy) ? aux.ListasPrecio.Precios.Where(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy).FirstOrDefault().ValorSemana : 0,
                    PrecioDia = aux.ListasPrecio.Precios.Any(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy) ? aux.ListasPrecio.Precios.Where(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy).FirstOrDefault().ValorDia : 0,
                    Servicios = aux.PropiedadesServicios.Select(y => y.Servicios).ToList(),
                    Fotos = aux.Fotos.OrderBy(y => y.Orden).Select(y => new FotosViewModel() { IDFoto = y.IDFoto, Foto = y.Foto + ".ashx?w=891&h=415" }).ToList(),
                    Video = aux.Video,
                    Latitud = aux.Latitud,
                    Longitud = aux.Longitud,
                };

                model.Propiedad = detalle;
                model.IDPropiedad = detalle.IDPropiedad;

                GetMeses(detalle);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Reserva(DetalleViewModel model)
        {
            ReservaFrontViewModel res = new ReservaFrontViewModel();
            if (model.Reserva != null)
                res = model.Reserva;
            if (model.IDPropiedad != null && model.IDPropiedad != 0)
                res.IDPropiedad = model.IDPropiedad;
            if (!string.IsNullOrEmpty(model.Nombre))
                res.Nombre = model.Nombre.ToFriendlyUrl();
            using (var dbContext = new ACHEEntities())
            {
                try
                {
                    DateTime fechaHoy = DateTime.Now.Date;
                    var aux = dbContext.Propiedades.Where(x => x.IDPropiedad == model.IDPropiedad).FirstOrDefault();

                    var propiedad = new DetalleViewModel
                    {
                        IDPropiedad = aux.IDPropiedad,
                        IDZona = aux.IDZona,
                        Nombre = aux.Nombre,
                        Codigo = aux.Codigo,
                        NombreFriendly = aux.Nombre.ToFriendlyUrl(),
                        MinCantDias = aux.MinCantDias,
                        Descripcion = aux.DescripcionBreve,
                        Tipo = aux.Tipo == "d" ? "Departamento" : "Casa",
                        Zona = aux.Zonas.Nombre,
                        CantAmbientes = aux.CantAmbientes,
                        CantCamas = aux.CantCamas,
                        CantHuespedes = aux.CantHuespedes,
                        PrecioMes = aux.ListasPrecio.Precios.Any(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy) ? aux.ListasPrecio.Precios.Where(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy).FirstOrDefault().ValorMes : 0,
                        PrecioQuincena = aux.ListasPrecio.Precios.Any(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy) ? aux.ListasPrecio.Precios.Where(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy).FirstOrDefault().ValorQuincena : 0,
                        PrecioSemana = aux.ListasPrecio.Precios.Any(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy) ? aux.ListasPrecio.Precios.Where(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy).FirstOrDefault().ValorSemana : 0,
                        PrecioDia = aux.ListasPrecio.Precios.Any(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy) ? aux.ListasPrecio.Precios.Where(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy).FirstOrDefault().ValorDia : 0,
                        Servicios = aux.PropiedadesServicios.Select(y => y.Servicios).ToList(),
                        Fotos = aux.Fotos.OrderBy(y => y.Orden).Select(y => new FotosViewModel() { IDFoto = y.IDFoto, Foto = y.Foto + ".ashx?w=891&h=415" }).ToList(),
                        Video = aux.Video,
                        Latitud = aux.Latitud,
                        Longitud = aux.Longitud,
                    };
                    if (propiedad != null)
                    {
                        #region similares
                        var similares = dbContext.Propiedades
                            .Include("Fotos")
                            .Include("ListasPrecio")
                            .Where(x => x.IDZona == propiedad.IDZona && x.IDPropiedad != propiedad.IDPropiedad && x.Activo)
                            .ToList()
                            .Select(x => new SimilaresViewModel()
                            {
                                IDPropiedad = x.IDPropiedad,
                                Nombre = x.Nombre,
                                NombreFriendly = x.Nombre.ToFriendlyUrl(),
                                Precio = x.ListasPrecio.Precios.Any(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy) ? x.ListasPrecio.Precios.Where(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy).FirstOrDefault().ValorMes : 0,
                                Zona = x.Zonas.Nombre,
                                Direccion = x.Direccion,
                                Foto = x.Fotos.FirstOrDefault().Foto,
                            }).ToList();
                        if (similares != null)
                            propiedad.Similares = similares;
                        #endregion

                        model = propiedad;
                        if (res.CantHuespedes > propiedad.CantHuespedes)
                            throw new Exception("La cantidad de huespedes debe ser menor o igual a la indicada en la propiedad");
                        res.Propiedad = propiedad;

                        GetMeses(propiedad);
                    }
                    //if (ModelState.IsValid == true) {
                    //if (res.FechaIngreso < DateTime.Now.Date)
                    //    throw new Exception("La fecha de ingreso debe ser mayor a la fecha de hoy");
                    //if (DateTime.Compare(res.FechaIngreso.Date, res.FechaEgreso.Date) >= 0)
                    //    throw new Exception("La fecha de egreso debe ser mayor a la fecha de ingreso");

                    res.IDPropiedad = res.IDPropiedad;
                    res.FechaIngreso = res.FechaIngreso;
                    //res.FechaEgreso = res.FechaEgreso;
                    res.CantHuespedes = res.CantHuespedes;
                    //}
                    //else throw new Exception("");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View("Detalle", model);
                }
            }
            ModelState.Clear();
            return View(res);
        }

        [HttpPost]
        public ActionResult Reservar(ReservaFrontViewModel model)
        {
            try
            {
                using (var dbContext = new ACHEEntities())
                {
                    //guardo la propiedad
                    DateTime fechaHoy = DateTime.Now.Date;
                    var aux = dbContext.Propiedades.Where(x => x.IDPropiedad == model.IDPropiedad).FirstOrDefault();

                    var propiedad = new DetalleViewModel
                    {
                        IDPropiedad = aux.IDPropiedad,
                        IDZona = aux.IDZona,
                        Nombre = aux.Nombre,
                        Codigo = aux.Codigo,
                        Descripcion = aux.DescripcionBreve,
                        Tipo = aux.Tipo == "d" ? "Departamento" : "Casa",
                        Zona = aux.Zonas.Nombre,
                        CantAmbientes = aux.CantAmbientes,
                        CantCamas = aux.CantCamas,
                        MinCantDias = aux.MinCantDias,
                        CantHuespedes = aux.CantHuespedes,
                        PrecioMes = aux.ListasPrecio.Precios.Any(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy) ? aux.ListasPrecio.Precios.Where(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy).FirstOrDefault().ValorMes : 0,
                        PrecioQuincena = aux.ListasPrecio.Precios.Any(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy) ? aux.ListasPrecio.Precios.Where(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy).FirstOrDefault().ValorQuincena : 0,
                        PrecioSemana = aux.ListasPrecio.Precios.Any(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy) ? aux.ListasPrecio.Precios.Where(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy).FirstOrDefault().ValorSemana : 0,
                        PrecioDia = aux.ListasPrecio.Precios.Any(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy) ? aux.ListasPrecio.Precios.Where(y => y.VigenciaDesde <= fechaHoy && y.VigenciaHasta >= fechaHoy).FirstOrDefault().ValorDia : 0,
                        Servicios = aux.PropiedadesServicios.Select(y => y.Servicios).ToList(),
                        Fotos = aux.Fotos.OrderBy(y => y.Orden).Select(y => new FotosViewModel() { IDFoto = y.IDFoto, Foto = y.Foto + ".ashx?Mode=Crop&w=415" }).Take(3).ToList(),
                        Video = aux.Video,
                        Latitud = aux.Latitud,
                        Longitud = aux.Longitud,
                    };

                    GetMeses(propiedad);

                    if (propiedad != null)
                        model.Propiedad = propiedad;
                    if (ModelState.IsValid)
                    {
                        //valido las fechas
                        if (model.FechaIngreso < fechaHoy)
                            throw new Exception("La fecha de ingreso debe ser mayor a la fecha de hoy");
                        //if (DateTime.Compare(model.FechaIngreso.Date, model.FechaEgreso.Date) >= 0)
                        //    throw new Exception("La fecha de egreso debe ser mayor a la fecha de ingreso");
                        //valido cant huespedes
                        if (model.CantHuespedes > model.Propiedad.CantHuespedes)
                            throw new Exception("La cantidad de huespedes debe ser menor o igual a la indicada en la propiedad");
                        //busco al cliente
                        Clientes cliente = new Clientes();
                        var existe = dbContext.Clientes.Any(x => x.Email.ToLower() == model.Cliente.Email);
                        if (existe)
                            cliente = dbContext.Clientes.Where(x => x.Email.ToLower() == model.Cliente.Email).FirstOrDefault();
                        else
                        {
                            //cliente no existe, lo creo
                            Clientes nuevo = new Clientes();
                            cliente.Nombre = model.Cliente.Nombre;
                            cliente.Apellido = model.Cliente.Apellido;
                            cliente.Email = model.Cliente.Email;
                            cliente.Telefono = model.Cliente.Telefono;
                            cliente.NroDocumento = model.Cliente.NroDocumento;
                            cliente.Pais = model.Cliente.Pais;
                            cliente.FechaAlta = DateTime.Now;

                            dbContext.Clientes.Add(cliente);
                        }
                        //verifico que no haya reserva en las fechas para la propiedad
                        var reservas = dbContext.Reservas.Where(x => x.IDPropiedad == model.IDPropiedad && x.Estado == "Aceptada").ToList();
                        if (reservas != null && reservas.Count() > 0)
                        {
                            foreach (var res in reservas)
                            {
                                if (model.FechaIngreso.IsDateBetween(res.FechaIngreso, res.FechaEgreso) || model.FechaIngreso.AddMonths(model.CantMeses).IsDateBetween(res.FechaIngreso, res.FechaEgreso))
                                {
                                    throw new Exception("La propiedad ya esta reservada entre las fechas ingresadas");
                                }
                            }
                        }
                        //throw new Exception("La propiedad ya esta reservada entre las fechas ingresadas");

                        //creo la reserva
                        Reservas nueva = new Reservas();
                        nueva.IDPropiedad = model.IDPropiedad;
                        nueva.IDCliente = cliente.IDCliente;
                        nueva.FechaIngreso = model.FechaIngreso;
                        nueva.FechaEgreso = model.FechaIngreso.AddMonths(model.CantMeses);
                        nueva.CantMeses = model.CantMeses;
                        nueva.FechaReserva = DateTime.Now;
                        decimal meses = model.CantMeses;
                        if (meses < propiedad.MinCantDias)
                            throw new Exception("La propiedad sólo se puede reservar a partir de " + propiedad.MinCantDias + " meses");
                        nueva.PrecioTotal = meses * propiedad.PrecioMes;
                        nueva.Estado = "Pendiente";
                        nueva.Observaciones = model.Observaciones;
                        nueva.TipoReserva = "Mensual";

                        dbContext.Reservas.Add(nueva);
                        dbContext.SaveChanges();
                    }
                    else
                        throw new Exception("");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Reserva", model);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EnviarContacto(string nombre, string apellido, string email, string telefono, string consulta, int idProp)
        {
            string result = string.Empty;
            ListDictionary datos = new ListDictionary();
            datos.Add("<NOMBRE>", nombre);
            datos.Add("<APELLIDO>", apellido);
            datos.Add("<EMAIL>", email);
            datos.Add("<TELEFONO>", telefono);
            datos.Add("<CONSULTA>", consulta);

            bool send = EmailHelper.SendMessage(EmailTemplate.Contacto, datos, ConfigurationManager.AppSettings["Email.To"], "JustLikeHome: Nuevo contacto desde la web");
            if (!send)
                result = "false";
            else
            {
                result = "true";
                try
                {
                    using (var dbContext = new ACHEEntities())
                    {
                        Contactos nuevo = new Contactos();
                        nuevo.Nombre = apellido + ", " + nombre;
                        nuevo.Email = email;
                        nuevo.Telefono = telefono;
                        nuevo.Mensaje = consulta;
                        nuevo.IDPropiedad = idProp;

                        dbContext.Contactos.Add(nuevo);
                        dbContext.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    result = "error";
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet); ;
        }

        [HttpPost]
        public ActionResult ConsultaPropiedad(string nombre, string email, string telefono, string mensaje)
        {
            string result = string.Empty;
            ListDictionary datos = new ListDictionary();
            datos.Add("<NOMBRE>", nombre);
            datos.Add("<EMAIL>", email);
            datos.Add("<TELEFONO>", telefono);
            datos.Add("<MENSAJE>", mensaje);

            bool send = EmailHelper.SendMessage(EmailTemplate.ConsultaPropiedad, datos, ConfigurationManager.AppSettings["Email.To"], "JustLikeHome: Nueva consulta por publicación de propiedad");
            if (!send)
                result = "false";
            else
                result = "true";
            return Json(result, JsonRequestBehavior.AllowGet); ;
        }

        private List<PropiedadesViewModel> GetAllPropiedades(ACHEEntities dbContext)
        {
            List<PropiedadesViewModel> result = dbContext.Propiedades
                    .Include("Fotos")
                    .Where(x => x.Activo)
                    .ToList()
                    .Select(x => new PropiedadesViewModel()
                    {
                        IDPropiedad = x.IDPropiedad,
                        Nombre = x.Nombre,
                        NombreFriendly = x.Nombre.ToFriendlyUrl(),
                        CantAmbientes = x.CantAmbientes,
                        CantHuespedes = x.CantHuespedes,
                        MinCantDias = x.MinCantDias,
                        IDZona = x.IDZona,
                        Zona = x.Zonas.Nombre,
                        IDListaPrecio = x.IDListaPrecio,
                        Fotos = x.Fotos.OrderBy(y => y.Orden).Select(y => new FotosViewModel() { IDFoto = y.IDFoto, Foto = y.Foto + ".ashx?Mode=Crop&w=540&h=330" }).ToList(),
                        CantFotos = x.Fotos.Count(),
                        Direccion = x.Direccion,
                        Descripcion = x.DescripcionBreve,
                        Latitud = x.Latitud,
                        Longitud = x.Longitud,
                    }).ToList();
            return result;
        }

        private List<ZonasFrontViewModel> GetAllZonas(ACHEEntities dbContext)
        {
            List<ZonasFrontViewModel> zonas = new List<ZonasFrontViewModel>();
            zonas = dbContext.Zonas.Where(x => x.Propiedades.Any(y => y.Activo)).OrderBy(x => x.Nombre)
                    .Select(x => new ZonasFrontViewModel()
                    {
                        IDZona = x.IDZona,
                        Nombre = x.Nombre,
                    }).ToList();
            return zonas;
        }

        private void GetMeses(DetalleViewModel propiedad)
        {
            List<MesViewModel> meses = new List<MesViewModel>();
            if (propiedad != null)
            {
                for (int i = propiedad.MinCantDias; i <= 6; i++)
                {
                    MesViewModel mes = new MesViewModel() { Texto = i.ToString(), Valor = i.ToString() };
                    meses.Add(mes);
                }
            }
            ViewBag.Meses = meses;
        }

        [HttpPost]
        public ActionResult GetReservas()
        {
            string result = string.Empty;
            List<FechaCalendario> aux = new List<FechaCalendario>();
            try
            {
                var listaReservas = Session["ListaReservas"];
                aux = (List<FechaCalendario>)listaReservas;
            }
            catch (Exception ex)
            {
                result = string.IsNullOrEmpty(ex.InnerException.Message) ? ex.Message : ex.InnerException.Message;
            }
            return Json(aux, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetMarkers()
        {
            string result = string.Empty;
            List<MarkerViewModel> aux = new List<MarkerViewModel>();
            try
            {
                var lista = Session["Lista"];
                aux = (List<MarkerViewModel>)lista;
            }
            catch (Exception ex)
            {
                result = string.IsNullOrEmpty(ex.InnerException.Message) ? ex.Message : ex.InnerException.Message;
            }
            return Json(aux, JsonRequestBehavior.AllowGet);
        }

        private class MesViewModel
        {
            public string Texto { get; set; }
            public string Valor { get; set; }
        }
    }
}