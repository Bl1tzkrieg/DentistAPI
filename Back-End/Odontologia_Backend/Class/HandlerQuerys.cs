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
        public async Task<List<Object>> ObtenerConsultas(string status)
        {
            try
            {
                return await this.context_Mysql.consultas
                        .Join(this.context_Mysql.usuarios, c => c.Consult_Usu_Id, u => u.Usu_Id,
                            (consulta, usuario) => new { consulta, usuario })
                        .Join(this.context_Mysql.clientes, c => c.consulta.Consult_Id_Cliente, cli => cli.Cli_Id,
                            ( c2, cliente) => new { c2.consulta, c2.usuario, cliente})
                        .Where( q => ( !string.IsNullOrEmpty(status) ? q.consulta.Consult_Status.Equals(status) : true ) )
                        .Select( x => new {
                            consulta_id = x.consulta.Consult_Id,
                            cedula_cliente = x.cliente.Cli_Cedula,
                            nombre_cliente = x.cliente.Cli_Nombre1,
                            apellido_cliente = x.cliente.Cli_Apellido1,
                            descripcion_consulta = x.consulta.Consult_Descripcion,
                            fecha_asignacion = x.consulta.Consult_FecAsig.ToString("dd/MM/yyyy"),
                            fecha_generacion = x.consulta.Consult_FecGen.ToString("dd/MM/yyyy"),
                            nombre_usuario = x.usuario.Usu_UserName,
                            consulta_estado = x.consulta.Consult_Status
                        })
                        .ToListAsync<Object>();
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
