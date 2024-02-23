using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MySqlX.XDevAPI;
using Odontologia_Backend.Data;
using Odontologia_Backend.Models;

namespace Odontologia_Backend.Class
{
    public class HandlerEmployees
    {
        private readonly Context_MySql context_Mysql;

        public HandlerEmployees(Context_MySql dbMysql)
        {
            this.context_Mysql = dbMysql;
        }

        /// <summary>
        /// Obtiene un empleado mediante su identificacion o cedula
        /// </summary>
        /// <param name="cedula">Identificacion</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Empleado?> ObtenerEmpleado( string cedula )
        {
            try
            {
                return await this.context_Mysql.empleados.FirstOrDefaultAsync( x => x.Emp_Cedula.Equals(cedula));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="empleado"></param>
        /// <returns>""</returns>
        /// <exception cref="Exception"></exception>
        public async Task<(int,string)> RegistrarEmpleado( Empleado empleado)
        {
            using (IDbContextTransaction transactionMySql = this.context_Mysql.Database.BeginTransaction())
            {
                try
                {
                    await this.context_Mysql.empleados.AddAsync(empleado);
                    await this.context_Mysql.SaveChangesAsync();
                    await transactionMySql.CommitAsync();
                    return (1, "Empleado registrado con exito");
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
