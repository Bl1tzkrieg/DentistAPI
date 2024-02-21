using Odontologia_Backend.Data;

namespace Odontologia_Backend.Class
{
    public class HandlerSales
    {
        private readonly Context_MySql context_Mysql;

        public HandlerSales(Context_MySql dbMysql)
        {
            this.context_Mysql = dbMysql;
        }
    }
}
