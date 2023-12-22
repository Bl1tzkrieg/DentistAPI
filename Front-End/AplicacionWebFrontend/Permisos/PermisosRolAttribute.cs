using AplicacionWebFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicacionWebFrontend.Permisos
{
    public class PermisosRolAttribute : ActionFilterAttribute
    {
        private int rol;

        public PermisosRolAttribute( int rol )
        {
            this.rol = rol;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (HttpContext.Current.Session["usuario"] != null)
            {
                Usuario usuario = HttpContext.Current.Session["usuario"] as Usuario;

                if (usuario.ROLL != this.rol)
                {
                    filterContext.Result = new RedirectResult("~/Acceso/SinPermisos");
                }
            }

            base.OnActionExecuting(filterContext);

        }
    }
}