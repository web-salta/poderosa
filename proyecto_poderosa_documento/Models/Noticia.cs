using System;

namespace proyecto_poderosa_documento.Models
{
    public class Noticia
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public DateTime? FechaNoticia { get; set; } // Campo para la fecha definida por el usuario
        public string Imagen { get; set; } // Nueva propiedad para la imagen
        public string ImagenResumen { get; set; } // Nueva propiedad para la imagen

        // Propiedad de relación con el usuario que subió la noticia
        public int UsuarioId { get; set; } // Aquí se almacena el ID del usuario
        public virtual Usuario Usuario { get; set; } // Propiedad de navegación
    }
}