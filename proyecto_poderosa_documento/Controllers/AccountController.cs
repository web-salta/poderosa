using proyecto_poderosa_documento.Models;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace proyecto_poderosa_documento.Controllers
{
    public class AccountController : Controller
    {
        private readonly LoginService _loginService;
        private readonly NoticiasDbContext db;

        public ActionResult CaptchaImage()
        {
            string captchaText = GenerateRandomText(5);
            Session["CaptchaCode"] = captchaText;

            using (Bitmap bitmap = new Bitmap(300, 40)) // Tamaño de la imagen
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.White);
                Random random = new Random();

                // Calcular el espacio horizontal entre caracteres
                int charSpacing = bitmap.Width / captchaText.Length;

                for (int i = 0; i < captchaText.Length; i++)
                {
                    Font font = new Font("Arial", 20, FontStyle.Bold);
                    Brush brush = new SolidBrush(Color.Black);

                    // Calcular posición horizontal y centrar verticalmente
                    float x = charSpacing * i + (charSpacing - graphics.MeasureString(captchaText[i].ToString(), font).Width) / 2;
                    float y = (bitmap.Height - graphics.MeasureString(captchaText[i].ToString(), font).Height) / 2;

                    // Aplicar rotación aleatoria
                    graphics.TranslateTransform(x, y);
                    graphics.RotateTransform(random.Next(-30, 30)); // Rotar entre -30 y 30 grados
                    graphics.DrawString(captchaText[i].ToString(), font, brush, 0, 0);
                    graphics.ResetTransform(); // Restablecer la transformación
                }

                // Agregar ruido
                AddNoise(graphics, bitmap);

                using (MemoryStream stream = new MemoryStream())
                {
                    bitmap.Save(stream, ImageFormat.Png);
                    return File(stream.ToArray(), "image/png");
                }
            }
        }

        private string GenerateRandomText(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            char[] result = new char[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = chars[random.Next(chars.Length)];
            }
            return new string(result);
        }

        private Bitmap ApplyWaveEffect(Bitmap bitmap)
        {
            Bitmap distorted = new Bitmap(bitmap.Width, bitmap.Height);
            Random random = new Random();
            double waveFrequency = random.Next(2, 5); // Frecuencia de la onda

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    int newX = (int)(x + 5 * Math.Sin(2 * Math.PI * y / waveFrequency));
                    int newY = y;

                    if (newX >= 0 && newX < bitmap.Width)
                    {
                        distorted.SetPixel(newX, newY, bitmap.GetPixel(x, y));
                    }
                }
            }

            return distorted;
        }

        private void AddNoise(Graphics graphics, Bitmap bitmap)
        {
            Random random = new Random();
            Pen pen = new Pen(Color.Gray);

            // Dibujar líneas aleatorias
            for (int i = 0; i < 10; i++)
            {
                int x1 = random.Next(bitmap.Width);
                int y1 = random.Next(bitmap.Height);
                int x2 = random.Next(bitmap.Width);
                int y2 = random.Next(bitmap.Height);
                graphics.DrawLine(pen, x1, y1, x2, y2);
            }
        }

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
        public ActionResult Login(string NombreUsuario, string contrasena, string returnUrl, string CaptchaCode)
        {
            // Validar el captcha
            if (Session["CaptchaCode"] == null || CaptchaCode != Session["CaptchaCode"].ToString())
            {
                ModelState.AddModelError("CaptchaCode", "El código Captcha ingresado no es válido.");
                return View();
            }
            // Verifica si el usuario es válido con los parámetros correctos
            bool isValidUser = _loginService.ValidarUsuario(NombreUsuario, contrasena);

            if (isValidUser)
            {
                var usuarioDb = db.Usuarios.FirstOrDefault(u => u.NombreUsuario == NombreUsuario);

                if (usuarioDb != null)
                {
                    FormsAuthentication.SetAuthCookie(NombreUsuario, false);

                    // Verificar si el returnUrl es válido
                    if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                    {
                        returnUrl = Url.Action("Index", "Home"); // Redirige a la página de inicio si returnUrl es inválido o vacío.
                    }

                    // Verificar el rol del usuario
                    if (usuarioDb.RolId == 1)
                    {
                        return RedirectToAction("Dashboard", "Noticias");
                    }
                    else
                    {
                        return Redirect(returnUrl); // Redirigir a la URL de retorno válida
                    }
                }
            }
            else
            {
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            // Cerrar sesión
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}
