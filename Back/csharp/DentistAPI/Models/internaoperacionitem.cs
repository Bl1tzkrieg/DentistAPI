namespace DentistAPI.Models
{
    public class internaoperacionitem
    {
        public int id { get; set; }
        public int fk_operacion { get; set; }
        public int fk_item { get; set; }

    }
}
