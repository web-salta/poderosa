using proyecto_poderosa_documento.Models;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyecto_poderosa_documento.Controllers
{
    public class PopUpController : Controller
    {
        private NoticiasDbContext db = new NoticiasDbContext(); // Add this line to define the db context

        // Mostrar el formulario de creación de PopUp
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // Acción para guardar el nuevo PopUp
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(PopUp popUp, HttpPostedFileBase Imagen, HttpPostedFileBase Documento)
        {
            // Verificar si la fecha fue proporcionada por el usuario
            if (popUp.FechaCreacion == default(DateTime))
            {
                // Si no se proporciona una fecha, asignar la fecha y hora actual del servidor
                popUp.FechaCreacion = DateTime.Now;
            }

            if (ModelState.IsValid)
            {
                // Obtener el nombre de usuario desde el sistema de autenticación
                var usuarioNombre = User.Identity.Name;

                // Obtener el UsuarioId desde la base de datos con el nombre de usuario
                var usuario = db.Usuarios.FirstOrDefault(u => u.NombreUsuario == usuarioNombre);

                if (usuario != null)
                {
                    // Asignar el UsuarioId al PopUp
                    popUp.UsuarioId = usuario.Id;
                }
                else
                {
                    // Si no se encuentra el usuario, redirigir al login
                    return RedirectToAction("Login", "Account");
                }

                // Verificar si se ha subido una imagen
                if (Imagen != null && Imagen.ContentLength > 0)
                {
                    // Generar un nombre único para la imagen
                    var fileName = Path.GetFileName(Imagen.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/popups/imagen"), fileName);

                    // Guardar la imagen en el servidor
                    Imagen.SaveAs(path);

                    // Asignar la ruta de la imagen al modelo
                    popUp.Imagen = "~/Content/popups/imagen/" + fileName;
                }

                if (Documento != null && Documento.ContentLength > 0)
                {
                    // Validar que el archivo sea un PDF
                    var extension = System.IO.Path.GetExtension(Documento.FileName);
                    if (extension.ToLower() != ".pdf")
                    {
                        ModelState.AddModelError("Documento", "Solo se permiten archivos PDF.");
                        return View(popUp);
                    }

                    // Generar el nombre único para el archivo
                    var fileName = Guid.NewGuid().ToString() + extension;

                    // Ruta donde se guardará el archivo
                    var path = Server.MapPath("~/Content/popups/documento/" + fileName);

                    // Guardar el archivo
                    Documento.SaveAs(path);

                    // Guardar la ruta en el modelo
                    popUp.Documento = "~/Content/popups/documento/" + fileName;

                    // Bloquear el campo Url estableciéndolo como vacío
                    popUp.Url = ""; // Asignar una cadena vacía
                }

                // Asignar la fecha proporcionada por el usuario
                popUp.FechaCreacion = popUp.FechaCreacion;

                // Establecer IsActive en true
                popUp.IsActive = true;

                // Guardar el PopUp en la base de datos
                db.PopUp.Add(popUp);
                db.SaveChanges();

                // Redirigir a la vista donde se muestran los PopUps
                return RedirectToAction("Dashboard", "Noticias"); // Puedes redirigir a otro método que liste los popups
            }

            return View(popUp); // Si la validación falla, vuelve al formulario
        }

        // Acción para listar los PopUps
        public ActionResult Index()
        {
            // Filtrar los popups que están activos
            var popUpsActivos = db.PopUp.Where(p => p.IsActive).OrderByDescending(p => p.FechaCreacion).ToList();
            return View(popUpsActivos);
        }
    }
}
