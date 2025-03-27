using proyecto_poderosa_documento.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyecto_poderosa_documento.Controllers
{
    public class NoticiasController : Controller
    {
        private NoticiasDbContext db = new NoticiasDbContext();

        // Acción para el Dashboard de Admin
        [Authorize]
        public ActionResult Dashboard()
        {
            var model = new DashboardViewModel
            {
                Noticias = db.Noticias.OrderByDescending(n => n.FechaPublicacion).ToList(),
                PopUps = db.PopUp.OrderByDescending(p => p.FechaCreacion).ToList()
            };

            return View(model);
        }

        // Index - Mostrar todas las noticias
        public ActionResult Index()
        {
            var noticias = db.Noticias.OrderByDescending(n => n.FechaPublicacion).ToList();
            return View(noticias);
        }

        // Crear - Vista para crear una nueva noticia
        [Authorize]// Solo requerir autenticación para esta acción
        public ActionResult Create()
        {
            var model = new Noticia();
            return View(model);
        }

        // Crear - Guardar una nueva noticia
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [ValidateInput(false)]
        public ActionResult Create(Noticia noticia, HttpPostedFileBase Imagen, HttpPostedFileBase ImagenResumen)
        {
            // Verificar si la fecha fue proporcionada por el usuario
            if (!noticia.FechaNoticia.HasValue)
            {
                ModelState.AddModelError("FechaNoticia", "¡La fecha es obligatoria!");
                return View(noticia);
            }

            // Verificar si el contenido fue proporcionado
            if (string.IsNullOrWhiteSpace(noticia.Contenido))
            {
                ModelState.AddModelError("Contenido", "¡El contenido es obligatorio!");
                return View(noticia);
            }

            if (ModelState.IsValid)
            {
                var usuarioNombre = User.Identity.Name;
                var usuario = db.Usuarios.FirstOrDefault(u => u.NombreUsuario == usuarioNombre);

                if (usuario != null)
                {
                    noticia.UsuarioId = usuario.Id;
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }

                if (Imagen != null && Imagen.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(Imagen.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/noticias/banner"), fileName);
                    Imagen.SaveAs(path);
                    noticia.Imagen = "~/Content/noticias/banner/" + fileName;
                }

                if (ImagenResumen != null && ImagenResumen.ContentLength > 0)
                {
                    var fileNameResumen = Path.GetFileName(ImagenResumen.FileName);
                    var pathResumen = Path.Combine(Server.MapPath("~/Content/noticias/resumen"), fileNameResumen);
                    ImagenResumen.SaveAs(pathResumen);
                    noticia.ImagenResumen = "~/Content/noticias/resumen/" + fileNameResumen;
                }

                noticia.FechaPublicacion = noticia.FechaNoticia.Value;
                db.Noticias.Add(noticia);
                db.SaveChanges();

                return RedirectToAction("Dashboard", "Noticias");
            }
            return View(noticia);
        }

        // Details - Ver detalles de una noticia
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

        // Acción para cargar el formulario de edición (GET)
        public ActionResult Edit(string titulo)
        {
            var noticia = db.Noticias.FirstOrDefault(n => n.Titulo.Replace(" ", "-").ToLower() == titulo.ToLower()); // Buscar la noticia por título
            if (noticia == null)
            {
                return HttpNotFound(); // Si no se encuentra la noticia, muestra error
            }
            return View(noticia); // Pasa la noticia a la vista para editar
        }

        // Acción para procesar el formulario de edición (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Noticia noticia, HttpPostedFileBase Imagen, HttpPostedFileBase ImagenResumen)
        {
            if (ModelState.IsValid)
            {
                var existingNoticia = db.Noticias.Find(noticia.Id);
                if (existingNoticia == null)
                {
                    return HttpNotFound();
                }

                // Update existingNoticia properties with new values
                if (Imagen != null && Imagen.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(Imagen.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/noticias/banner"), fileName);
                    Imagen.SaveAs(path);
                    existingNoticia.Imagen = "~/Content/noticias/banner/" + fileName;
                }

                if (ImagenResumen != null && ImagenResumen.ContentLength > 0)
                {
                    var fileNameResumen = Path.GetFileName(ImagenResumen.FileName);
                    var pathResumen = Path.Combine(Server.MapPath("~/Content/noticias/resumen"), fileNameResumen);
                    ImagenResumen.SaveAs(pathResumen);
                    existingNoticia.ImagenResumen = "~/Content/noticias/resumen/" + fileNameResumen;
                }

                existingNoticia.Titulo = noticia.Titulo;
                existingNoticia.Descripcion = noticia.Descripcion;
                existingNoticia.Contenido = noticia.Contenido;

                // Verificar y establecer una fecha válida para FechaPublicacion
                if (noticia.FechaPublicacion == DateTime.MinValue)
                {
                    noticia.FechaPublicacion = DateTime.Now; // o cualquier otra fecha predeterminada válida
                }
                existingNoticia.FechaPublicacion = noticia.FechaPublicacion;

                db.Entry(existingNoticia).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Dashboard", "Noticias");
            }
            return View(noticia);
        }

        // Eliminar una noticia
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Buscar la noticia por su ID
            var noticia = db.Noticias.Find(id);

            if (noticia != null)
            {
                db.Noticias.Remove(noticia);  // Eliminar la noticia
                db.SaveChanges();  // Guardar cambios en la base de datos
            }
            return RedirectToAction("Dashboard", "Noticias");  // Redirigir a la lista de noticias
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deactivate(int id)
        {
            var popUp = db.PopUp.Find(id);
            if (popUp == null)
            {
                return HttpNotFound();
            }

            // Desactivar el PopUp
            popUp.IsActive = false;
            db.Entry(popUp).State = EntityState.Modified;
            db.SaveChanges();

            // Redirigir al Dashboard de Noticias después de desactivar el PopUp
            return RedirectToAction("Dashboard", "Noticias");
        }
    }
}
