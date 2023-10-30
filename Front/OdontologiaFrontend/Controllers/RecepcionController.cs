using OdontologiaFrontend.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdontologiaFrontend.Controllers
{
    [Authorize]
    public class RecepcionController : Controller
    {
        [PermisosRol(rol: 4)]
        // GET: Recepcion
        public ActionResult Index()
        {
            return View();
        }

        [PermisosRol(rol: 4)]
        // GET: Recepcion
        public ActionResult Consultas()
        {
            return View();
        }

        [PermisosRol(rol: 4)]
        // GET: Recepcion
        public ActionResult RegistrarConsulta()
        {
            return View();
        }
    }
}