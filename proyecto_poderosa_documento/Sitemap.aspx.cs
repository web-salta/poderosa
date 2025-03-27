using System;
using System.Text;
using System.Xml;

namespace proyecto_poderosa_documento
{
    public partial class Sitemap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Establecer el tipo de contenido a XML
            Response.ContentType = "application/xml";

            // Crear un objeto XmlWriter para generar el sitemap
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true; // Para que el XML esté bien formateado
            settings.NewLineOnAttributes = true; // Salto de línea después de cada atributo
            StringBuilder sb = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                writer.WriteStartDocument();

                // Declarar solo una vez el espacio de nombres en el <urlset>
                writer.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

                // Aquí agregamos las URLs de manera dinámica
                // URL de la página principal
                WriteUrl(writer, "https://www.poderosa.com.pe/", DateTime.Now.ToString("yyyy-MM-dd"), "daily", "1.0");

                // Puedes agregar más URLs dinámicamente
                WriteUrl(writer, "https://www.poderosa.com.pe/quienes-somos/poderosa", "2025-03-19", "monthly", "0.7");
                WriteUrl(writer, "https://www.poderosa.com.pe/quienes-somos/directorio-y-gerencia", "2025-03-19", "monthly", "0.7");
                WriteUrl(writer, "https://www.poderosa.com.pe/quienes-somos/accionistas", "2025-03-19", "monthly", "0.7");

                // Agregar más URLs aquí según lo necesites...

                // Cerrar el <urlset> después de agregar todas las URLs
                writer.WriteEndElement(); // urlset

                writer.WriteEndDocument();
            }

            Response.Write(sb.ToString());
        }

        private void WriteUrl(XmlWriter writer, string loc, string lastmod, string changefreq, string priority)
        {
            writer.WriteStartElement("url");
            writer.WriteElementString("loc", loc);
            writer.WriteElementString("lastmod", lastmod);
            writer.WriteElementString("changefreq", changefreq);
            writer.WriteElementString("priority", priority);
            writer.WriteEndElement(); // url
        }
    }
}
