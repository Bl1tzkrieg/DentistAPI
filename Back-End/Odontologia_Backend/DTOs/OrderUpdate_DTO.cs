namespace Odontologia_Backend.DTOs
{
    public class OrderUpdate_DTO
    {
        public int pedido_id { get; set; }
        public short pedido_cantidad { get; set; }
        public string pedido_descripcion { get; set; }
    }
}
