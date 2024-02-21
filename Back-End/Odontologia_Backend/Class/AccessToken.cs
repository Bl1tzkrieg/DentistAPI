using Microsoft.EntityFrameworkCore;
using Odontologia_Backend.Data;
using Odontologia_Backend.Models;

namespace Odontologia_Backend.Class
{
    public class AccessToken
    {
        private readonly Context_MySql context_Mysql;

        public AccessToken( Context_MySql dbMysql ) 
        { 
            this.context_Mysql = dbMysql;
        }

        /// <summary>
        /// Verifica las credenciales del usuario para darle permisos de acuerdo a su rol
        /// </summary>
        /// <param name="_user"></param>
        /// <param name="_password"></param>
        /// <returns></returns>
        public async Task<Usuario?> GetUser( string _user, string _password )
        {
            try
            {

                // Validar si la clave encryptada enviada coincide con la que esta guardada en base de datos.

                return await this.context_Mysql.usuarios.Where(x => x.Usu_UserName.Equals(_user.ToUpper()) && x.Usu_Password.Equals(_password)).FirstOrDefaultAsync();
            }
            catch ( Exception )
            {
                throw;
            }
        }
    }
}
