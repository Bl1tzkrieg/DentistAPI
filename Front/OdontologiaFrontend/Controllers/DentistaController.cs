using OdontologiaFrontend.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdontologiaFrontend.Controllers
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

        [PermisosRol(rol: 2)]
        public ActionResult Consultas()
        {
            return View();
        }

        [PermisosRol(rol: 2)]
        public ActionResult Insumos()
        {
            return View();
        }
    }
}