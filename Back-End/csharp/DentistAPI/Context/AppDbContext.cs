using DentistAPI.Models;
using DentistAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class AppDbContext : DbContext
{

     //public string DbPath { get; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {

    }

    public DbSet<acceso> acceso { get; set; }
    public DbSet<caso> caso { get; set; }
    public DbSet<cita> cita { get; set; }
    public DbSet<historia> historia { get; set; }
    public DbSet<historialcompraventa> historialcompraventa { get; set; }
    public DbSet<internaoperacionitem> internaoperacionitem { get; set; }
    public DbSet<inventario> inventario { get; set; }
    public DbSet<item> item { get; set; }
    public DbSet<operacion> operacion { get; set; }
    public DbSet<perfil> perfil { get; set; }
    public DbSet<permisos> permisos { get; set; }
    public DbSet<persona> persona { get; set; }



    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Data Source=C:\Users\ATKCPS\Desktop\Desarrollo de Sistemas\OdontologyDB.sqlite");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<persona_DTO>().HasNoKey();

    }
}
