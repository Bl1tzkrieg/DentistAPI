using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionWebFrontend.DTO
{
    public class RegistrarInsumo_DTO
    {
        public string pedido_descripcion { get; set; }
        public short pedido_cantidad { get; set; }
        public short pedido_usuario_id { get; set; }
    }
}