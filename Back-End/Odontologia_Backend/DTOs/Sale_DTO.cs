namespace Odontologia_Backend.DTOs
{
    public class Sale_DTO
    {
        public int id_cliente { get; set; }
        public string fecha_generacion { get; set; }
        public string nro_factura { get; set; }
        public short id_usuario { get; set; }
        public int venta_detalle_id { get; set; }
    }
}
