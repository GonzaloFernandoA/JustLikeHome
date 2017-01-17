using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACHE.Model;

namespace ACHE.Models {
    public class ListadoViewModel {
        public List<PropiedadesViewModel> Propiedades { get; set; }
        public List<ZonasViewModel> Zonas { get; set; }
        public int IDZona { get; set; }
        public int CantAmbientes { get; set; }
        public int CantHuespedes { get; set; }
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
    }
}