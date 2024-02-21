using Odontologia_Backend.Data;

namespace Odontologia_Backend.Class
{
    public class HandlerUsers
    {
        private readonly Context_MySql context_Mysql;

        public HandlerUsers(Context_MySql dbMysql)
        {
            this.context_Mysql = dbMysql;
        }
    }
}
