﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AriasRomanJonathan_Tarea2
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EncuestaTecnologiasEntities : DbContext
    {
        public EncuestaTecnologiasEntities()
            : base("name=EncuestaTecnologiasEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Encuestas> Encuestas { get; set; }
        public virtual DbSet<LenguajesProgramacion> LenguajesProgramacion { get; set; }
        public virtual DbSet<Paises> Paises { get; set; }
        public virtual DbSet<Reportes> Reportes { get; set; }
        public virtual DbSet<RolesTI> RolesTI { get; set; }
    }
}
