using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACHE.Model {

    public class PropiedadesViewModel {
        public int IDPropiedad { get; set; }
        public int IDZona { get; set; }
        public string Zona { get; set; }
        public int IDListaPrecio { get; set; }
        public string ListaPrecio { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string NombreFriendly { get; set; }
        public string Tipo { get; set; }
        public decimal Precio { get; set; }
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
        public int CantFotos { get; set; }
    }
}
