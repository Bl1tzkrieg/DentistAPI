using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionWebFrontend.Models
{
    public class Cliente
    {
        public int cliente_id { get; set; }
        public string cliente_nombre { get; set; }
        public string cliente_apellido { get;
        set; }
        public string cliente_cedula { get; set; }
        public string cliente_status { get; set; }
    }
}