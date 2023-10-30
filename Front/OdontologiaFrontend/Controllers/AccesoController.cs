using OdontologiaFrontend.Data;
using OdontologiaFrontend.DTO;
using OdontologiaFrontend.Models;
using OdontologiaFrontend.Tools;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OdontologiaFrontend.Controllers
{
    public class AccesoController : Controller
    {
        private string sql = string.Empty;

        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        // este es para registrar un usuario nuevo  ejemplo de peticion registrar
        [HttpPost]
        public async Task<ActionResult> Registrar(Usuario_DTO user)
        {
            bool registrado = false;
            string mensaje;

            if (user.PASSWORD == user.CONFIRMARPASSWORD)
            {
                user.PASSWORD = Security.Encryptar(user.PASSWORD);
            }
            else
            {
                ViewData["Mensaje"] = "la contrasenia no coincide";
                return View();
            }

            using (ContextSql contextSql = new ContextSql())
            {
                List<SqlParameter> parametros = new List<SqlParameter> {
                    new SqlParameter("@var1", user.CORREO ),
                    new SqlParameter("@var2", user.PASSWORD ),
                    new SqlParameter("@var3", Convert.ToInt32(0) ),
                };

                Usuario statusUsuario = await contextSql.USUARIO.Where(x => x.CORREO == user.CORREO).FirstOrDefaultAsync();

                if (statusUsuario != null)
                {
                    mensaje = "Correo ya existe!";
                    ViewData["Mensaje"] = mensaje;
                    return View();
                }

                this.sql = "INSERT INTO USUARIO(correo, password, roll) VALUES (@var1, @var2, @var3);";

                var statusRegistro = await contextSql.Database.ExecuteSqlCommandAsync(this.sql, parametros.ToArray());

                if (statusRegistro > 0)
                {
                    await contextSql.SaveChangesAsync();
                    registrado = true;
                    mensaje = "Registrado Con Exito";
                    ViewData["Mensaje"] = mensaje;
                }

            }

            if (registrado)
            {
                ViewData["Registrado"] = "Registro Exitoso";
                return RedirectToAction("Login", "Acceso");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login(Usuario_DTO user)
        {
            user.PASSWORD = Security.Encryptar(user.PASSWORD);

            using (ContextSql contextSql = new ContextSql())
            {

                Usuario u = contextSql.USUARIO.FirstOrDefault(x => x.CORREO == user.CORREO && x.PASSWORD == user.PASSWORD);

                if (u is null)
                {
                    // ViewData["Mensaje"] = "usuario no encontrado";
                    // return View();

                    var noRegistrado = new {
                        registrado = false
                    };

                    return Json(noRegistrado,JsonRequestBehavior.AllowGet);
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

                    return Json(usuario,JsonRequestBehavior.AllowGet);


                    /*
                    string metodo = string.Empty;
                    string controller = string.Empty;

                    switch (u.ROLL)
                    {
                        case 1:
                            metodo = "Index";
                            controller = "Admin";
                            break;
                        case 2:
                            metodo = "Index";
                            controller = "Dentista";
                            break;
                        case 3:
                            metodo = "Index";
                            controller = "Facturador";
                            break;
                        case 4:
                            metodo = "Index";
                            controller = "Recepcion";
                            break;
                        default:
                            metodo = "Index";
                            controller = "Acceso";
                            break;
                    }
                    

                    // con esto authentico para acceder a los controlladores
                    FormsAuthentication.SetAuthCookie(u.CORREO, false);

                    Session["usuario"] = u;
                    Session["perfil"] = controller;
                    return RedirectToAction(metodo, controller);
                    */



                }

            }
        }

        // GET
        public ActionResult Cerrar()
        {
            FormsAuthentication.SignOut();
            Session["usuario"] = null;
            Session.Clear();
            return RedirectToAction("Login", "Acceso");
        }

        // GET
        public ActionResult SinPermisos()
        {
            return View();
        }
    }
}