using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
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

        /// <summary>
        /// Obtiene todos los pedidos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("obtener_pedidos")]
        public async Task<ActionResult> GetPedidos()
        {
            try
            {
                List<Object> statusPedidos = await this.recursos_Pedidos.ObtenerPedidos();

                return Ok(statusPedidos);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, default, 500);
            }
        }

        /// <summary>
        /// Obtiene un pedido.
        /// </summary>
        /// <param name="v1">pedido id</param>
        /// <returns></returns>
        [HttpGet("obtener_pedido")]
        public async Task<ActionResult> GetPedido([FromQuery] int v1 = -1)
        {
            try
            {
                if (v1 < 1) return BadRequest();

                Object? statusPedido = await this.recursos_Pedidos.ObtenerPedido(v1);

                return statusPedido is not null ? Ok(statusPedido) : NotFound(); 
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, default, 500);
            }
        }

        /// <summary>
        /// Registra un pedido.
        /// </summary>
        /// <param name="reqBody"></param>
        /// <returns></returns>
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
                
                return status > 0 ? StatusCode(201) : BadRequest();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, default, 500);
            }
        }

        /// <summary>
        /// Actualiza un pedido.
        /// </summary>
        /// <param name="reqBody"></param>
        /// <returns></returns>
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

                return status > 0 ? Ok() : BadRequest();
            }
            catch ( Exception ex)
            {
                return Problem(ex.Message, default, 500);
            }
        }

        /// <summary>
        /// Cancela un pedido
        /// </summary>
        /// <param name="v1">pedido id</param>
        /// <returns></returns>
        [HttpPatch("cancelar_pedido")]
        public async Task<ActionResult> CancelarPedido([FromQuery] int v1) 
        {
            try
            {
                (int status, string message) = await this.recursos_Pedidos.CancelarPedido(v1);

                return status > 0 ? Ok() : BadRequest();
            }
            catch ( Exception e )
            {
                return Problem(e.Message, default, 500);
            }
        }

        /// <summary>
        /// Factura un pedido.
        /// </summary>
        /// <param name="reqBody"></param>
        /// <returns></returns>
        [HttpPost("facturar_pedido")]
        public async Task<ActionResult> FacturarPedido([FromBody] Invoice_Order_DTO reqBody)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }

                (int status, string message) = await this.recursos_Pedidos.FacturarPedido(reqBody);

                return status > 0 ? StatusCode(201) : BadRequest();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, default, 500);
            }
        }

        /// <summary>
        /// Obtiene la cantidad de pedidos segun el tipo de estatus.
        /// </summary>
        /// <param name="v1">tipo estatus 1:FACTURADO - 2:CANCELADO</param>
        /// <returns></returns>
        [HttpGet("obtener_cantidad_pedidos_por_estatus")]
        public ActionResult ObtenerCantidadPedidosPorStatus([FromQuery] int v1) 
        {
            try
            {
                return Ok(this.recursos_Pedidos.ObtenerCantidadDeInsumosPorEstado(v1));
            }
            catch (Exception e)
            {
                return Problem(e.Message, default, 500);
            }
        }
    }
}
