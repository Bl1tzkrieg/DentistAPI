using DentistAPI.Models;
using DentistAPI.Models.DTO;
using DentistAPI.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace DentistAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserRepository userRep;
        public CitaController(AppDbContext Context, UserRepository userRep)
        {
            _context = Context;
            this.userRep = userRep;
        }

        [HttpPost("crearCita")]
        public async Task<IActionResult> CrearCita([FromBody] CitaDto citaDto)
        {

            var paciente = await userRep.GetPersonasbyCedula(citaDto.cedula);
            if (paciente is null) { return NotFound("No se ha conseguido el paciente."); }

            string guidString = citaDto.doctor;
            Guid guid;
            if (Guid.TryParse(guidString, out guid))
            {
                guid = Guid.Parse(guidString);
            }
            else
            {
                return BadRequest("Hubo un error al procesar el doctor");
                // The string could not be parsed into a Guid.
            }

            var Cita = new cita
            {
                uuid = Guid.NewGuid(), // Generate a new uuid for cita
                fk_persona_paciente = paciente.Uuid,
                fk_perfil_doctor = guid,
                statusconsulta = 0 // Set statusconsulta to '0' as requested
            };

            _context.cita.Add(Cita);
            await _context.SaveChangesAsync();

            return Ok("Cita creada satisfactoriamente");
        }

    }
}
