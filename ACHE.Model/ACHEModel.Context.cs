﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ACHEEntities : DbContext
    {
        public ACHEEntities()
            : base("name=ACHEEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<Contactos> Contactos { get; set; }
        public virtual DbSet<Fotos> Fotos { get; set; }
        public virtual DbSet<ListasPrecio> ListasPrecio { get; set; }
        public virtual DbSet<Precios> Precios { get; set; }
        public virtual DbSet<Propiedades> Propiedades { get; set; }
        public virtual DbSet<PropiedadesServicios> PropiedadesServicios { get; set; }
        public virtual DbSet<Reservas> Reservas { get; set; }
        public virtual DbSet<Servicios> Servicios { get; set; }
        public virtual DbSet<Zonas> Zonas { get; set; }
    }
}