using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Odontologia_Backend.Data;
using Odontologia_Backend.Models;

namespace Odontologia_Backend.Class
{
    public class HandlerSales
    {
        private readonly Context_MySql context_Mysql;

        public HandlerSales(Context_MySql dbMysql)
        {
            this.context_Mysql = dbMysql;
        }

        /// <summary>
        /// Devuelve una lista de todas las ventas realizadas
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<Object>> ObtenerVentas()
        {
            try
            {
                return await this.context_Mysql.ventas
                    .Join(this.context_Mysql.usuarios, venta => venta.Venta_Usu_Id, usuario => usuario.Usu_Id, (venta,usuario) => new { venta, usuario } )
                    .Join(this.context_Mysql.clientes, v2 => v2.venta.Venta_Cli_Id, cliente => cliente.Cli_Id, (v2,cliente) => new { v2.venta, v2.usuario, cliente })
                    .Join(this.context_Mysql.ventas_detalle, v3 => v3.venta.Venta_Vd_Id, vd => vd.Vd_Id, (v3, venta_detalle) => new { v3.venta, v3.cliente, v3.usuario, venta_detalle })
                    .Join(this.context_Mysql.tipo_pago, vd => vd.venta_detalle.Vd_Id, tp => tp.Tp_Id, (vd2, tipo_pago) => new { vd2.venta, vd2.cliente, vd2.usuario, vd2.venta_detalle, tipo_pago})
                    .Select( x => new
                    {
                        venta_id = x.venta.Venta_Id,
                        venta_nombre_cliente = x.cliente.Cli_Nombre1,
                        venta_apellido_cliente = x.cliente.Cli_Apellido1,
                        venta_cedula_cliente = x.cliente.Cli_Cedula,
                        venta_username = x.usuario.Usu_UserName,
                        venta_numero_factura = x.venta.Venta_NroFactura,
                        venta_fecha = x.venta.Venta_FecGen.ToString("dd/MM/yyyy"),
                        venta_tipo_pago = x.tipo_pago.Tp_Descripcion,
                        venta_total = x.venta_detalle.Vd_Total
                    })
                    .ToListAsync<Object>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Object?> ObtenerVenta( int id )
        {
            try
            {
                return await this.context_Mysql.ventas
                    .Join(this.context_Mysql.usuarios, venta => venta.Venta_Usu_Id, usuario => usuario.Usu_Id, (venta, usuario) => new { venta, usuario })
                    .Join(this.context_Mysql.clientes, v2 => v2.venta.Venta_Cli_Id, cliente => cliente.Cli_Id, (v2, cliente) => new { v2.venta, v2.usuario, cliente })
                    .Join(this.context_Mysql.ventas_detalle, v3 => v3.venta.Venta_Vd_Id, vd => vd.Vd_Id, (v3, venta_detalle) => new { v3.venta, v3.cliente, v3.usuario, venta_detalle })
                    .Join(this.context_Mysql.tipo_pago, vd => vd.venta_detalle.Vd_Id, tp => tp.Tp_Id, (vd2, tipo_pago) => new { vd2.venta, vd2.cliente, vd2.usuario, vd2.venta_detalle, tipo_pago })
                    .Where( x => x.venta.Venta_Id.Equals(id))
                    .Select(x => new
                    {
                        venta_id = x.venta.Venta_Id,
                        venta_nombre_cliente = x.cliente.Cli_Nombre1,
                        venta_apellido_cliente = x.cliente.Cli_Apellido1,
                        venta_cedula_cliente = x.cliente.Cli_Cedula,
                        venta_username = x.usuario.Usu_UserName,
                        venta_numero_factura = x.venta.Venta_NroFactura,
                        venta_fecha = x.venta.Venta_FecGen.ToString("dd/MM/yyyy"),
                        venta_tipo_pago = x.tipo_pago.Tp_Descripcion,
                        venta_total = x.venta_detalle.Vd_Total
                    })
                    .FirstOrDefaultAsync<Object>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Registra una nueva venta en la base de datos
        /// </summary>
        /// <param name="venta"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<(int,string)> RegistrarVenta( Venta venta)
        {
            using (IDbContextTransaction transactionMySql = this.context_Mysql.Database.BeginTransaction())
            {
                try
                {
                    await this.context_Mysql.ventas.AddAsync(venta);
                    await this.context_Mysql.SaveChangesAsync();
                    await transactionMySql.CommitAsync();
                    return (1, "Venta registrada con exito");
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

    }
}
