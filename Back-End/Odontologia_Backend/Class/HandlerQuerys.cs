using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Odontologia_Backend.Data;
using Odontologia_Backend.Models;

namespace Odontologia_Backend.Class
{
    public class HandlerQuerys
    {
        private readonly Context_MySql context_Mysql;

        public HandlerQuerys(Context_MySql dbMysql) 
        { 
            this.context_Mysql = dbMysql;
        }

        /// <summary>
        /// Devuelve una lista de consultas segun el estatus ingresado.
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<Consulta>> ObtenerConsultas(string status)
        {
            try
            {
                return await this.context_Mysql.consultas.Where(x => x.Consult_Status.Equals(status)).ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Registra una nueva consulta en la base de datos
        /// </summary>
        /// <param name="consulta"></param>
        /// <returns></returns>
        public async Task<(int,string)> RegistrarConsulta(Consulta consulta)
        {
            using (IDbContextTransaction transactionMySql = this.context_Mysql.Database.BeginTransaction())
            {
                try
                {
                    await this.context_Mysql.consultas.AddAsync(consulta);
                    await this.context_Mysql.SaveChangesAsync();
                    await transactionMySql.CommitAsync();
                    return (1, "Consulta registrada con exito");
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
