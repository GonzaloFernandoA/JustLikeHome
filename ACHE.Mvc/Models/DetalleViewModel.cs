using ACHE.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACHE.Models {
    public class DetalleViewModel {
        public int IDPropiedad { get; set; }
        public int IDZona { get; set; }
        public string Zona { get; set; }
        public int IDListaPrecio { get; set; }
        public string ListaPrecio { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string NombreFriendly { get; set; }
        public string Tipo { get; set; }
        public decimal PrecioMes { get; set; }
        public decimal PrecioQuincena { get; set; }
        public decimal PrecioSemana { get; set; }
        public decimal PrecioDia { get; set; }
        public int Calificacion { get; set; }
        public int MinCantDias { get; set; }
        public string Direccion { get; set; }
        public string Descripcion { get; set; }
        public string Activo { get; set; }
        public string Destacado { get; set; }
        public int Metros { get; set; }
        public int CantAmbientes { get; set; }
        public int CantHuespedes { get; set; }
        public int CantCamas { get; set; }
        public string Video { get; set; }
        public string Longitud { get; set; }
        public string Latitud { get; set; }
        public List<FotosViewModel> Fotos { get; set; }
        public List<Servicios> Servicios { get; set; }
        public List<SimilaresViewModel> Similares { get; set; }
        public ReservaFrontViewModel Reserva { get; set; }
        public DateTime Calendario1Desde { get; set; }
        public DateTime Calendario1Hasta { get; set; }
        public DateTime Calendario2Desde { get; set; }
        public DateTime Calendario2Hasta { get; set; }
        public List<FechaCalendario> Reservadas { get; set; }
    }

    public class SimilaresViewModel {
        public int IDPropiedad { get; set; }
        public string Nombre { get; set; }
        public string NombreFriendly { get; set; }
        public decimal Precio { get; set; }
        public string Foto { get; set; }
        public string Direccion { get; set; }
        public string Zona { get; set; }
    }

    public class FechaCalendario {
        public int Desde { get; set; }
        public int Hasta { get; set; }
        public int DesdeMes { get; set; }
        public int HastaMes { get; set; }
    }
}