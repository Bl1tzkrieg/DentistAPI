using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionWebFrontend.Models
{
    public class Pedidos
    {
        public int pedido_id { get; set; }
        public short pedido_cantidad { get; set; }
        public string pedido_descripcion { get; set; }
        public string pedido_username { get; set; }
        public string pedido_fecha { get; set; }
        public string pedido_status { get; set; }
    }
}