namespace DentistAPI.Models
{
    public class perfil
    {
        public int id { get; set; }
        public Guid uuid { get; set; }
        public Guid fk_permisos { get; set; }
        public Guid fk_persona { get; set; }

    }
}
