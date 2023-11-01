namespace DentistAPI.Models.DTO
{
    public class PersonaDto
    {
        public string Nombre { get; set; }
        public DateTime Nacimiento { get; set; }
        public Guid Uuid { get; set; }
        public string Nota { get; set; }
        public int Cedula { get; set; }
    }

}
