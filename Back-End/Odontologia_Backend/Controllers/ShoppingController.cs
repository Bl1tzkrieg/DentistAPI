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
    public class ShoppingController : ControllerBase
    {
        // private readonly IConfiguration config_System;
        private HandlerShopping recursos_Compras;

        public ShoppingController(Context_MySql dbMysql)
        {
            this.recursos_Compras = new HandlerShopping(dbMysql);
        }

        [HttpGet("obtener_compras")]
        public async Task<ActionResult> GetCompras()
        {
            try
            {
                List<Object> listaDeCompras = await this.recursos_Compras.ObtenerCompras();

                return listaDeCompras.Count > 0 ? Ok(listaDeCompras) : NotFound("No hay registros de compras");  
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, default, 500);
            }
        }

        [HttpGet("obtener_compra")]
        public async Task<ActionResult> GetCompra([FromQuery] int v1 = -1 )
        {
            try
            {
                if (v1 == -1) return BadRequest("Solicitud no procesada por favor ingrese el numero de identificacion del pedido");

                Object? statusCompra = await this.recursos_Compras.ObtenerCompra(v1);

                return statusCompra != null ? Ok(statusCompra) : NotFound();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, default, 500);
            }

        }

        /// <summary>
        /// Registra una compra asociada
        /// </summary>
        /// <param name="reqBody"></param>
        /// <returns></returns>
        [HttpPost("registrar_compra")]
        public async Task<ActionResult> PostCompra([FromBody] Buys_DTO reqBody )
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }

                (int status, string message) = await this.recursos_Compras.RegistrarCompra(reqBody);

                // Comentario luego de registrar una compra se debe actualizar el id del pedido asociado.

                return status > 0 ? Ok(message) : BadRequest(message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, default, 500);
            }
        }

    }
}
