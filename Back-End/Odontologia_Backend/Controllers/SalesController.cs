using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using Odontologia_Backend.Class;
using Odontologia_Backend.Data;
using Odontologia_Backend.DTOs;
using Odontologia_Backend.Helper;
using Odontologia_Backend.Models;

namespace Odontologia_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        // private readonly IConfiguration config_System;
        private HandlerSales recursos_Ventas;

        public SalesController(Context_MySql dbMysql)
        {
            this.recursos_Ventas = new HandlerSales(dbMysql);
        }

        [HttpGet("obtener_ventas")]
        public async Task<ActionResult> GetVentas()
        {
            try
            {
                List<Venta> listaDeVentas = await this.recursos_Ventas.ObtenerVentas();

                return listaDeVentas.Count > 0 ? Ok(listaDeVentas) : BadRequest();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, default, 500);
            }
        }

        [HttpPost("registrar_venta")]
        public async Task<ActionResult> PostVentas([FromBody] Sale_DTO sale)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }

                (int statusRegistro, string message) = await recursos_Ventas.RegistrarVenta(new Venta
                {
                    Venta_Cli_Id = sale.id_cliente,
                    Venta_NroFactura = sale.nro_factura,
                    Venta_Usu_Id = sale.id_usuario,
                    Venta_Vd_Id = sale.venta_detalle_id,
                    Venta_fecha = DateTime.Now
                });

                return statusRegistro > 0 ? Ok(message) : BadRequest(message);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message, default, 500);
            }
        }


        [HttpPatch]
        public async Task<ActionResult> PatchVentas()
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
