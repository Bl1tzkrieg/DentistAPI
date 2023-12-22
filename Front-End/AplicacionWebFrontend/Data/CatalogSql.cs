using AplicacionWebFrontend.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace AplicacionWebFrontend.Data
{
    public class CatalogSql : DbContext
    {
        protected static string nombreConexion = "name=DbAcceso";

        public CatalogSql() : base(nombreConexion) 
        {
            //Deshabilitamos setInitializer para que no haga nada de inicializacion
            Database.SetInitializer<CatalogSql>(null);
        }

        // Entidades
        public DbSet<Usuario> USUARIO { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Con esto configura que los nombres de las tablas son exactamente igual a los de las clases.
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Con esto configura que todos los campos string NO son unicode osea se traducen a varchar y no a nvarchar, si no se hace esto se demora full en cualquier SQL
            modelBuilder.Properties<string>().Configure(c => c.IsUnicode(false));
            // Esta prodiedad properties es propia de entityFramework library version 6.2
        }

    }
}