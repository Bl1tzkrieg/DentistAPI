using DentistAPI.Helper;
using DentistAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace DentistAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccesoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserRepository _userRepository;

        public AccesoController(AppDbContext Context, UserRepository userRepository)
        {
            _userRepository = userRepository;
            _context = Context;
        }

        [HttpPost("crear_encascada_usuario")]
        public IActionResult CreateEncascadedUser([FromBody] CreateUserRequest request)
        {
            try
            {
                var acceso = _userRepository.CreateEncascadedUser(request.Username, request.Password, request.Correo, request.FkPerfil);
                return Created($"/api/acceso/{acceso.id}", acceso);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("iniciarsesion_usuario")]
        public IActionResult InitiateSession([FromBody] LoginRequest request)
        {
            try
            {
                var acceso = _userRepository.InitiateSession(request.Username, request.Password);
                if (acceso != null)
                {
                    var token = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{request.Username}:{acceso.token}")); // Include the GUID in the token
                    return Ok(new { Token = token });
                }
                else
                {
                    return Unauthorized(new { message = "Invalid username or password." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("autenticartoken_usuario")]
        public IActionResult AuthenticateToken([FromBody] AuthenticateTokenRequest request)
        {
            try
            {
                var receivedToken = request.Msg;

                var decodedToken = receivedToken.Split(':');
                Console.WriteLine($"Decoded Token: {decodedToken[0]}:{decodedToken[1]}"); // Log the decoded token for debugging purposes

                if (decodedToken.Length == 2)
                {
                    var username = decodedToken[0];
                    var receivedGuid = decodedToken[1]; // This is the GUID portion of the token

                    // Retrieve the stored token from the database based on the username
                    var storedTokenFromDatabase = _context.acceso
                        .Where(a => a.username == username)
                        .Select(a => a.token)
                        .FirstOrDefault();

                    if (storedTokenFromDatabase != null)
                    {
                        // Directly compare the Base64 encoded tokens
                        if (storedTokenFromDatabase == receivedGuid)
                        {
                            return Ok(new { MSG = request.Msg });
                        }
                    }
                }

                return Unauthorized(new { message = "Invalid token format." });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }


    }
}


public class CreateUserRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Correo { get; set; }
    public int FkPerfil { get; set; }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class AuthenticateTokenRequest
{
    public string Msg { get; set; }
}