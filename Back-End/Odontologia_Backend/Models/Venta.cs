namespace Odontologia_Backend.Models
{
    public class Venta
    {
        public int Venta_Id { get; set; }
        public int Venta_Cli_Id { get; set; }
        public DateTime Venta_NroFactura { get; set; }
        public short Venta_Usu_Id { get; set; }
        public int Venta_Vd_Id { get; set; }
    }
}
