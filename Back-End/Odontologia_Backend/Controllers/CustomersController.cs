using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Odontologia_Backend.Class;
using Odontologia_Backend.Data;
using Odontologia_Backend.DTOs;
using Odontologia_Backend.Helper;
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

        [HttpPost]
        public async Task<ActionResult> PostCliente(Customer_DTO cliente )
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }

                (int statusRegistro, string message) = await recursos_Clientes.RegistrarCliente( new Cliente
                {
                    Cli_Cedula = cliente.identificacion,
                    Cli_Nombre1 = cliente.nombre_1,
                    Cli_Nombre2 = cliente.nombre_2,
                    Cli_Apellido1 = cliente.apellido_1,
                    Cli_Apellido2 = cliente.apellido_2, 
                });

                return statusRegistro > 0 ? Ok(message) : BadRequest(message);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message, default, 500);
            }
        }

        [HttpPatch]
        public async Task<ActionResult> PatchCliente(Customer_DTO cliente)
        {
            try
            {
                return Ok();
            }
            catch ( Exception ex )
            {
                return Problem(ex.Message, default, 500);
            }
        }
    }
}
