using System;

namespace proyecto_poderosa_documento.Models
{
	public class Noticia
	{
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public string Imagen { get; set; } // Nueva propiedad para la imagen
    }
}