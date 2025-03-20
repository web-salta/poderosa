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
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Noticia>()
                .HasRequired(n => n.Usuario) // La noticia debe tener un usuario
                .WithMany() // Un usuario puede tener muchas noticias
                .HasForeignKey(n => n.UsuarioId); // Relación con la clave foránea UsuarioId
        }
    }
}