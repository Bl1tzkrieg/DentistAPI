namespace Odontologia_Backend.DTOs
{
    public class Query_DTO
    {
        public int id_cliente { get; set; }
        public string descripcion { get; set; }
        public short id_usuario { get; set; }
        public string fecha_asignacion { get; set; }
        public string fecha_generacion { get; set; }
    }
}
