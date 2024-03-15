using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Odontologia_Backend.Class;
using Odontologia_Backend.Data;
using Odontologia_Backend.DTOs;
using Odontologia_Backend.Helper;

namespace Odontologia_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private HandlerOrders recursos_Pedidos;

        public OrdersController(Context_MySql dbMysql)
        {
            this.recursos_Pedidos = new HandlerOrders(dbMysql);
        }

        [HttpGet("obtener_pedidos")]
        public async Task<ActionResult> GetPedidos()
        {
            try
            {
                List<Object> statusPedidos = await this.recursos_Pedidos.ObtenerPedidos();

                return statusPedidos.Count > 0 ? Ok(statusPedidos) : NotFound("No hay pedidos disponibles");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, default, 500);
            }
        }

        [HttpGet("obtener_pedido")]
        public async Task<ActionResult> GetPedido([FromQuery] int v1)
        {
            try
            {
                Object? statusPedido = await this.recursos_Pedidos.ObtenerPedido(v1);

                return statusPedido is not null ? Ok(statusPedido) : NotFound(); 
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, default, 500);
            }
        }

        [HttpPost("registrar_pedido")]
        public async Task<ActionResult> PostPedidos([FromBody] Order_DTO reqBody )
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }

                (int status, string message) = await this.recursos_Pedidos.RegistrarPedido(reqBody);
                
                return status > 0 ? Ok(message) : BadRequest(message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, default, 500);
            }
        }

        [HttpPatch("actualizar_pedido")]
        public async Task<ActionResult> PatchPedido([FromBody] OrderUpdate_DTO reqBody)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }

                (int status, string message) = await this.recursos_Pedidos.ActualizarPedido(reqBody);

                return status > 0 ? Ok(message) : BadRequest(message);
            }
            catch ( Exception ex)
            {
                return Problem(ex.Message, default, 500);
            }
        }
    }
}
