using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OdontologiaFrontend.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        public string CORREO { get; set; }
        public string PASSWORD { get; set; }
        public int ROLL { get; set; }
    }
}