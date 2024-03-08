using System.ComponentModel.DataAnnotations;

namespace Odontologia_Backend.Models
{
    public class Ventas_detalle
    {
        [Key]
        public int Vd_Id { get; set; }
        public byte Vd_Servicio_Id { get; set; }
        public decimal Vd_Costo { get; set; }
        public decimal Vd_Total { get; set; }
    }
}
