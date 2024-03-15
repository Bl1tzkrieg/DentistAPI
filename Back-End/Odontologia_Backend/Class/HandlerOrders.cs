using Microsoft.EntityFrameworkCore;
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
                    .Where(q => true)
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

        public async Task<(int, string)> RegistrarPedido( Order_DTO pedido )
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

        public async Task<(int, string)> ActualizarPedido( OrderUpdate_DTO pedido )
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

    }
}
