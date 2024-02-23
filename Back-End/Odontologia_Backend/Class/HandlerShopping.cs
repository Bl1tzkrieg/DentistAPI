using Microsoft.EntityFrameworkCore;
using Odontologia_Backend.Data;
using Odontologia_Backend.Models;

namespace Odontologia_Backend.Class
{
    public class HandlerShopping
    {
        private readonly Context_MySql context_Mysql;

        public HandlerShopping( Context_MySql dbMysql )
        {
            this.context_Mysql = dbMysql;
        }

        public async Task<List<Compra>> ObtenerCompras()
        {
            try
            {
                return await this.context_Mysql.compras.ToListAsync();
            }
            catch ( Exception ex )
            {
                throw;
            }
        }
    }
}
