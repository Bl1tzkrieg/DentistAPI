using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Odontologia_Backend.Class;
using Odontologia_Backend.Data;

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
    }
}
