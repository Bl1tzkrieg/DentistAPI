namespace Odontologia_Backend.DTOs
{
    public class User_DTO
    {
        public string username { get; set; }
        public string password { get; set; }
        public byte rol { get; set; }
        public short empleado_id { get; set; }
        public bool status { get; set; }
        public string documento { get; set; }
    }
}
