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

        public async Task<List<Compra>> ObtenerCompras()
        {
            try
            {
                return await this.context_Mysql.compras.ToListAsync();
            }
            catch ( Exception ex )
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
