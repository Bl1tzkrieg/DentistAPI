using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionWebFrontend.DTO
{
    public class UsuarioAccess_DTO
    {
        public string username { get; set; }
        public bool status { get; set; }
        public byte rol { get; set; }
        public string token { get; set; }
    }
}