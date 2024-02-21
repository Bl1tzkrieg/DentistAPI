using Odontologia_Backend.Data;

namespace Odontologia_Backend.Class
{
    public class HandlerCustomer
    {
        private readonly Context_MySql context_Mysql;

        public HandlerCustomer(Context_MySql dbMysql)
        {
            this.context_Mysql = dbMysql;
        }
    }
}
