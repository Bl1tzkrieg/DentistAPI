using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionWebFrontend.DTO
{
    public class RegistroUsuario_DTO
    {
        public short empleado_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public byte rol { get; set; }
    }
}