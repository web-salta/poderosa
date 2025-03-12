using Helper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyecto_poderosa_documento.Areas.Admin.Filters;

namespace proyecto_poderosa_documento.Areas.Admin.Controllers
{
   
    public class LoginController : Controller
    {

        // GET: Admin/Login
        [NoLogin]
        public ActionResult Index()
        {
            return View();
        }

        private usuario usuario = new usuario();
        public JsonResult Acceder(string nom_usu, string clave)
        {
            var rm = usuario.Acceder(nom_usu, clave);
            if (rm.response)
            {
                rm.href = Url.Content("~/admin/documento");
            }        
            return Json(rm);
        }

        public ActionResult Logout()
        {
            //elimina la session
            SessionHelper.DestroyUserSession();
            //direcciona al front-office
            return Redirect("~/admin/login");
        }
    }
}