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
        /// <param name="user_id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<Usuario>> ObtenerUsuario( int user_id )
        {
            try
            {
                if (user_id.Equals(-1))
                    return await this.context_Mysql.usuarios.ToListAsync();

                return await this.context_Mysql.usuarios.Where(x => x.Usu_Id == user_id).ToListAsync();
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

        /// <summary>
        /// Actualiza un usuario segun su username
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public async Task<(int,string)> ActualizarUsuario( Usuario usuario)
        {
            try
            {
                Usuario? statusUser = await this.context_Mysql.usuarios.FirstOrDefaultAsync(x => x.Usu_UserName.Equals(usuario.Usu_UserName));

                if (statusUser == null) return (0, "Usuario no Encontrado");

                statusUser.Usu_Password = usuario.Usu_Password;
                statusUser.Usu_Status = usuario.Usu_Status;
                statusUser.Usu_Rol_Id = usuario.Usu_Rol_Id;
                
                // actualizar cambios
                await this.context_Mysql.SaveChangesAsync();

                return (1, "Usuario Actualizado con exito!");
            }
            catch ( Exception ex )
            {
                throw;
            }
        }
    }
}
