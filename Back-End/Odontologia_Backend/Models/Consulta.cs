namespace Odontologia_Backend.Models
{
    public class Consulta
    {
        public int Consult_Id { get; set; }
        public int Consult_Id_Cliente { get; set; }
        public string Consult_Descripcion { get; set; }
        public short Consult_Usu_Id { get; set; }
        public DateTime Consult_FecAsig { get; set; }
        public DateTime Consult_FecGen { get; set; }
        public bool Consult_Status { get; set; }
    }
}
