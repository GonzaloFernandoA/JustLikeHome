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
    
    public partial class Fotos
    {
        public int IDFoto { get; set; }
        public string Foto { get; set; }
        public int Orden { get; set; }
        public int IDPropiedad { get; set; }
    
        public virtual Propiedades Propiedades { get; set; }
    }
}
