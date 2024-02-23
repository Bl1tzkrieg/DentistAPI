using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Odontologia_Backend.Class;
using Odontologia_Backend.Data;
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

        [HttpGet]
        public async Task<ActionResult> GetCompras()
        {
            try
            {
                List<Compra> listaDeCompras = await this.recursos_Compras.ObtenerCompras();

                return listaDeCompras.Count > 0 ? Ok(listaDeCompras) : BadRequest();  
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, default, 500);
            }
        }
    }
}
