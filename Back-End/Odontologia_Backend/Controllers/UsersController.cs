using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Odontologia_Backend.Class;
using Odontologia_Backend.Data;
using Odontologia_Backend.DTOs;
using Odontologia_Backend.Helper;
using Odontologia_Backend.Models;

namespace Odontologia_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // private readonly IConfiguration config_System;
        private HandlerUsers recursos_Usuarios;

        public UsersController(Context_MySql dbMysql)
        {
            this.recursos_Usuarios = new HandlerUsers(dbMysql);
        }

        [HttpGet("obtener_usuario")]
        public async Task<ActionResult> GetUsuario([FromQuery] int v1 = -1 )
        {
            try
            {
                List<Usuario> user = await this.recursos_Usuarios.ObtenerUsuario(v1);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, default, 500);
            }
        }

        [HttpPost("registrar_usuario")]
        public async Task<ActionResult> PostUsuario([FromBody] User_DTO user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }

                (int statusRegistro, string message) = await this.recursos_Usuarios.RegistrarUsuario( user );

                return statusRegistro > 0 ? StatusCode(201) : BadRequest();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, default, 500);
            }
        }

        [HttpPatch("actualizar_usuario")]
        public async Task<ActionResult> PatchUsuario([FromBody] UserUpdate_DTO user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }

                (int status, string message) = await this.recursos_Usuarios.ActualizarUsuario(new Usuario
                {
                    Usu_UserName = user.username.ToUpper(),
                    Usu_Password = user.password,
                    Usu_Status = user.username_status,
                    Usu_Rol_Id = user.rol,
                });

                return status > 0 ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, default, 500);
            }
        }

    }
}
