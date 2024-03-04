using Microsoft.EntityFrameworkCore;
using Odontologia_Backend.Data;

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
                        pedido_fecha = x.pedido.Ped_Fecha.ToString("dd/MM/yyyy")
                    })
                    .ToListAsync<Object>();

                return listaDePedidos;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
