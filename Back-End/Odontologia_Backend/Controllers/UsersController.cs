using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Odontologia_Backend.Class;
using Odontologia_Backend.Data;

namespace Odontologia_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // private readonly IConfiguration config_System;
        private HandlerUsers recursos_Usuarios;

        public UsersController(Context_MySql dbMysql)
        {
            this.recursos_Usuarios = new HandlerUsers(dbMysql);
        }
    }
}
