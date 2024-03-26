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
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(new { username = user.USUARIO, password = user.PASSWORD });
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync("https://localhost:7091/api/AccessToken", content);

                    if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        string message = await response.Content.ReadAsStringAsync();
                        return Json(new { statuscode = (int)response.StatusCode, message, data = string.Empty }, JsonRequestBehavior.AllowGet);
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                        return Json(new { statuscode = (int)response.StatusCode, message = "Internal server error", data = string.Empty }, JsonRequestBehavior.AllowGet);

                    string contenido = await response.Content.ReadAsStringAsync();
                    UsuarioAccess_DTO _user = JsonConvert.DeserializeObject<UsuarioAccess_DTO>(contenido);

                    // usuario autenticado
                    FormsAuthentication.SetAuthCookie(_user.username, false);

                    // estableciendo valores de sesión 
                    Session["info_usuario"] = _user;
                    Session["perfil"] = getRolName(_user.rol);

                    return Json(new { statuscode = (int)response.StatusCode, message = "acceso permitido", data = _user }, JsonRequestBehavior.AllowGet);
                    
                }
            }
            catch (Exception ex)
            {
                return Json(new { statuscode = 500, message = ex.Message, data = string.Empty }, JsonRequestBehavior.AllowGet);
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