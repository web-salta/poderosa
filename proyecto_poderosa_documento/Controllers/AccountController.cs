using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyecto_poderosa_documento.Models;

namespace proyecto_poderosa_documento.Controllers
{
    public class AccountController : Controller
    {
        private readonly LoginService _loginService;
        public AccountController()
        {
            _loginService = new LoginService();
        }

        // Vista para mostrar el formulario de login
        public ActionResult Login()
        {
            return View();
        }

        // Acción para procesar el login
        [HttpPost]
        public ActionResult Login(string usuario, string contrasena)
        {
            bool isValidUser = _loginService.ValidarUsuario(usuario, contrasena);

            if (isValidUser)
            {
                // Aquí puedes redirigir a otra página si el login es exitoso
                return RedirectToAction("Create", "Noticias");
            }
            else
            {
                // Mostrar mensaje de error si las credenciales son incorrectas
                ViewBag.ErrorMessage = "Usuario o contraseña incorrectos.";
                return View();
            }
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(string usuario, string contrasena, string contrasena2)
        {
            if (contrasena != contrasena2)
            {
                ViewBag.ErrorMessage = "Las contraseñas no coinciden.";
                return View();
            }

            bool isRegistered = _loginService.RegistrarUsuario(usuario, contrasena);

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