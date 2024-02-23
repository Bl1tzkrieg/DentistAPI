using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Odontologia_Backend.Data;
using Odontologia_Backend.Models;
using System.Data.Common;

namespace Odontologia_Backend.Class
{
    public class HandlerCustomer
    {
        private readonly Context_MySql context_Mysql;

        public HandlerCustomer(Context_MySql dbMysql)
        {
            this.context_Mysql = dbMysql;
        }

        /// <summary>
        /// Obtiene un cliente segun la cedula enviada
        /// </summary>
        /// <param name="cedula"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Cliente?> ObtenerCliente(string cedula)
        {
            try
            {
                return await this.context_Mysql.clientes.FirstOrDefaultAsync( x => x.Cli_Cedula.Equals(cedula));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Registra un nuevo cliente.
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<(int,string)> RegistrarCliente( Cliente cliente )
        {
            using (IDbContextTransaction transactionMySql = this.context_Mysql.Database.BeginTransaction())
            {
                try
                {
                    await this.context_Mysql.clientes.AddAsync(cliente);
                    await this.context_Mysql.SaveChangesAsync();
                    await transactionMySql.CommitAsync();
                    return (1, "Cliente registrado con exito");
                }
                catch (Exception)
                {
                    await transactionMySql.RollbackAsync();
                    throw;
                }
                
            }

        }




    }// Fin clase HandlerCustomer
}
