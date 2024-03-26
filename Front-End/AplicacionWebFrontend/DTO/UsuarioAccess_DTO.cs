using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionWebFrontend.DTO
{
    public class UsuarioAccess_DTO
    {
        public int user_id { get; set; }
        public string username { get; set; }
        public bool status { get; set; }
        public byte rol { get; set; }
        public string token { get; set; }
        public string message { get; set; }
    }
}