using Odontologia_Backend.Data;

namespace Odontologia_Backend.Class
{
    public class HandlerQuerys
    {
        private readonly Context_MySql context_Mysql;

        public HandlerQuerys(Context_MySql dbMysql) 
        { 
            this.context_Mysql = dbMysql;
        }
    }
}
