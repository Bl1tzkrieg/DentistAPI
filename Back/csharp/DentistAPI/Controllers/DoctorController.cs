using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DentistAPI.Helper;
using Microsoft.EntityFrameworkCore;

namespace DentistAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly UserRepository userRep;
        public DoctorController(AppDbContext Context, UserRepository userRep)
        {
            this.userRep = userRep;
        }

        [HttpGet("dentistas")]
        public IActionResult ObtenerDentistas()
        {

            var dentistas = userRep.GetDentistas();
            if (dentistas.Result == null) { return NotFound("No se encontraron dentistas"); }
            return Ok(dentistas.Result);
        }


    }
}
