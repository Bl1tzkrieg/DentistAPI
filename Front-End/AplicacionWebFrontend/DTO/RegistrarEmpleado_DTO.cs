using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionWebFrontend.DTO
{
    public class RegistrarEmpleado_DTO
    {
        public string identificacion { get; set; }
        public string nombre_1 { get; set; }
        public string nombre_2 { get; set; }
        public string apellido_1 { get; set; }
        public string apellido_2 { get; set; }
        public string correo { get; set; }
    }
}