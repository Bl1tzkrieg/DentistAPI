using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Odontologia_Backend.Class;
using Odontologia_Backend.Data;
using Odontologia_Backend.Models;

namespace Odontologia_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        // private readonly IConfiguration config_System;
        private HandlerEmployees recursos_Empleados;

        public EmployeesController(Context_MySql dbMysql)
        {
            this.recursos_Empleados = new HandlerEmployees(dbMysql);
        }


        [HttpGet]
        public async Task<ActionResult> GetEmpleado([FromQuery] string v1 )
        {
            try
            {
                Empleado? _empleado = await this.recursos_Empleados.ObtenerEmpleado(v1);

                return _empleado is not null ? Ok(_empleado) : NotFound("Empleado No Encontrado");
            }
            catch (Exception ex)
            {
                return Problem( ex.Message, default, 500 );
            }
        }
    }
}
