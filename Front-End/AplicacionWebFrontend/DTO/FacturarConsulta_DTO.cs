using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionWebFrontend.DTO
{
    public class FacturarConsulta_DTO
    {
        public int consulta_id { get; set; }
        public int cliente_id { get; set; }
        public string numero_factura { get; set; }
        public short usuario_id { get; set; }
        public decimal total_pagar { get; set; }
        public byte tipo_pago { get; set; }
    }
}