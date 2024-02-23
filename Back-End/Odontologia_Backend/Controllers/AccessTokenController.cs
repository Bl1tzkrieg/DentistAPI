using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Odontologia_Backend.Class;
using Odontologia_Backend.Data;
using Odontologia_Backend.DTOs;
using Odontologia_Backend.Helper;
using Odontologia_Backend.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Odontologia_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessTokenController : ControllerBase
    {
        private readonly IConfiguration config_System;
        //private readonly Context_MySql contextMysql;
        private AccessToken accessUser;

        public AccessTokenController(Context_MySql dbMysql, IConfiguration config)
        {
            this.config_System = config;
            this.accessUser = new(dbMysql);
        }

        /// <summary>
        /// Verifica que las credenciales recibidas cumplan con los criterios establecidos
        /// para retornar un token con toda la permisologia del usuario.
        /// </summary>
        /// <param name="user">Credenciales</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> StartSession([FromBody] UserAccess_DTO user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }

                Usuario? usuario = await accessUser.GetUser(user.username, user.password);
                //DateTime _ValidToken = DateTime.Now.AddDays(1);

                if (usuario is null) return Unauthorized("Usuario o password Invalidos");

                if (usuario.Usu_Status.Equals(true)) return Unauthorized("tus privilegios han sido anulados");

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, config_System["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    // informacion del usuario
                    new Claim("username", usuario.Usu_UserName.ToString() ),
                    new Claim("rol", usuario.Usu_Rol_Id.ToString() ),
                    new Claim("status", usuario.Usu_Status.ToString() )
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config_System["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                        config_System["Jwt:Issuer"],
                        config_System["Jwt:Audience"],
                        claims.ToArray(),
                        //expires: _ValidToken,
                        signingCredentials: signIn);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, default, 500);
            }

        }


    } // fin class AccessTokenController
}
