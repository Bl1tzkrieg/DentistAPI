using OdontologiaFrontend.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdontologiaFrontend.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        [PermisosRol(rol: 1)]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin
        [PermisosRol(rol: 1)]
        public ActionResult Consultas()
        {
            return View();
        }

        // GET: Admin
        [PermisosRol(rol: 1)]
        public ActionResult Compras()
        {
            return View();
        }

        // GET: Admin
        [PermisosRol(rol: 1)]
        public ActionResult Ventas()
        {
            return View();
        }

        // GET: Admin
        [PermisosRol(rol: 1)]
        public ActionResult Usuarios()
        {
            return View();
        }
    }
}