using System.ComponentModel.DataAnnotations;

namespace Odontologia_Backend.Models
{
    public class Order
    {
        [Key]
        public int Ped_Id { get; set; }
        public short Ped_Cant { get; set; }
        public string Ped_Descripcion { get; set; }
        public short Ped_Usu_Id { get; set; }
        public DateTime Ped_Fecha { get; set; }
        public string Ped_Status { get; set; }
    }
}
