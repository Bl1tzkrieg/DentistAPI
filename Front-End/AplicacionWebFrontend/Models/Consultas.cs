using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AplicacionWebFrontend.Models
{
    public class Consultas
    {
        public int consulta_id { get; set; }
        public string cedula_cliente { get; set; }
        public string nombre_cliente { get; set; }
        public string apellido_cliente { get; set; }
        public string descripcion_consulta { get; set; }
        public string fecha_generacion { get; set; }
        public string fecha_asignacion { get; set; }
        public string nombre_usuario { get; set; }
        public string consulta_estado { get; set; }
    }
}