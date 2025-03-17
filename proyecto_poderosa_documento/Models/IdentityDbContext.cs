using System.Data.Entity;

namespace proyecto_poderosa_documento.Models
{
    public class IdentityDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }

        public IdentityDbContext() : base("name=DefaultConnection") { }
    }
}