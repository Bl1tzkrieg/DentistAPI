using System.ComponentModel.DataAnnotations;

namespace Odontologia_Backend.Models
{
    public class TipoDePago
    {
        [Key]
        public byte Tp_Id { get; set; }
        public string Tp_Descripcion { get; set; }
    }
}
