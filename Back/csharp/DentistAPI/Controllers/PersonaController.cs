using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DentistAPI.Models;
using DentistAPI.Models.DTO;
using Microsoft.EntityFrameworkCore.Storage;
using DentistAPI.Helper;
using Microsoft.EntityFrameworkCore;

namespace DentistAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserRepository userRep;
        public PersonaController(AppDbContext Context, UserRepository userRep)
        {
            _context = Context;
            this.userRep = userRep;
        }
        [HttpPost("createpersonas")]
        public async Task<IActionResult> CreatePersona([FromBody] persona_DTO _DTO)
        {
            using IDbContextTransaction uow = _context.Database.BeginTransaction();
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
            }

            try 
            {
                var persona = new persona
                {
                    uuid = Guid.NewGuid(), // Generate a new uuid automatically
                    nombre = _DTO.Nombre,
                    nacimiento = _DTO.Nacimiento,
                    nota = _DTO.Nota,
                    cedula = _DTO.Cedula
                };

                _context.persona.Add(persona);
                _context.SaveChanges();

                //return CreatedAtAction("GetPersona", new { id = persona.id }, persona);
                await uow.CommitAsync();
                return Ok("test"); 

            }
            catch (Exception ex)
            {
                await uow.RollbackAsync();
                return BadRequest($"Solicitud no pudo ser procesada, se ha generado la siguiente exception: {((ex.InnerException != null) ? ex.InnerException.Message : ex.Message)}");
            }
        }

        [HttpGet("personadetails")]
        public async Task<ActionResult<object>> GetPersonaByCedula([FromQuery] int cedula)
        {
            var persona = userRep.GetPersonasbyCedula(cedula);

            if (persona.Result == null)
            {
                return NotFound("No hay resultados de la busqueda");
            }
            return Ok(persona.Result);
        }

    }


}

