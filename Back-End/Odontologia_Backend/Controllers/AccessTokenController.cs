using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Odontologia_Backend.Data;
using Odontologia_Backend.DTOs;

namespace Odontologia_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessTokenController : ControllerBase
    {
        private readonly IConfiguration config_System;
        private readonly Context_MySql contextMysql;

        public AccessTokenController(Context_MySql dbMysql, IConfiguration config)
        {
            this.config_System = config;
            this.contextMysql = dbMysql;
        }

        /// <summary>
        /// Verifica que las credenciales recibidas cumplan con los criterios establecidos
        /// para retornar un token con toda la permisologia del usuario.
        /// </summary>
        /// <param name="user">Credenciales</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> StartSession([FromBody] User_DTO user)
        {
            try
            {
                // desencryptar usuario y password

                // consultar el usuario en la base de datos

                // Si el nombre de usuario y password no coinciden return 401 usuario y clave invalida

                // Si No, Armar el token con la informacion siguiente Rol
                // - Nombre, Rol de usuario y status del usuario.

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }


    } // fin class AccessTokenController
}
