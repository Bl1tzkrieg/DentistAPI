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
        public async Task<List<Venta>> ObtenerVentas()
        {
            try
            {
                return await this.context_Mysql.ventas.ToListAsync();
            }
            catch (Exception ex)
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
