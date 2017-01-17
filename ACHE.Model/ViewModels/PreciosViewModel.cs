using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACHE.Model{
    public class PreciosViewModel {
        public int IDPrecio { get; set; }
        public int IDListaPrecio { get; set; }
        public string Nombre { get; set; }
        public string ListaPrecio { get; set; }
        public decimal ValorDia { get; set; }
        public decimal ValorSemana { get; set; }
        public decimal ValorQuincena { get; set; }
        public decimal ValorMes { get; set; }
        public DateTime VigenciaDesde { get; set; }
        public DateTime VigenciaHasta { get; set; }
    }
}
