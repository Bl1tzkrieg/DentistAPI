using AplicacionWebFrontend.DTO;
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

            if (HttpContext.Current.Session["info_usuario"] != null)
            {
                UsuarioAccess_DTO usuario = HttpContext.Current.Session["info_usuario"] as UsuarioAccess_DTO;

                if (usuario.rol != this.rol)
                {
                    filterContext.Result = new RedirectResult("~/Acceso/SinPermisos");
                }
            }

            base.OnActionExecuting(filterContext);

        }
    }
}