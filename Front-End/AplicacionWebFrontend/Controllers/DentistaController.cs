using AplicacionWebFrontend.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicacionWebFrontend.Controllers
{
    [Authorize]
    public class DentistaController : Controller
    {
        // GET: Dentista
        [PermisosRol(rol: 2)]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Dentista
        [PermisosRol(rol: 2)]
        public ActionResult Consultas()
        {
            return View();
        }

        // GET: Dentista
        [PermisosRol(rol: 2)]
        public ActionResult Insumos()
        {
            return View();
        }
    }
}