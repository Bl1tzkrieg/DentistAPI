using Microsoft.EntityFrameworkCore;

namespace Odontologia_Backend.Data
{
    public class Context_MySql : DbContext
    {
        public Context_MySql(DbContextOptions<Context_MySql> options) : base(options) { }
    }
}
