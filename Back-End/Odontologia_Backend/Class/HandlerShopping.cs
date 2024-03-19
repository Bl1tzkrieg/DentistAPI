using Microsoft.EntityFrameworkCore;
using Odontologia_Backend.Data;
using Odontologia_Backend.DTOs;
using Odontologia_Backend.Models;

namespace Odontologia_Backend.Class
{
    public class HandlerShopping
    {
        private readonly Context_MySql context_Mysql;

        public HandlerShopping( Context_MySql dbMysql )
        {
            this.context_Mysql = dbMysql;
        }

        public async Task<List<Object>> ObtenerCompras()
        {
            try
            {
                return await this.context_Mysql.compras
                    .Join(this.context_Mysql.pedido_insumo, c => c.Comp_Ped_Id, p => p.Ped_Id, (compra,pedido) => new { compra, pedido })
                    .Join(this.context_Mysql.usuarios, o => o.pedido.Ped_Usu_Id, u => u.Usu_Id, ( p2, usuario) => new { p2.compra, p2.pedido, usuario })
                    .Where( q => q.pedido.Ped_Status.Equals("FACTURADO"))
                    .Select(x => new
                    {
                        compra_id = x.compra.Comp_Id,
                        compra_usuario = x.usuario.Usu_UserName,
                        compra_fecha = x.compra.Comp_Fecha.ToString("dd/MM/yyyy"),
                        compra_pedido_id = x.compra.Comp_Ped_Id,
                        compra_numero_factura = x.compra.Comp_NroFactura,
                        pedido_cantidad = x.pedido.Ped_Cant,
                        pedido_fecha_registro = x.pedido.Ped_Fecha,
                        pedido_status = x.pedido.Ped_Status,
                        pedido_descripcion = x.pedido.Ped_Descripcion,
                        compra_total = x.compra.Comp_Total,
                    }).ToListAsync<Object>();
            }
            catch ( Exception )
            {
                throw;
            }
        }

        public async Task<Object?> ObtenerCompra( int id )
        {
            try
            { 
                return await this.context_Mysql.compras
                    .Join(this.context_Mysql.pedido_insumo, c => c.Comp_Ped_Id, p => p.Ped_Id, (compra, pedido) => new { compra, pedido })
                    .Join(this.context_Mysql.usuarios, o => o.pedido.Ped_Usu_Id, u => u.Usu_Id, (p2, usuario) => new { p2.compra, p2.pedido, usuario })
                    .Where(x => x.compra.Comp_Ped_Id == id)
                    .Select(x => new
                    {
                        compra_id = x.compra.Comp_Id,
                        compra_usuario = x.usuario.Usu_UserName,
                        compra_fecha = x.compra.Comp_Fecha.ToString("dd/MM/yyyy"),
                        compra_pedido_id = x.compra.Comp_Ped_Id,
                        compra_numero_factura = x.compra.Comp_NroFactura,
                        pedido_cantidad = x.pedido.Ped_Cant,
                        pedido_fecha_registro = x.pedido.Ped_Fecha.ToString("dd/MM/yyyy"),
                        pedido_status = x.pedido.Ped_Status,
                        pedido_descripcion = x.pedido.Ped_Descripcion,
                        compra_total = x.compra.Comp_Total,
                    }).FirstOrDefaultAsync<Object>();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<(int,string)> RegistrarCompra( Buys_DTO compra )
        {
            try
            {
                await this.context_Mysql.compras.AddAsync(new Compra
                {
                    Comp_Usu_Id = compra.id_usuario,
                    Comp_Ped_Id = compra.id_pedido,
                    Comp_Fecha = DateTime.Now,
                    Comp_NroFactura = compra.nro_factura,
                    Comp_Total = compra.total,
                });

                await this.context_Mysql.SaveChangesAsync();

                return (1, "Compra registrada con exito!");
            }
            catch (Exception ex )
            {
                throw;
            }
        }
    }
}
