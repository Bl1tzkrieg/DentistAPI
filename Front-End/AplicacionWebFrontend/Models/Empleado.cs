using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionWebFrontend.Models
{
    public class Empleado
    {
        public short Emp_Id { get; set; }
        public string Emp_Cedula { get; set; }
        public string Emp_Nombre1 { get; set; }
        public string Emp_Nombre2 { get; set; }
        public string Emp_Apellido1 { get; set; }
        public string Emp_Apellido2 { get; set; }
        public string Emp_Correo { get; set; }
    }
}