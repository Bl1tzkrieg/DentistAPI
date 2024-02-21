using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Odontologia_Backend.Class;
using Odontologia_Backend.Data;

namespace Odontologia_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        // private readonly IConfiguration config_System;
        private HandlerQuerys recursos_Clientes;

        public CustomersController(Context_MySql dbMysql)
        {
            this.recursos_Clientes = new HandlerQuerys(dbMysql);
        }
    }
}
