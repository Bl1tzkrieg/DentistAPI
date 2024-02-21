using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Odontologia_Backend.Class;
using Odontologia_Backend.Data;

namespace Odontologia_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuerysController : ControllerBase
    {
        // private readonly IConfiguration config_System;
        private HandlerQuerys recursos_Consultas;

        public QuerysController(Context_MySql dbMysql)
        {
            this.recursos_Consultas = new HandlerQuerys(dbMysql);
        }

    }
}
