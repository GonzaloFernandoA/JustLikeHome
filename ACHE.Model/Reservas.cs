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
    
    public partial class Reservas
    {
        public int IDReserva { get; set; }
        public int IDPropiedad { get; set; }
        public string TipoReserva { get; set; }
        public string Estado { get; set; }
        public System.DateTime FechaIngreso { get; set; }
        public System.DateTime FechaEgreso { get; set; }
        public string Observaciones { get; set; }
        public int IDCliente { get; set; }
        public System.DateTime FechaReserva { get; set; }
        public decimal PrecioTotal { get; set; }
        public int CantMeses { get; set; }
    
        public virtual Clientes Clientes { get; set; }
        public virtual Propiedades Propiedades { get; set; }
    }
}