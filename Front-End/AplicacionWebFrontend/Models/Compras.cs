using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionWebFrontend.Models
{
    public class Compras
    {
        public int compra_id { get; set; }
        public string compra_usuario { get; set; }
        public string compra_fecha { get; set; }
        public int compra_pedido_id { get; set; }
        public string compra_numero_factura { get; set; }
        public short pedido_cantidad { get; set; }
        public string pedido_fecha_registro { get; set; }
        public string pedido_status { get; set; }
        public string pedido_descripcion { get; set; }
        public decimal compra_total { get; set; }
    }
}