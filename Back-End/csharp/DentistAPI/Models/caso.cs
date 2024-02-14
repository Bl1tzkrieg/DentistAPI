namespace DentistAPI.Models
{
    public class caso
    {
        public int id { get; set; }
        public Guid uuid { get; set; }
        public int fk_historia { get; set; }
        public int fk_cita { get; set; }
        public DateTime fecha { get; set; }
        public string rutafichero { get; set; }
    }
}
