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
    
    public partial class Servicios
    {
        public Servicios()
        {
            this.PropiedadesServicios = new HashSet<PropiedadesServicios>();
        }
    
        public int IDServicio { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
    
        public virtual ICollection<PropiedadesServicios> PropiedadesServicios { get; set; }
    }
}
