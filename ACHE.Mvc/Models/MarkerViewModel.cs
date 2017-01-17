using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACHE.Models
{
    public class MarkerViewModel
    {
        public string Nombre { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string Descripcion { get; set; }
        public string Foto { get; set; }
        public string Link { get; set; }
        public int Orden { get; set; }
    }
}