namespace DentistAPI.Models
{
    public class operacion
    {
        public int id { get; set; }
        public string uuid { get; set; }
        public int fk_historialcompraventa { get; set; }
        public DateTime fecha { get; set; }
        public int monto_crudo { get; set; }
        public int tipooperacion { get; set; }
        public int estado { get; set; }
        public string descripcion { get; set; }

    }
}
