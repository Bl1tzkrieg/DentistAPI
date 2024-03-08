using System.Reflection.Metadata.Ecma335;

namespace Odontologia_Backend.DTOs
{
    public class Order_DTO
    {
        public string pedido_descripcion { get; set; }
        public short pedido_cantidad { get; set; }
        public short pedido_usuario_id { get; set; }
    }
}
