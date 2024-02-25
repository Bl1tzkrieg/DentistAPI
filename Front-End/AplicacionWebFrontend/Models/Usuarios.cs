using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionWebFrontend.Models
{
    public class Usuarios
    {
        public short Usu_Id { get; set; }
        public string Usu_UserName { get; set; }
        public string Usu_Password { get; set; }
        public byte Usu_Rol_Id { get; set; }
        public short Usu_Emp_Id { get; set; }
        public bool Usu_Status { get; set; }
    }
}