namespace Odontologia_Backend.DTOs
{
    public class Buys_DTO
    {
        public short id_usuario { get; set; }
        public int id_pedido { get; set; }
        public string nro_factura { get; set; }
        public Decimal total { get; set; }
    }
}
