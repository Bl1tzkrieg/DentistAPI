using DentistAPI.Models;
using DentistAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DentistAPI.Helper
{
    public class UserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public acceso CreateEncascadedUser(string username, string password, string correo, int fkPerfil)
        {
            var token = GenerateToken(username); // Generate token in username:token format

            // Log the generated token for debugging purposes
            Console.WriteLine($"Generated Token: {token}");

            var base64EncodedToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(token)); // Encode token in Base64

            var acceso = new acceso
            {
                pass = HashPassword(username, password),
                username = username,
                correo = correo,
                token = base64EncodedToken, // Store Base64 encoded token
                fk_perfil = fkPerfil
            };

            _context.acceso.Add(acceso);
            _context.SaveChanges();

            return acceso;
        }

        private string GenerateToken(string username = null)
        {
            var guid = Guid.NewGuid().ToString("N"); // Generate a new GUID as the token

            if (username != null)
            {
                return $"{username}:{guid}"; // Format token as username:guid
            }

            return guid; // Return only the GUID without the username prefix
        }

        public cita GetCitaByUUID(Guid uuid)
        {
            return _context.cita.FirstOrDefault(c => c.uuid == uuid);
        }

        public acceso InitiateSession(string username, string password)
        {
            var hashedPassword = HashPassword(username, password);
            var acceso = _context.acceso.FirstOrDefault(a => a.username == username && a.pass == hashedPassword);

            if (acceso != null)
            {
                var newToken = GenerateToken(); // Generate a new token in GUID format without the username prefix
                acceso.token = Convert.ToBase64String(Encoding.UTF8.GetBytes(newToken)); // Store the token as Base64 in the database
                _context.SaveChanges();
                return acceso;
            }
            return null;
        }

        public bool AuthenticateToken(string username, string receivedToken)
        {
            var storedTokenFromDatabase = _context.acceso
                .Where(a => a.username == username)
                .Select(a => a.token)
                .FirstOrDefault();

            if (storedTokenFromDatabase != null)
            {
                var decodedBytes = Convert.FromBase64String(storedTokenFromDatabase);
                var storedToken = Encoding.UTF8.GetString(decodedBytes);

                // Ensure the decoded storedToken has the same format as receivedToken
                return storedToken == receivedToken;
            }

            return false;
        }


        private string HashPassword(string username, string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(username + password + "SAL"));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public async Task<object> GetDentistas()
        {
            var dentistas = await _context.persona
                .Join(
                    _context.perfil,
                    persona => persona.uuid,
                    perfil => perfil.fk_persona,
                    (persona, perfil) => new { Persona = persona, Perfil = perfil }
                )
                .Join(
                    _context.permisos,
                    joined => joined.Perfil.fk_permisos,
                    permiso => permiso.uuid,
                    (joined, permiso) => new { joined.Persona, joined.Perfil, Permiso = permiso }
                )
                .Where(joined => joined.Permiso.rol == 2) // Assuming '2' is the ID for 'dentista' role in permisos table
                .Select(joined => new
                {
                    joined.Persona.uuid,
                    joined.Persona.nombre,
                })
                .ToListAsync();
            return (dentistas);
        }

        public async Task<PersonaDto> GetPersonasbyCedula(int cedula)
        {
            var persona = await _context.persona
            .Where(p => p.cedula == cedula)
            .Select(p => new PersonaDto
            {
                Nombre = p.nombre,
                Nacimiento = p.nacimiento,
                Uuid = p.uuid,
                Nota = p.nota,
                Cedula = p.cedula,
            })
            .FirstOrDefaultAsync();
            return persona;
        }
    }
}
