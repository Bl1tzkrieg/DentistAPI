using System.ComponentModel.DataAnnotations;

namespace Odontologia_Backend.Models
{
    public class Rol
    {
        [Key]
        public byte Rol_Id { get; set; }
        public string Rol_Descripcion { get; set; }
    }
}
