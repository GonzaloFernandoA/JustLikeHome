using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACHE.Model {
    public class ReservasViewModel {
        public int IDReserva { get; set; }
        public int IDPropiedad { get; set; }
        public string Propiedad { get; set; }
        public int IDCliente { get; set; }
        public string Cliente { get; set; }
        public string TipoReserva { get; set; }
        public string Estado { get; set; }
        public DateTime FechaReserva { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaEgreso { get; set; }
        public int CantMeses { get; set; }
        public string Observaciones { get; set; }
    }
}
