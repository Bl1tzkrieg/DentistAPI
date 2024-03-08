using Microsoft.EntityFrameworkCore;
using Odontologia_Backend.DTOs;
using Odontologia_Backend.Models;

namespace Odontologia_Backend.Data
{
    public class Context_MySql : DbContext
    {
        public Context_MySql(DbContextOptions<Context_MySql> options) : base(options) { }

        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Rol> roles { get; set; }
        public DbSet<Empleado> empleados { get; set; }
        public DbSet<Cliente> clientes { get; set; }
        public DbSet<Venta> ventas { get; set; }
        public DbSet<Ventas_detalle> ventas_detalles { get; set; }
        public DbSet<Servicio> servicios { get; set; }
        public DbSet<Compra> compras { get; set; }
        public DbSet<Consulta> consultas { get; set; }
        public DbSet<Order> pedido_insumo { get; set; }

    }

}
