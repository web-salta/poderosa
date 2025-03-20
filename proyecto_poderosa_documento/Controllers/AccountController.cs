using proyecto_poderosa_documento.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace proyecto_poderosa_documento.Controllers
{
    public class AccountController : Controller
    {
        private readonly LoginService _loginService;
        private readonly NoticiasDbContext db;

        public AccountController()
        {
            _loginService = new LoginService();
            db = new NoticiasDbContext();
        }

        // Vista para mostrar el formulario de login
        public ActionResult Login(string returnUrl)
        {
            // Si el usuario intenta acceder a una página, almacenamos la URL de retorno
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // Acción para procesar el login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string NombreUsuario, string contrasena, string returnUrl)
        {
            // Verifica si el usuario es válido con los parámetros correctos
            bool isValidUser = _loginService.ValidarUsuario(NombreUsuario, contrasena);

            if (isValidUser)
            {
                // Verificamos si el usuario existe en la base de datos
                var usuarioDb = db.Usuarios.FirstOrDefault(u => u.NombreUsuario == NombreUsuario);

                if (usuarioDb != null)
                {
                    // Autenticamos al usuario
                    FormsAuthentication.SetAuthCookie(NombreUsuario, false);

                    // Verificar el rol del usuario
                    if (usuarioDb.RolId == 1) // Si el rol es "Admin"
                    {
                        // Redirigir a Noticias/Create si es admin
                        return RedirectToAction("Dashboard", "Noticias");
                    }
                    else
                    {
                        // Redirigir a una página diferente para usuarios con rol distinto
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                // Mostrar mensaje de error si las credenciales son incorrectas
                ViewBag.ErrorMessage = "Usuario o contraseña incorrectos.";
            }

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string NombreUsuario, string contrasena, string contrasena2)
        {
            if (contrasena != contrasena2)
            {
                ViewBag.ErrorMessage = "Las contraseñas no coinciden.";
                return View();
            }

            bool isRegistered = _loginService.RegistrarUsuario(NombreUsuario, contrasena);

            if (isRegistered)
            {
                // Aquí puedes redirigir a otra página si el registro es exitoso
                return RedirectToAction("Login", "Account");
            }
            else
            {
                // Mostrar mensaje de error si el registro falla
                ViewBag.ErrorMessage = "Error al registrar el usuario.";
                return View();
            }
        }
    }
}
