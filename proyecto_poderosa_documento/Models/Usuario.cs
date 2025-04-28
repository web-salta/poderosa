namespace proyecto_poderosa_documento.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        // Cambié 'Usuario' por 'NombreUsuario' para reflejar la columna correcta en la base de datos
        public string NombreUsuario { get; set; }

        public string Contrasena { get; set; }

        // Renombré 'RolId' para que coincida con la estructura de la base de datos
        public int RolId { get; set; }

        // Propiedad de navegación para el Rol
        public virtual Rol Rol { get; set; }
    }
}