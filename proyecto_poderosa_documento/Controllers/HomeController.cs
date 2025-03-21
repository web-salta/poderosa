using proyecto_poderosa_documento.Models; // Add this line to include the namespace for your models
using System.Linq;
using System.Web.Mvc;

namespace proyecto_poderosa_documento.Controllers
{
    public class HomeController : Controller
    {
        private readonly PopUpDbContext db = new PopUpDbContext(); // Add this line to define the db context

        public ActionResult index()
        {
            var popUp = db.PopUp.Where(p => p.IsActive).OrderByDescending(p => p.FechaCreacion).FirstOrDefault(); // Obtener el último PopUp insertado
            ViewBag.PopUp = popUp; // Pasarlo a la vista

            return View();
        }
        public ActionResult proveedores()
        {
            return View();
        }
        public ActionResult contacto()
        {
            return View();
        }
    }
}