using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OdontologiaFrontend.DTO
{
    public class Usuario_DTO
    {
        public int ID { get; set; }
        public string CORREO { get; set; }
        public string PASSWORD { get; set; }
        public string CONFIRMARPASSWORD { get; set; }
    }
}