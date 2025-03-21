using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyecto_poderosa_documento.Models
{
    [Table("PopUp")]
    public class PopUp
    {
        public int Id { get; set; }  // Identificador único
        public string NombrePopUp { get; set; }  // Nombre del PopUp
        public string Url { get; set; }  // URL del PopUp
        public int UsuarioId { get; set; }  // ID del usuario que creó el PopUp
        public DateTime FechaCreacion { get; set; }  // Fecha de creación
        public string Imagen { get; set; }  // Ruta de la imagen (si aplica)
        public bool IsActive { get; set; }

        // Propiedad de navegación para el Usuario
        public virtual Usuario Usuario { get; set; }
    }
}