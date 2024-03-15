using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionWebFrontend.DTO
{
    public class ActualizarConsulta_DTO
    {
        public int consulta_id { get; set; }
        public string nueva_fecha { get; set; }
        public string descripcion { get; set; }
    }
}