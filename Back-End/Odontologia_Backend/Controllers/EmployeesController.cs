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
    public class EmployeesController : ControllerBase
    {
        // private readonly IConfiguration config_System;
        private HandlerEmployees recursos_Empleados;

        public EmployeesController(Context_MySql dbMysql)
        {
            this.recursos_Empleados = new HandlerEmployees(dbMysql);
        }


        [HttpGet("obtener_empleado")]
        public async Task<ActionResult> GetEmpleado([FromQuery] string v1 )
        {
            try
            {
                if (string.IsNullOrEmpty(v1)) return BadRequest("No se Ingresaron Datos");

                Empleado? _empleado = await this.recursos_Empleados.ObtenerEmpleado(v1);

                return _empleado is not null ? Ok(_empleado) : NotFound("Empleado No Encontrado");
            }
            catch (Exception ex)
            {
                return Problem( ex.Message, default, 500 );
            }
        }

        [HttpPost("registrar_empleado")]
        public async Task<ActionResult> PostEmpleado([FromBody] Employee_DTO empleado)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }

                (int statusRegistro, string message) = await this.recursos_Empleados.RegistrarEmpleado( new Empleado
                {
                    Emp_Cedula = empleado.identificacion,
                    Emp_Nombre1 = empleado.nombre_1,
                    Emp_Nombre2 = empleado.nombre_2,
                    Emp_Apellido1 = empleado.apellido_1,
                    Emp_Apellido2 = empleado.apellido_2,
                    Emp_Correo = empleado.correo,
                });

                return statusRegistro > 0 ? Ok(message) : BadRequest();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, default, 500);
            }
        }

        [HttpPatch]
        public async Task<ActionResult> PatchEmpleado( string v1 )
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, default, 500);
            }
        }


    }
}
