﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EMMA.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EMMAEntities : DbContext
    {
        public EMMAEntities()
            : base("name=EMMAEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ChamCong> ChamCong { get; set; }
        public virtual DbSet<CHUCVU> CHUCVU { get; set; }
        public virtual DbSet<HOPDONGLAODONG> HOPDONGLAODONG { get; set; }
        public virtual DbSet<LUONG> LUONG { get; set; }
        public virtual DbSet<NHANVIEN> NHANVIEN { get; set; }
        public virtual DbSet<PHONGBAN> PHONGBAN { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
