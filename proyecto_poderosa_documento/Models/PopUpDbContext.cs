using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace proyecto_poderosa_documento.Models
{
	public class PopUpDbContext : DbContext
	{
        public PopUpDbContext() : base("name=DefaultConnection") { }

        // DbSet para PopUps
        public DbSet<PopUp> PopUp { get; set; }
    }
}