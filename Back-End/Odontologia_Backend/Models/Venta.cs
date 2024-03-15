using System.ComponentModel.DataAnnotations;

namespace Odontologia_Backend.Models
{
    public class Venta
    {
        [Key]
        public int Venta_Id { get; set; }
        public int Venta_Cli_Id { get; set; }
        public DateTime Venta_FecGen { get; set; }
        public string Venta_NroFactura { get; set; }
        public short Venta_Usu_Id { get; set; }
        public int Venta_Vd_Id { get; set; }
    }
}
