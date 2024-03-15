using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionWebFrontend.DTO
{
    public class ActualizarInsumo_DTO
    {
        public int pedido_id { get; set; }
        public short pedido_cantidad { get; set; }
        public string pedido_descripcion { get; set; }
    }
}