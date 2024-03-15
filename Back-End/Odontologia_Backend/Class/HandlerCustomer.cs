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
        public async Task<List<Object>> ObtenerClientes()
        {
            try
            {
                return await this.context_Mysql.clientes
                    .Where(c => true)
                    .Select( c => new { 
                        identificador = c.Cli_Id,
                        identificacion = c.Cli_Cedula,
                        nombre_completo = string.Concat($"{c.Cli_Nombre1} {c.Cli_Nombre2}", $" {c.Cli_Apellido1} {(string.IsNullOrEmpty(c.Cli_Apellido2) ? "" : c.Cli_Apellido2)}"),
                        cliente_status = c.Cli_Status,
                    })
                    .ToListAsync<Object>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Object?> ObtenerCliente( string cedula )
        {
            try
            {
                return await this.context_Mysql.clientes
                    .Where( x => x.Cli_Cedula.Equals( cedula ) )
                    .Select( c => new
                    {
                        cliente_id = c.Cli_Id,
                        cliente_nombre = c.Cli_Nombre1,
                        cliente_apellido = c.Cli_Apellido1,
                        cliente_cedula = c.Cli_Cedula,
                        cliente_status = c.Cli_Status
                    })
                    .FirstOrDefaultAsync<Object>();
            }
            catch ( Exception )
            {
                throw;
            }
        }

        
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
