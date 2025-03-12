using System.Web.Mvc;

namespace proyecto_poderosa_documento.Controllers
{
    public class publicacionesController : Controller
    {
        // GET: publicaciones
        public ActionResult index()
        {
            return View();
        }
        public ActionResult publicaciones_no_periodicas()
        {
            return View();
        }

        public ActionResult boletines_informativos()
        {
            return View();
        }

        public ActionResult coleccionables()
        {
            return View();
        }

        public ActionResult batolito()
        {
            return View();
        }

        public ActionResult batolito_comunitario()
        {
            return View();
        }

        public ActionResult memorias()
        {
            return View();
        }

        public ActionResult memoria_2018()
        {
            return View();
        }

        public ActionResult libros()
        {
            return View();
        }
        public ActionResult reportes_de_sostenibilidad()
        {
            return View();
        }
    }
}