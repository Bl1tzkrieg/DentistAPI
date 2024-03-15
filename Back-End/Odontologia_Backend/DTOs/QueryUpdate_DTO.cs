using System.ComponentModel.DataAnnotations;

namespace Odontologia_Backend.DTOs
{
    public class QueryUpdate_DTO
    {
        public int id_consulta { get; set; }
        public string fecha_consulta { get; set; }
        public string descripcion { get; set; }
    }
}
