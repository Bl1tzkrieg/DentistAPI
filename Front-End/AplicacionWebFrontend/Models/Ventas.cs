using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionWebFrontend.Models
{
    public class Ventas
    {
        public int venta_id { get; set; }
        public string venta_nombre_cliente { get; set; }
        public string venta_apellido_cliente { get; set; }
        public string venta_cedula_cliente { get; set; }
        public string venta_username { get; set; }
        public string venta_numero_factura { get; set; }
        public string venta_fecha { get; set; }
        public string venta_tipo_pago { get; set; }
        public decimal venta_total { get; set; }
    }
}