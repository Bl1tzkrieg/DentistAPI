using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MySqlX.XDevAPI;
using Odontologia_Backend.Data;
using Odontologia_Backend.Models;

namespace Odontologia_Backend.Class
{
    public class HandlerUsers
    {
        private readonly Context_MySql context_Mysql;

        public HandlerUsers(Context_MySql dbMysql)
        {
            this.context_Mysql = dbMysql;
        }

        /// <summary>
        /// Devuelve Un usuario filtrando la busqueda por su nombre de usuario
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Usuario?> ObtenerUsuario( string user )
        {
            try
            {
                return await this.context_Mysql.usuarios.FirstOrDefaultAsync( x => x.Usu_UserName.Equals(user));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Registra un nuevo usuario a la base de datos
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public async Task<(int,string)> RegistrarUsuario( Usuario usuario)
        {
            using (IDbContextTransaction transactionMySql = this.context_Mysql.Database.BeginTransaction())
            {
                try
                {
                    await this.context_Mysql.usuarios.AddAsync(usuario);
                    await this.context_Mysql.SaveChangesAsync();
                    await transactionMySql.CommitAsync();
                    return (1, "Usuario registrado con exito");
                }
                catch (Exception)
                {
                    await transactionMySql.RollbackAsync();
                    throw;
                }

            }
        }
    }
}
