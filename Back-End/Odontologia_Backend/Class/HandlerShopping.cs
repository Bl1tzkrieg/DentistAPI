using Odontologia_Backend.Data;

namespace Odontologia_Backend.Class
{
    public class HandlerShopping
    {
        private readonly Context_MySql context_Mysql;

        public HandlerShopping( Context_MySql dbMysql )
        {
            this.context_Mysql = dbMysql;
        }
    }
}
