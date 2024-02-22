using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Odontologia_Backend.Class;
using Odontologia_Backend.Data;
using Odontologia_Backend.Models;

namespace Odontologia_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        // private readonly IConfiguration config_System;
        private HandlerCustomer recursos_Clientes;

        public CustomersController(Context_MySql dbMysql)
        {
            this.recursos_Clientes = new HandlerCustomer(dbMysql);
        }

        [HttpGet]
        public async Task<ActionResult> GetCliente([FromQuery] string v1)
        {
            try
            {
                Cliente? customer = await this.recursos_Clientes.ObtenerCliente(v1);

                return customer is not null ? Ok(customer) : NotFound("Cliente no Encontrado!");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, default, 500);
            }
        }
    }
}
