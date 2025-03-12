namespace Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ProyectoContext : DbContext
    {
        public ProyectoContext()
            : base("name=ProyectoContext")
        {
        }

        public virtual DbSet<anos> anos { get; set; }
        public virtual DbSet<categoria> categoria { get; set; }
        public virtual DbSet<documento> documento { get; set; }
        public virtual DbSet<usuario> usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<anos>()
                .HasMany(e => e.documento)
                .WithOptional(e => e.anos)
                .HasForeignKey(e => e.anoId);

            modelBuilder.Entity<categoria>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<documento>()
                .Property(e => e.titulo_archivo)
                .IsUnicode(false);

            modelBuilder.Entity<documento>()
                .Property(e => e.nombre_archivo)
                .IsUnicode(false);

            modelBuilder.Entity<documento>()
                .Property(e => e.descripcion_archivo)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.nom_usu)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.clave)
                .IsUnicode(false);
        }
    }
}
