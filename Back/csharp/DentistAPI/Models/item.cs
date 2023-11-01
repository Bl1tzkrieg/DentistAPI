namespace DentistAPI.Models
{
    public class item
    {
        public int id { get; set; }
        public string uuid { get; set; }
        public int fk_inventario { get; set; }
        public int cantidad { get; set; }
        public string nombre { get; set; }

    }
}
