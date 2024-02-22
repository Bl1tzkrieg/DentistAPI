using Microsoft.EntityFrameworkCore;
using Odontologia_Backend.Data;
using Odontologia_Backend.Models;

namespace Odontologia_Backend.Class
{
    public class HandlerCustomer
    {
        private readonly Context_MySql context_Mysql;

        public HandlerCustomer(Context_MySql dbMysql)
        {
            this.context_Mysql = dbMysql;
        }

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
    }
}
