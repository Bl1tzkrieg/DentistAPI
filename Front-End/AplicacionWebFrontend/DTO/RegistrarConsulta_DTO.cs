using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionWebFrontend.DTO
{
    public class RegistrarConsulta_DTO
    {
        public int id_cliente { get; set; }
        public string descripcion { get; set; }
        public int id_usuario { get; set; }
        public string fecha_asignacion { get; set; }
    }
}