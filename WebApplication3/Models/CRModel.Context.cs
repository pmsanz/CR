﻿

//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------


namespace WebApplication3.Models
{

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class CREntities1 : DbContext
{
    public CREntities1()
        : base("name=CREntities1")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }

    public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }

    public virtual DbSet<CATEGORIAS> CATEGORIAS { get; set; }

    public virtual DbSet<CLIENTES> CLIENTES { get; set; }

    public virtual DbSet<CUENTAS_CLIENTES> CUENTAS_CLIENTES { get; set; }

    public virtual DbSet<CUENTAS_PROVEEDORES> CUENTAS_PROVEEDORES { get; set; }

    public virtual DbSet<ESTADOS_PEDIDOS> ESTADOS_PEDIDOS { get; set; }

    public virtual DbSet<MOVIMIENTOS_CLIENTES> MOVIMIENTOS_CLIENTES { get; set; }

    public virtual DbSet<MOVIMIENTOS_PROVEEDORES> MOVIMIENTOS_PROVEEDORES { get; set; }

    public virtual DbSet<PEDIDOS_CLIENTES> PEDIDOS_CLIENTES { get; set; }

    public virtual DbSet<PEDIDOS_CLIENTES_DETALLES> PEDIDOS_CLIENTES_DETALLES { get; set; }

    public virtual DbSet<PEDIDOS_PROVEEDORES> PEDIDOS_PROVEEDORES { get; set; }

    public virtual DbSet<PEDIDOS_PROVEEDORES_DETALLES> PEDIDOS_PROVEEDORES_DETALLES { get; set; }

    public virtual DbSet<PRODUCTOS> PRODUCTOS { get; set; }

    public virtual DbSet<PROVEEDORES> PROVEEDORES { get; set; }

    public virtual DbSet<PROVINCIAS> PROVINCIAS { get; set; }

    public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

    public virtual DbSet<TIPO_FACTURACIONES> TIPO_FACTURACIONES { get; set; }

    public virtual DbSet<TIPO_IVA> TIPO_IVA { get; set; }

    public virtual DbSet<TIPO_PAGOS> TIPO_PAGOS { get; set; }

}

}

