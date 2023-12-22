using AplicacionWebFrontend.Data;
using AplicacionWebFrontend.DTO;
using AplicacionWebFrontend.Models;
using AplicacionWebFrontend.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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
        public ActionResult Login( Usuario_DTO user)
        {
            user.PASSWORD = Security.Encryptar( user.PASSWORD );

            using( CatalogSql contextSql = new CatalogSql() )
            {
                Usuario u = contextSql.USUARIO.FirstOrDefault( x => x.CORREO == user.CORREO && x.PASSWORD == user.PASSWORD );

                if( u is null)
                {
                    var noRegistrado = new
                    {
                        registrado = false
                    };

                    return Json(noRegistrado, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    // con esto authentico para acceder a los controlladores
                    FormsAuthentication.SetAuthCookie(u.CORREO, false);

                    string rol = string.Empty;

                    switch (u.ROLL)
                    {
                        case 1:
                            rol = "Admin";
                            break;
                        case 2:
                            rol = "Dentista";
                            break;
                        case 3:
                            rol = "Facturador";
                            break;
                        case 4:
                            rol = "Recepcion";
                            break;
                        default:
                            rol = "Acceso";
                            break;
                    }

                    Session["usuario"] = u;
                    Session["perfil"] = rol;
                    // el token ya se guardo antes

                    var usuario = new
                    {
                        userName = u.CORREO,
                        perfil = u.ROLL,
                        rol,
                        registrado = true
                    };

                    return Json(usuario, JsonRequestBehavior.AllowGet);
                }
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


    }
}