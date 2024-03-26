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
                List<Object> listaDeVentas = await this.recursos_Ventas.ObtenerVentas();

                return Ok(listaDeVentas);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, default, 500);
            }
        }

        [HttpGet("obtener_venta")]
        public async Task<ActionResult> GetVenta([FromQuery] int v1 = -1 )
        {
            try 
            {
                if (v1 <= -1) return BadRequest();

                Object? statusVenta = await this.recursos_Ventas.ObtenerVenta( v1 );

                return statusVenta != null ? Ok(statusVenta) : NotFound();
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
                    Venta_FecGen = DateTime.Now
                });

                return statusRegistro > 0 ? Ok(message) : BadRequest(message);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message, default, 500);
            }
        }

        [HttpGet("obtener_cantidad_ventas")]
        public ActionResult ObtenerCantidadVentas()
        {
            try
            {
                int cantidad_ventas = this.recursos_Ventas.ObtenerCantidadDeVentas();

                return Ok(cantidad_ventas);
            }
            catch ( Exception ex )
            {
                return Problem(ex.Message, default, 500);
            }
        }

        [HttpGet("obtener_total_ingresos")]
        public ActionResult ObtenerTotalIngresos()
        {
            try
            {
                decimal total = this.recursos_Ventas.ObtenerTotalIngresos();

                return Ok(total);
            }
            catch ( Exception ex)
            {
                return Problem(ex.Message, default, 500);
            }
        }
    }
}
