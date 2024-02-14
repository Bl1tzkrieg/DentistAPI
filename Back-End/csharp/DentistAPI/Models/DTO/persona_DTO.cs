using System.ComponentModel.DataAnnotations;
namespace DentistAPI.Models.DTO
{
    public class persona_DTO
    {
        public string Nombre { get; set; }
        public DateTime Nacimiento { get; set; }
        public string Nota { get; set; }
        public int Cedula { get; set; }
    }
}