using proyecto_poderosa_documento.Models;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyecto_poderosa_documento.Controllers
{
    public class NoticiasController : Controller
    {
        private NoticiasDbContext db = new NoticiasDbContext();

        // Index - Mostrar todas las noticias
        public ActionResult Index()
        {
            var noticias = db.Noticias.OrderByDescending(n => n.FechaPublicacion).ToList();
            return View(noticias);
        }

        // Crear - Vista para crear una nueva noticia
        public ActionResult Create()
        {
            return View();
        }

        // Crear - Guardar una nueva noticia
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Noticia noticia, HttpPostedFileBase Imagen, HttpPostedFileBase ImagenResumen)
        {
            if (ModelState.IsValid)
            {
                // Verificar si se ha subido una imagen
                if (Imagen != null && Imagen.ContentLength > 0)
                {
                    // Generar un nombre único para la imagen
                    var fileName = Path.GetFileName(Imagen.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/noticias/banner"), fileName);

                    // Guardar la imagen en el servidor
                    Imagen.SaveAs(path);

                    // Asignar la ruta de la imagen al modelo
                    noticia.Imagen = "~/Content/noticias/banner/" + fileName;
                }

                // Verificar si se ha subido una imagen resumen
                if (ImagenResumen != null && ImagenResumen.ContentLength > 0)
                {
                    // Generar un nombre único para la imagen resumen
                    var fileNameResumen = Path.GetFileName(ImagenResumen.FileName);
                    var pathResumen = Path.Combine(Server.MapPath("~/Content/noticias/resumen"), fileNameResumen);

                    // Guardar la imagen resumen en el servidor
                    ImagenResumen.SaveAs(pathResumen);

                    // Asignar la ruta de la imagen resumen al modelo
                    noticia.ImagenResumen = "~/Content/noticias/resumen/" + fileNameResumen;
                }

                // Establecer la fecha actual
                noticia.FechaPublicacion = DateTime.Now;

                // Guardar la noticia en la base de datos
                db.Noticias.Add(noticia);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(noticia);
        }
        public ActionResult Details(string titulo)
        {
            var noticia = db.Noticias.FirstOrDefault(n => n.Titulo.Replace(" ", "-").ToLower() == titulo.ToLower());
            if (noticia == null)
            {
                return HttpNotFound();
            }
            // Obtener las 3 últimas noticias, ordenadas por fecha de publicación
            var ultimasNoticias = db.Noticias
                .OrderByDescending(n => n.FechaPublicacion)  // Ordenar por fecha de publicación
                .Take(3)  // Limitar a las 3 últimas noticias
                .ToList();

            // Pasar tanto la noticia encontrada como las 3 últimas noticias a la vista
            ViewBag.UltimasNoticias = ultimasNoticias;
            return View(noticia);  // Pasa la noticia encontrada a la vista
        }
    }
}