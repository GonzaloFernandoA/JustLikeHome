//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ACHE.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Propiedades
    {
        public Propiedades()
        {
            this.Contactos = new HashSet<Contactos>();
            this.Fotos = new HashSet<Fotos>();
            this.PropiedadesServicios = new HashSet<PropiedadesServicios>();
            this.Reservas = new HashSet<Reservas>();
        }
    
        public int IDPropiedad { get; set; }
        public int IDZona { get; set; }
        public int IDListaPrecio { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public int Calificacion { get; set; }
        public int MinCantDias { get; set; }
        public string Direccion { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public bool Destacado { get; set; }
        public int Metros { get; set; }
        public int CantAmbientes { get; set; }
        public int CantHuespedes { get; set; }
        public int CantCamas { get; set; }
        public string Video { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string DescripcionBreve { get; set; }
    
        public virtual ICollection<Contactos> Contactos { get; set; }
        public virtual ICollection<Fotos> Fotos { get; set; }
        public virtual ListasPrecio ListasPrecio { get; set; }
        public virtual Zonas Zonas { get; set; }
        public virtual ICollection<PropiedadesServicios> PropiedadesServicios { get; set; }
        public virtual ICollection<Reservas> Reservas { get; set; }
    }
}
