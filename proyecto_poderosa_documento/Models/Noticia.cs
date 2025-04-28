using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace proyecto_poderosa_documento.Models
{
    public class Noticia
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        [Required(ErrorMessage = "El campo Slug es obligatorio.")]
        public string Slug { get; set; } // Nuevo campo para la URL
        public string Descripcion { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public DateTime? FechaNoticia { get; set; } // Campo para la fecha definida por el usuario
        public string Imagen { get; set; } // Nueva propiedad para la imagen
        public string ImagenResumen { get; set; } // Nueva propiedad para la imagen Chica
        public string ImagenesCarrusel { get; set; } // Nueva propiedad para la imagen de carrusel
        public string ImagenesCompartir { get; set; } // Nueva propiedad para la imagen de compartir en las redes sociales

        // Propiedad de relación con el usuario que subió la noticia
        public int UsuarioId { get; set; } // Aquí se almacena el ID del usuario
        public virtual Usuario Usuario { get; set; } // Propiedad de navegación

        public string GetSanitizedTitulo()
        {
            return new string(Titulo.Where(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)).ToArray());
        }
        public string GetPlainContent()
        {
            if (string.IsNullOrEmpty(Contenido)) return string.Empty;
            return Regex.Replace(Contenido, "<.*?>", string.Empty); // Elimina las etiquetas HTML
        }
    }
}