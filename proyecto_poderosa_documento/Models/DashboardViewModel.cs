using System.Collections.Generic;

namespace proyecto_poderosa_documento.Models
{
    public class DashboardViewModel
    {
        public IEnumerable<Noticia> Noticias { get; set; }
        public IEnumerable<PopUp> PopUps { get; set; }
        public string PopUpImageUrl { get; set; }
    }
}