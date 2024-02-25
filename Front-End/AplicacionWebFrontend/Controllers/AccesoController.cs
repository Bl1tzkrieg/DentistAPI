using AplicacionWebFrontend.Data;
using AplicacionWebFrontend.DTO;
using AplicacionWebFrontend.Models;
using AplicacionWebFrontend.Tools;
using Microsoft.Ajax.Utilities;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Permissions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace AplicacionWebFrontend.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(Usuario_DTO user)
        {

            try
            {
                var api_Request = new RestClient("https://localhost:7091/");
                var request = new RestRequest("api/AccessToken", Method.Post );

                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(new { username = user.USUARIO, password = user.PASSWORD });

                var respuesta = await api_Request.PostAsync(request);

                if (respuesta.StatusCode == 0)
                {
                    throw new Exception(respuesta.ErrorMessage);
                }

                if (respuesta.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new Exception(respuesta.StatusDescription);
                }

                string contenido = respuesta.Content;
                UsuarioAccess_DTO _user = JsonConvert.DeserializeObject<UsuarioAccess_DTO>(contenido);

                // usuario authenticado
                FormsAuthentication.SetAuthCookie(_user.username, false);

                // estableciendo valores de session 
                Session["info_usuario"] = _user;
                Session["perfil"] = getRolName(_user.rol);

                return Json(_user,JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        // GET
        public ActionResult Cerrar()
        {
            FormsAuthentication.SignOut();
            Session["usuario"] = null;
            Session.Clear();
            return RedirectToAction("Index", "Acceso");
        }

        // GET
        public ActionResult SinPermisos()
        {
            return View();
        }

        private string getRolName( byte rol )
        {
            switch (rol)
            {
                case 1: return "Admin";
                case 2: return "Dentista";
                case 3: return "Facturador";
                case 4: return "Recepcion";
            }

            return "";
        }

    }
}