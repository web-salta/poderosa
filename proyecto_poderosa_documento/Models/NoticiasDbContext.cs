using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace proyecto_poderosa_documento.Models
{
	public class NoticiasDbContext : DbContext
    {
        public DbSet<Noticia> Noticias { get; set; }
        public NoticiasDbContext() : base("name=DefaultConnection") { }
    }
}