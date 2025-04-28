using System.Web.Mvc;

namespace proyecto_poderosa_documento.Controllers
{
    public class planner_2022Controller : Controller
    {
        // GET: planner_2022
        public ActionResult Index()
        {
            return View();
            // return RedirectToAction("reglas_de_oro_por_la_vida");
        }

        public ActionResult trabajo_en_equipo()
        {
            return View();
        }
        public ActionResult la_hora_del_planeta()
        {
            return View();
        }

        public ActionResult dia_de_la_mujer()
        {
            return View();
        }

        public ActionResult reglas_de_oro_por_la_vida()
        {
            return View();
        }

        public ActionResult saludo_2022()
        {
            return View();
        }

        public ActionResult historico()
        {
            return View();
        }
    }
}
