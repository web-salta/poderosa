using System.Collections.Generic;

namespace proyecto_poderosa_documento.Models
{
    public class Rol
    {
        public int Id { get; set; }  // Id del rol
        public string Nombre { get; set; }  // Nombre del rol (por ejemplo, "Admin", "User")

        // Relación inversa con los usuarios
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}