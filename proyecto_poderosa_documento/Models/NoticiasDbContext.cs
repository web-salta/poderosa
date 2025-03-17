using System.Data.Entity;

namespace proyecto_poderosa_documento.Models
{
    public class NoticiasDbContext : DbContext
    {
        // DbSet para Noticias
        public DbSet<Noticia> Noticias { get; set; }
        // DbSet para Usuarios
        public DbSet<Usuario> Usuarios { get; set; }
        // DbSet para Roles (opcional)
        public DbSet<Rol> Roles { get; set; }
        public NoticiasDbContext() : base("name=DefaultConnection") { }
    }
}