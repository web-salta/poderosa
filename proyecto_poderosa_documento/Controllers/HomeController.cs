using proyecto_poderosa_documento.Models;
using System.Linq;
using System.Web.Mvc;

namespace proyecto_poderosa_documento.Controllers
{
    public class HomeController : Controller
    {
        private readonly PopUpDbContext db = new PopUpDbContext(); // Add this line to define the db context
        private readonly NoticiasDbContext noticiasDb = new NoticiasDbContext(); // Contexto para Noticias
        public ActionResult Index()
        {
            var popUp = db.PopUp.Where(p => p.IsActive).OrderByDescending(p => p.FechaCreacion).FirstOrDefault(); // Obtener el último PopUp insertado
            ViewBag.PopUp = popUp; // Pasarlo a la vista


            // Obtener las tres últimas noticias
            var ultimasNoticias = noticiasDb.Noticias
                .OrderByDescending(n => n.FechaPublicacion)
                .Take(3)
                .ToList();
            ViewBag.UltimasNoticias = ultimasNoticias; // Pasarlas a la vista
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