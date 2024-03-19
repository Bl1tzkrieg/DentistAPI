using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionWebFrontend.DTO
{
    public class FacturarInsumo_DTO
    {
        public short usuario_id { get; set; }
        public int pedido_id { get; set; }
        public string numero_factura { get; set; }
        public decimal monto_total { get; set; }
    }
}