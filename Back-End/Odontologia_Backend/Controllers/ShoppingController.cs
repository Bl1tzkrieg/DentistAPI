using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Odontologia_Backend.Class;
using Odontologia_Backend.Data;

namespace Odontologia_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {
        // private readonly IConfiguration config_System;
        private HandlerQuerys recursos_Compras;

        public ShoppingController(Context_MySql dbMysql)
        {
            this.recursos_Compras = new HandlerQuerys(dbMysql);
        }
    }
}
