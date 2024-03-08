using System.ComponentModel.DataAnnotations;

namespace Odontologia_Backend.Models
{
    public class Servicio
    {
        [Key]
        public byte Serv_Id { get; set; }
        public string Serv_Descripcion { get; set; }
        public Decimal Serv_Precio { get; set; }
    }
}
