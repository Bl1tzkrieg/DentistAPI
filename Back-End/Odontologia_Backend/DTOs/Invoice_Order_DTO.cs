namespace Odontologia_Backend.DTOs
{
    public class Invoice_Order_DTO
    {
        public short usuario_id { get; set; }
        public int pedido_id { get; set; }
        public string numero_factura { get; set; }
        public decimal monto_total { get; set; }
    }
}
