using System.ComponentModel.DataAnnotations;

namespace DentistAPI.Models
{
    public class acceso
    {
        [Key]
        public int id { get; set; }
        public string pass { get; set; }
        public string username { get; set; }
        public string token { get; set; }
        public string correo { get; set; }
        public int fk_perfil { get; set; }
    }
}