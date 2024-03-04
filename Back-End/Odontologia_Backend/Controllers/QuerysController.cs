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
    public class QuerysController : ControllerBase
    {
        // private readonly IConfiguration config_System;
        private HandlerQuerys recursos_Consultas;

        public QuerysController(Context_MySql dbMysql)
        {
            this.recursos_Consultas = new HandlerQuerys(dbMysql);
        }

        [HttpGet("obtener_consultas")]
        public async Task<ActionResult> GetConsultas([FromQuery] string v1)
        {
            try
            {
                List<Object> listaDeConsultas = await this.recursos_Consultas.ObtenerConsultas(v1);

                return listaDeConsultas.Count > 0 ? Ok(listaDeConsultas) : BadRequest("lista vacia");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, default, 500);
            }
        }

        [HttpPost("registrar_consulta")]
        public async Task<ActionResult> PostConsulta(Query_DTO query)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }

                (int statusRegistro, string message) = await this.recursos_Consultas.RegistrarConsulta(new Consulta
                {
                    Consult_Id_Cliente = query.id_cliente,
                    Consult_Descripcion = query.descripcion,
                    Consult_Usu_Id = query.id_usuario,
                    Consult_FecAsig = DateTime.Parse(query.fecha_asignacion),
                    Consult_FecGen = DateTime.Parse(query.fecha_generacion),
                    Consult_Status = "PENDIENTE"
                });

                return statusRegistro > 0 ? Ok(message) : BadRequest();
            }
            catch (Exception ex) 
            {
                return Problem(ex.Message, default, 500);
            }
        }

        [HttpPatch("actualizar_consulta")]
        public async Task<ActionResult> PatchConsulta()
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
