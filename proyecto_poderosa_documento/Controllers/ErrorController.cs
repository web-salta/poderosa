using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyecto_poderosa_documento.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(string mensaje, int error = 0)
        {
            switch (error)
            {
                case 505:
                    ViewBag.Title = "505 Ocurrio un error inesperado";
                    ViewBag.Description = "Ocurrio un error inesperado";

                    break;

                case 404:
                    ViewBag.Title = "404 Página no encontrada";
                    ViewBag.Description = "La URL que está intentando ingresar no existe";
                    break;
                case 403:
                    ViewBag.Title = "403 Usted no tiene permisos ";
                    ViewBag.Description = "La URL que está intentando ingresar no existe";
                    break;

                default:
                    ViewBag.Title = "Página no encontrada";
                    ViewBag.mensaje = mensaje;
                    ViewBag.Description = "Ocurrio un error inesperado";
                    break;
            }

            return View("~/views/error/ErrorPage.cshtml");
        }
    }
}