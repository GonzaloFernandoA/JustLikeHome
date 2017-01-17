using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ACHE.Model;
using PagedList;

namespace ACHE.Models {
    public class IndexViewModel {
        public List<PropiedadesViewModel> Propiedades { get; set; }
        public StaticPagedList<PropiedadesViewModel> PropiedadesLista { get; set; }
        public List<PropiedadesViewModel> Destacadas { get; set; }
        public List<ZonasFrontViewModel> Zonas { get; set; }
        public int IDZona { get; set; }
        public int CantAmbientes { get; set; }
        public int CantHuespedes { get; set; }
        public DateTime Desde { get; set; }
        public int CantMeses { get; set; }
        public int Orden { get; set; }
    }

    public class ZonasFrontViewModel {
        public int IDZona { get; set; }
        public string Nombre { get; set; }
    }
}