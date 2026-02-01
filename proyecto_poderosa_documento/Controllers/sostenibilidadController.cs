using System.Web.Mvc;

namespace proyecto_poderosa_documento.Controllers
{
    public class sostenibilidadController : Controller
    {
        // GET: sostenibilidad
        public ActionResult gestion_ambiental(string accion)
        {
            // Determinar la vista a devolver en función del parámetro
            switch (accion?.ToLower())
            {
                case "introduccion":
                    return View("~/Views/sostenibilidad/gestion_ambiental/introduccion.cshtml");
                case "nuestro-aporte":
                    return View("~/Views/sostenibilidad/gestion_ambiental/nuestro-aporte.cshtml");
                default:
                    return HttpNotFound(); // Si no coincide, retornar 404
            }
        }

        public ActionResult gestion_de_la_energia(string accion)
        {
            // Determinar la vista a devolver en función del parámetro
            switch (accion?.ToLower())
            {
                case "introduccion":
                    return View("~/Views/sostenibilidad/gestion_de_la_energia/introduccion.cshtml");
                case "nuestros-avances-con-energias-limpias":
                    return View("~/Views/sostenibilidad/gestion_de_la_energia/nuestros-avances-con-energias-limpias.cshtml");
                default:
                    return HttpNotFound(); // Si no coincide, retornar 404
            }
        }

        public ActionResult gestion_de_cumplimiento(string accion)
        {
            // Determinar la vista a devolver en función del parámetro
            switch (accion?.ToLower())
            {
                case "introduccion":
                    return View("~/Views/sostenibilidad/gestion_de_cumplimiento/introduccion.cshtml");
                case "canal-de-etica":
                    return View("~/Views/sostenibilidad/gestion_de_cumplimiento/canal-de-etica.cshtml");
                default:
                    return HttpNotFound(); // Si no coincide, retornar 404
            }
        }

        public ActionResult gestion_de_talento(string accion)
        {
            // Determinar la vista a devolver en función del parámetro
            switch (accion?.ToLower())
            {
                case "introduccion":
                    return View("~/Views/sostenibilidad/gestion_de_talento/introduccion.cshtml");
                case "capacitacion":
                    return View("~/Views/sostenibilidad/gestion_de_talento/capacitacion.cshtml");
                case "reclutamiento-y-seleccion":
                    return View("~/Views/sostenibilidad/gestion_de_talento/reclutamiento-y-seleccion.cshtml");
                default:
                    return HttpNotFound(); // Si no coincide, retornar 404
            }
        }

        public ActionResult comunidad(string accion)
        {
            // Determinar la vista a devolver en función del parámetro
            switch (accion?.ToLower())
            {
                case "poblaciones-vecinas":
                    return View("~/Views/sostenibilidad/comunidad/poblaciones-vecinas.cshtml");
                case "desarrollo-social":
                    return View("~/Views/sostenibilidad/comunidad/desarrollo-social.cshtml");
                case "asociacion-pataz":
                    return View("~/Views/sostenibilidad/comunidad/asociacion-pataz.cshtml");
                default:
                    return HttpNotFound(); // Si no coincide, retornar 404
            }
        }
    }
}
