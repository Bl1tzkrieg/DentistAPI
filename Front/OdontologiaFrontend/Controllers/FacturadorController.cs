using OdontologiaFrontend.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdontologiaFrontend.Controllers
{
    [Authorize]
    public class FacturadorController : Controller
    {
        [PermisosRol(rol: 3)]
        // GET: Facturador
        public ActionResult Index()
        {
            return View();
        }

        [PermisosRol(rol: 3)]
        // GET: Facturador
        public ActionResult Consultas()
        {
            return View();
        }

        [PermisosRol(rol: 3)]
        // GET: Facturador
        public ActionResult Facturas()
        {
            return View();
        }

        [PermisosRol(rol: 3)]
        // GET: Facturador
        public ActionResult Insumos()
        {
            return View();
        }

        [PermisosRol(rol: 3)]
        // GET: Facturador
        public ActionResult Proveedores()
        {
            return View();
        }
    }
}