using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentistAPI.Models
{
    public class persona
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; } = 0;
        public Guid uuid { get; set; }
        public string nombre { get; set; }
        public DateTime nacimiento { get; set; }
        public string nota { get; set; }
        public int cedula { get; set; }
    }
}
