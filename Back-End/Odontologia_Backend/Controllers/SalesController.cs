using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Odontologia_Backend.Class;
using Odontologia_Backend.Data;

namespace Odontologia_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        // private readonly IConfiguration config_System;
        private HandlerSales recursos_Ventas;

        public SalesController(Context_MySql dbMysql)
        {
            this.recursos_Ventas = new HandlerSales(dbMysql);
        }
    }
}
