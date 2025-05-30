using proyecto_poderosa_documento.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
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

        // Redirxección desde /Noticias/{slug} a /Noticias/Details/{slug}
        [Route("Noticias/{slug:regex(^((?!Index|Create|Edit|Delete|Dashboard|Details).)*$)}")]
        public ActionResult RedirectToDetails(string slug)
        {
            return RedirectToActionPermanent("Details", new { slug = slug });
        }

        // Crear - Vista para crear una nueva noticia
        [Authorize]// Solo requerir autenticación para esta acción
        public ActionResult Create()
        {
            // Establecer la cultura en español
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES");

            var model = new Noticia();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [ValidateInput(false)]
        public ActionResult Create(Noticia noticia, HttpPostedFileBase Imagen, HttpPostedFileBase ImagenResumen, IEnumerable<HttpPostedFileBase> ImagenesCarrusel, HttpPostedFileBase ImagenesCompartir)
        {
            // Validar que el campo Tipo sea obligatorio y válido
            if (noticia.Tipo != 1 && noticia.Tipo != 2)
            {
                ModelState.AddModelError("Tipo", "Debe seleccionar un tipo válido.");
                return View(noticia);
            }

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

            // Validar que el Slug sea obligatorio
            if (string.IsNullOrWhiteSpace(noticia.Slug))
            {
                ModelState.AddModelError("Slug", "El campo Slug es obligatorio.");
                return View(noticia);
            }

            // Normalizar el Slug proporcionado por el usuario
            noticia.Slug = GenerateSlug(noticia.Slug);

            // Validar que el Slug sea único
            if (db.Noticias.Any(n => n.Slug == noticia.Slug))
            {
                ModelState.AddModelError("Slug", "El Slug proporcionado ya existe. Por favor, elige uno diferente.");
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

                // Validar y guardar Imagen
                if (Imagen != null && Imagen.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(Imagen.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/noticias/banner"), fileName);

                    if (System.IO.File.Exists(path))
                    {
                        ModelState.AddModelError("Imagen", "Ya existe un archivo con el mismo nombre en el servidor.");
                        return View(noticia);
                    }

                    Imagen.SaveAs(path);
                    noticia.Imagen = "~/Content/noticias/banner/" + fileName;
                }

                // Validar y guardar ImagenResumen
                if (ImagenResumen != null && ImagenResumen.ContentLength > 0)
                {
                    var fileNameResumen = Path.GetFileName(ImagenResumen.FileName);
                    var pathResumen = Path.Combine(Server.MapPath("~/Content/noticias/resumen"), fileNameResumen);

                    if (System.IO.File.Exists(pathResumen))
                    {
                        ModelState.AddModelError("ImagenResumen", "Ya existe un archivo con el mismo nombre en el servidor.");
                        return View(noticia);
                    }

                    ImagenResumen.SaveAs(pathResumen);
                    noticia.ImagenResumen = "~/Content/noticias/resumen/" + fileNameResumen;
                }

                // Validar y guardar ImagenesCarrusel
                if (ImagenesCarrusel != null && ImagenesCarrusel.Any())
                {
                    var imagenesCarruselList = new List<string>();
                    foreach (var imagen in ImagenesCarrusel)
                    {
                        if (imagen != null && imagen.ContentLength > 0)
                        {
                            var fileNameCarrusel = Path.GetFileName(imagen.FileName);
                            var pathCarrusel = Path.Combine(Server.MapPath("~/Content/noticias/carrusel"), fileNameCarrusel);

                            if (System.IO.File.Exists(pathCarrusel))
                            {
                                ModelState.AddModelError("ImagenesCarrusel", $"Ya existe un archivo con el nombre {fileNameCarrusel} en el servidor.");
                                return View(noticia);
                            }

                            imagen.SaveAs(pathCarrusel);
                            imagenesCarruselList.Add("~/Content/noticias/carrusel/" + fileNameCarrusel);
                        }
                    }
                    noticia.ImagenesCarrusel = string.Join(",", imagenesCarruselList);
                }

                // Validar y guardar ImagenesCompartir
                if (ImagenesCompartir != null && ImagenesCompartir.ContentLength > 0)
                {
                    var fileNameCompartir = Path.GetFileName(ImagenesCompartir.FileName);
                    var pathCompartir = Path.Combine(Server.MapPath("~/Content/noticias/compartir"), fileNameCompartir);

                    if (System.IO.File.Exists(pathCompartir))
                    {
                        ModelState.AddModelError("ImagenesCompartir", "Ya existe un archivo con el mismo nombre en el servidor.");
                        return View(noticia);
                    }

                    ImagenesCompartir.SaveAs(pathCompartir);
                    noticia.ImagenesCompartir = "~/Content/noticias/compartir/" + fileNameCompartir;
                }

                noticia.FechaPublicacion = noticia.FechaNoticia.Value;
                db.Noticias.Add(noticia);
                db.SaveChanges();

                return RedirectToAction("Dashboard", "Noticias");
            }
            return View(noticia);
        }

        private string GenerateSlug(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            // Convertir a minúsculas
            input = input.ToLowerInvariant();

            // Reemplazar caracteres con acentos por su equivalente sin acento
            input = input
                .Replace("á", "a")
                .Replace("é", "e")
                .Replace("í", "i")
                .Replace("ó", "o")
                .Replace("ú", "u")
                .Replace("ñ", "n");

            // Eliminar caracteres especiales
            input = System.Text.RegularExpressions.Regex.Replace(input, @"[^a-z0-9\s-]", "");

            // Reemplazar espacios en blanco por guiones
            input = System.Text.RegularExpressions.Regex.Replace(input, @"\s+", "-").Trim('-');

            return input;
        }

        // Details - Ver detalles de una noticia
        public ActionResult Details(string slug)
        {
            // Buscar la noticia por el Slug
            var noticia = db.Noticias.FirstOrDefault(n => n.Slug.ToLower() == slug.ToLower());
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

        public ActionResult Edit(string slug)
        {
            // Buscar la noticia en la base de datos usando el Slug
            var noticia = db.Noticias.FirstOrDefault(n => n.Slug.ToLower() == slug.ToLower());
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
        public ActionResult Edit(Noticia noticia, HttpPostedFileBase Imagen, HttpPostedFileBase ImagenResumen, IEnumerable<HttpPostedFileBase> ImagenesCarrusel, HttpPostedFileBase ImagenesCompartir)
        {
            // Validar que el campo Tipo sea obligatorio y válido
            if (noticia.Tipo != 1 && noticia.Tipo != 2)
            {
                ModelState.AddModelError("Tipo", "Debe seleccionar un tipo válido.");
                return View(noticia);
            }

            if (ModelState.IsValid)
            {
                var existingNoticia = db.Noticias.Find(noticia.Id);
                if (existingNoticia == null)
                {
                    return HttpNotFound();
                }

                // Validar que el Slug sea obligatorio
                if (string.IsNullOrWhiteSpace(noticia.Slug))
                {
                    ModelState.AddModelError("Slug", "El campo Slug es obligatorio.");
                    return View(noticia);
                }

                // Normalizar el Slug proporcionado por el usuario
                noticia.Slug = GenerateSlug(noticia.Slug);

                // Validar que el Slug sea único (excluyendo la noticia actual)
                if (db.Noticias.Any(n => n.Slug == noticia.Slug && n.Id != noticia.Id))
                {
                    ModelState.AddModelError("Slug", "El Slug proporcionado ya existe. Por favor, elige uno diferente.");
                    return View(noticia);
                }

                // Actualizar las propiedades de la noticia existente
                existingNoticia.Titulo = noticia.Titulo;
                existingNoticia.Descripcion = noticia.Descripcion;
                existingNoticia.Contenido = noticia.Contenido;
                existingNoticia.Slug = noticia.Slug;
                existingNoticia.Tipo = noticia.Tipo;

                // Actualizar la fecha de publicación si se proporciona
                if (noticia.FechaNoticia.HasValue)
                {
                    existingNoticia.FechaPublicacion = noticia.FechaNoticia.Value;
                }

                // Validar y guardar Imagen
                if (Imagen != null && Imagen.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(Imagen.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/noticias/banner"), fileName);

                    if (System.IO.File.Exists(path))
                    {
                        ModelState.AddModelError("Imagen", "Ya existe un archivo con el mismo nombre en el servidor.");
                        return View(noticia);
                    }

                    Imagen.SaveAs(path);
                    existingNoticia.Imagen = "~/Content/noticias/banner/" + fileName;
                }

                // Validar y guardar ImagenResumen
                if (ImagenResumen != null && ImagenResumen.ContentLength > 0)
                {
                    var fileNameResumen = Path.GetFileName(ImagenResumen.FileName);
                    var pathResumen = Path.Combine(Server.MapPath("~/Content/noticias/resumen"), fileNameResumen);

                    if (System.IO.File.Exists(pathResumen))
                    {
                        ModelState.AddModelError("ImagenResumen", "Ya existe un archivo con el mismo nombre en el servidor.");
                        return View(noticia);
                    }

                    ImagenResumen.SaveAs(pathResumen);
                    existingNoticia.ImagenResumen = "~/Content/noticias/resumen/" + fileNameResumen;
                }

                // Validar y guardar ImagenesCarrusel
                if (ImagenesCarrusel != null && ImagenesCarrusel.Any())
                {
                    var imagenesCarruselList = new List<string>();
                    foreach (var imagen in ImagenesCarrusel)
                    {
                        if (imagen != null && imagen.ContentLength > 0)
                        {
                            var fileNameCarrusel = Path.GetFileName(imagen.FileName);
                            var pathCarrusel = Path.Combine(Server.MapPath("~/Content/noticias/carrusel"), fileNameCarrusel);

                            if (System.IO.File.Exists(pathCarrusel))
                            {
                                ModelState.AddModelError("ImagenesCarrusel", $"Ya existe un archivo con el nombre {fileNameCarrusel} en el servidor.");
                                return View(noticia);
                            }

                            imagen.SaveAs(pathCarrusel);
                            imagenesCarruselList.Add("~/Content/noticias/carrusel/" + fileNameCarrusel);
                        }
                    }
                    existingNoticia.ImagenesCarrusel = string.Join(",", imagenesCarruselList);
                }

                // Validar y guardar ImagenesCompartir
                if (ImagenesCompartir != null && ImagenesCompartir.ContentLength > 0)
                {
                    var fileNameCompartir = Path.GetFileName(ImagenesCompartir.FileName);
                    var pathCompartir = Path.Combine(Server.MapPath("~/Content/noticias/compartir"), fileNameCompartir);

                    if (System.IO.File.Exists(pathCompartir))
                    {
                        ModelState.AddModelError("ImagenesCompartir", "Ya existe un archivo con el mismo nombre en el servidor.");
                        return View(noticia);
                    }

                    ImagenesCompartir.SaveAs(pathCompartir);
                    existingNoticia.ImagenesCompartir = "~/Content/noticias/compartir/" + fileNameCompartir;
                }

                // Establecer la fecha de publicación según la fecha proporcionada por el usuario
                if (noticia.FechaNoticia.HasValue)
                {
                    existingNoticia.FechaPublicacion = noticia.FechaNoticia.Value;
                }

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
