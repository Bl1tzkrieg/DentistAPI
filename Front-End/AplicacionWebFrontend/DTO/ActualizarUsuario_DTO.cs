using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionWebFrontend.DTO
{
    public class ActualizarUsuario_DTO
    {
        public string username { get; set; }
        public string password { get; set; }
        public byte rol { get; set; }
        public bool username_status { get; set; }
    }
}