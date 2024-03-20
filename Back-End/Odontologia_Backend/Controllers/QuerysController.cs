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
        public async Task<ActionResult> GetConsultas([FromQuery] int v1 = 1 )
        {
            try
            {
                List<Object> listaDeConsultas = await this.recursos_Consultas.ObtenerConsultas(v1);

                return listaDeConsultas.Count > 0 ? Ok(listaDeConsultas) : NotFound(listaDeConsultas);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, default, 500);
            }
        }

        [HttpGet("obtener_consulta")]
        public async Task<ActionResult> GetConsulta([FromQuery] int v1)
        {
            try
            {
                Object? statusConsulta = await this.recursos_Consultas.ObtenerConsulta(v1);

                return statusConsulta is null ? NotFound("Consulta no encontrada") : Ok(statusConsulta);

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
                    Consult_FecGen = DateTime.Now,
                    Consult_Status = "PENDIENTE",
                    Consult_Facturada = "POR FACTURAR"
                });

                return statusRegistro > 0 ? Ok(message) : BadRequest();
            }
            catch (Exception ex) 
            {
                return Problem(ex.Message, default, 500);
            }
        }

        [HttpPatch("actualizar_consulta")]
        public async Task<ActionResult> PatchConsulta(QueryUpdate_DTO query)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }

                (int status, string message) = await this.recursos_Consultas.ActualizarConsulta( new Consulta {
                    Consult_Id = query.id_consulta,
                    Consult_FecAsig = DateTime.Parse(query.fecha_consulta),
                    Consult_Descripcion = query.descripcion,
                });

                return status > 0 ? Ok(message) : BadRequest(message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, default, 500);
            }
        }

        [HttpPatch("cancelar_consulta")]
        public async Task<ActionResult> CancelarConsulta([FromQuery] int v1)
        {
            try 
            {
                (int status, string message) = await this.recursos_Consultas.CancelarConsulta(v1);

                return status > 0 ? Ok(message) : BadRequest(message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, default, 500);
            }
        }

        [HttpPost("facturar_consulta")]
        public async Task<ActionResult> Facturar_Consulta([FromBody] Query_Bill_Consultation_DTO reqBody )
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }

                (int status, string message) = await this.recursos_Consultas.FacturarConsulta(reqBody);

                return status > 0 ? Ok(message) : BadRequest(message);
            }
            catch ( Exception ex )
            {
                return Problem(ex.Message, default, 500);
            }
        }

    }
}
