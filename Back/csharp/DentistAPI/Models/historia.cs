namespace DentistAPI.Models
{
    public class historia
    {
        public int id { get; set; }
        public string uuid { get; set; }
        public int fk_persona { get; set; }
    }
}
