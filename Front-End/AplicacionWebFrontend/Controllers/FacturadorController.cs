using AplicacionWebFrontend.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicacionWebFrontend.Controllers
{
    [Authorize]
    public class FacturadorController : Controller
    {
        // GET: Facturador
        [PermisosRol(rol: 3)]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Facturador
        [PermisosRol(rol: 3)]
        public ActionResult Consultas()
        {
            return View();
        }

        // GET: Facturador
        [PermisosRol(rol: 3)]
        public ActionResult Ventas()
        {
            return View();
        }

        // GET: Facturador
        [PermisosRol(rol: 3)]
        public ActionResult Compras()
        {
            return View();
        }

        // GET: Facturador
        [PermisosRol(rol: 3)]
        public ActionResult Insumos()
        {
            return View();
        }
    }
}