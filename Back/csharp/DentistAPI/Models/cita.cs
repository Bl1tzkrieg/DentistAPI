namespace DentistAPI.Models
{
    public class cita
    {
        public int id { get; set; }
        public Guid uuid { get; set; }
        public Guid fk_persona_paciente { get; set; }
        public Guid fk_perfil_doctor { get; set; }
        public int statusconsulta { get; set; }
        public string rutafichero { get; set; }
    }
}
