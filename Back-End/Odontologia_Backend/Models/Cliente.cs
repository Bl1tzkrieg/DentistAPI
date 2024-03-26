using System.ComponentModel.DataAnnotations;

namespace Odontologia_Backend.Models
{
    public class Cliente
    {
        [Key]
        public int Cli_Id { get; set; }
        public string Cli_Cedula { get; set; }
        public string Cli_Nombre1 { get; set; }
        public string Cli_Nombre2 { get; set; }
        public string Cli_Apellido1 { get; set; }
        public string Cli_Apellido2 { get; set; }
        public bool Cli_Status { get; set; }
        public string Cli_Telefono { get; set; }
    }
}
