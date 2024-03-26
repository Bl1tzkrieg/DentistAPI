using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Odontologia_Backend.Data;
using Odontologia_Backend.DTOs;
using Odontologia_Backend.Models;

namespace Odontologia_Backend.Class
{
    public class HandlerOrders
    {
        private readonly Context_MySql context_Mysql;

        public HandlerOrders(Context_MySql dbMysql)
        {
            this.context_Mysql = dbMysql;
        }

        public async Task<List<Object>> ObtenerPedidos()
        {
            try
            {
                List<Object> listaDePedidos = await this.context_Mysql.pedido_insumo
                    .Join(this.context_Mysql.usuarios, i => i.Ped_Usu_Id, u => u.Usu_Id,
                    (pedido, usuario) => new { pedido, usuario })
                    .Where(q => q.pedido.Ped_Status.Equals("PENDIENTE"))
                    .Select( x => new
                    {
                        pedido_id = x.pedido.Ped_Id,
                        pedido_cantidad = x.pedido.Ped_Cant,
                        pedido_descripcion = x.pedido.Ped_Descripcion,
                        pedido_username = x.usuario.Usu_UserName,
                        pedido_fecha = x.pedido.Ped_Fecha.ToString("dd/MM/yyyy"),
                        pedido_status = x.pedido.Ped_Status
                    })
                    .ToListAsync<Object>();

                return listaDePedidos;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Object?> ObtenerPedido(int id_pedido)
        {
            try
            {
                return await this.context_Mysql.pedido_insumo
                    .Join(this.context_Mysql.usuarios, i => i.Ped_Usu_Id, u => u.Usu_Id,
                    (pedido, usuario) => new { pedido, usuario })
                    .Where( o => o.pedido.Ped_Id.Equals(id_pedido))
                    .Select(x => new
                    {
                        pedido_id = x.pedido.Ped_Id,
                        pedido_cantidad = x.pedido.Ped_Cant,
                        pedido_descripcion = x.pedido.Ped_Descripcion,
                        pedido_username = x.usuario.Usu_UserName,
                        pedido_fecha = x.pedido.Ped_Fecha.ToString("dd/MM/yyyy"),
                        pedido_status = x.pedido.Ped_Status
                    })
                    .FirstOrDefaultAsync<Object>();
            }
            catch ( Exception )
            {
                throw;
            }
        }

        public async Task<(int,string)> CancelarPedido(int id_pedido)
        {
            try
            {
                Order? statusPedido = await this.context_Mysql.pedido_insumo.FirstOrDefaultAsync(q => q.Ped_Id.Equals(id_pedido));

                if (statusPedido == null) return (0, "El pedido solicitado no esta registrado.");

                statusPedido.Ped_Status = "CANCELADO";

                await this.context_Mysql.SaveChangesAsync();

                return (1, "Pedido cancelado correctamente");
            }
            catch (Exception )
            {
                throw;
            }
        }

        public async Task<(int,string)> RegistrarPedido(Order_DTO pedido)
        {
            try
            {

                await this.context_Mysql.pedido_insumo.AddAsync(new Order
                {
                    Ped_Cant = pedido.pedido_cantidad,
                    Ped_Descripcion = pedido.pedido_descripcion,
                    Ped_Usu_Id = pedido.pedido_usuario_id,
                    Ped_Fecha = DateTime.Now,
                    Ped_Status = "PENDIENTE"
                });

                await this.context_Mysql.SaveChangesAsync();

                return (1, "Pedido registrado con Exito");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<(int,string)> ActualizarPedido(OrderUpdate_DTO pedido)
        {
            try
            {
                Order? statusPedido = await this.context_Mysql.pedido_insumo.FirstOrDefaultAsync(x => x.Ped_Id.Equals(pedido.pedido_id));

                if (statusPedido == null) return (0, "El pedido solicitado no existe");

                statusPedido.Ped_Cant = pedido.pedido_cantidad;
                statusPedido.Ped_Descripcion = pedido.pedido_descripcion;

                await this.context_Mysql.SaveChangesAsync();

                return (1, "Pedido Actualizado con exito!");
            }
            catch ( Exception )
            {
                throw;
            }
        }

        public async Task<(int,string)> FacturarPedido(Invoice_Order_DTO pedido)
        {
            using (IDbContextTransaction transactionMySql = this.context_Mysql.Database.BeginTransaction())
            {
                try
                {
                    // obtener el pedido de pedidos_insumos
                    Order? statusPedido = await this.context_Mysql.pedido_insumo.FirstOrDefaultAsync(x => x.Ped_Id.Equals(pedido.pedido_id));

                    if (statusPedido == null) return (0, "El pedido no se encuentra registrado.");

                    if (statusPedido.Ped_Status.Equals("FACTURADO")) return (0, "El pedido ya ha sido facturado");

                    Compra newShopping = new()
                    {
                        Comp_Usu_Id = pedido.usuario_id,
                        Comp_Fecha = DateTime.Now,
                        Comp_Ped_Id = pedido.pedido_id,
                        Comp_NroFactura = pedido.numero_factura,
                        Comp_Total = pedido.monto_total
                    };

                    await this.context_Mysql.compras.AddAsync(newShopping);
                    await this.context_Mysql.SaveChangesAsync();

                    statusPedido.Ped_Status = "FACTURADO";
                    await this.context_Mysql.SaveChangesAsync();

                    // si no se han levantado excepciones o errores por incongruencias etc. procedemos a hacer el commit.
                    await transactionMySql.CommitAsync();
                    return (1, "Pedido facturado con exito");
                }
                catch (Exception)
                {
                    await transactionMySql.RollbackAsync();
                    throw;
                }
            }
        }

        public int ObtenerCantidadDeInsumosPorEstado(int tipo)
        {
            try
            {
                string[] statusInsumo = { "FACTURADO", "CANCELADO" };

                return this.context_Mysql.pedido_insumo.Count( x => x.Ped_Status.Equals(statusInsumo[tipo-1]));
            }
            catch ( Exception )
            {
                throw;
            }
        }

    }
}
