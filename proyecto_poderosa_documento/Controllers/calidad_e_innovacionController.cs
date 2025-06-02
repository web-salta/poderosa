using System.Web.Mvc;

namespace proyecto_poderosa_documento.Controllers
{
    public class calidad_e_innovacionController : Controller
    {
        // GET: calidad_e_innovacion
        public ActionResult mejora_continua(string accion)
        {
            // Determinar la vista a devolver en función del parámetro
            switch (accion?.ToLower())
            {
                case "introduccion":
                    return View("~/Views/calidad_e_innovacion/mejora_continua/introduccion.cshtml");
                default:
                    return HttpNotFound(); // Si no coincide, retornar 404
            }
        }

        public ActionResult gestion_de_calidad(string accion)
        {
            // Determinar la vista a devolver en función del parámetro
            switch (accion?.ToLower())
            {
                case "introduccion":
                    return View("~/Views/calidad_e_innovacion/gestion_de_calidad/introduccion.cshtml");
                default:
                    return HttpNotFound(); // Si no coincide, retornar 404
            }
        }

        public ActionResult colpa_5s(string accion)
        {
            // Determinar la vista a devolver en función del parámetro
            switch (accion?.ToLower())
            {
                case "introduccion":
                    return View("~/Views/calidad_e_innovacion/colpa_5s/introduccion.cshtml");
                default:
                    return HttpNotFound(); // Si no coincide, retornar 404
            }
        }

        public ActionResult calidad_total(string accion)
        {
            // Determinar la vista a devolver en función del parámetro
            switch (accion?.ToLower())
            {
                case "introduccion":
                    return View("~/Views/calidad_e_innovacion/calidad_total/introduccion.cshtml");
                default:
                    return HttpNotFound(); // Si no coincide, retornar 404
            }
        }

        public ActionResult innovacion(string accion)
        {
            // Determinar la vista a devolver en función del parámetro
            switch (accion?.ToLower())
            {
                case "introduccion":
                    return View("~/Views/calidad_e_innovacion/innovacion/introduccion.cshtml");
                case "nuestras-principales-iniciativas-en-innovacion":
                    return View("~/Views/calidad_e_innovacion/innovacion/nuestras-principales-iniciativas-en-innovacion.cshtml");
                default:
                    return HttpNotFound(); // Si no coincide, retornar 404
            }
        }
    }
}
