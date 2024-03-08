using System.ComponentModel.DataAnnotations;

namespace Odontologia_Backend.Models
{
    public class Compra
    {
        [Key]
        public int Comp_Id { get; set; }
        public short Comp_Usu_Id { get; set; }
        public DateTime Comp_Fecha { get; set; }
        public int Comp_Ped_Id { get; set; }
        public string Comp_NroFactura { get; set; }
        public Decimal Comp_Total { get; set; }
    }
}
