using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Odontologia_Backend.Models
{
    public class Usuario
    {
        [Key]
        public short Usu_Id { get; set; }
        public string Usu_UserName { get; set; }
        public string Usu_Password { get; set; }
        public byte Usu_Rol_Id { get; set; }
        public short Usu_Emp_Id { get; set; }
        public bool Usu_Status { get; set; }
    }
}
