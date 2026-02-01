using System;
using System.Configuration;
using System.Data.SqlClient;

namespace proyecto_poderosa_documento.Models
{
    public class DatabaseHelper
    {
        private string connectionString;

        public DatabaseHelper()
        {
            // Obtener la cadena de conexión desde Web.config
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public SqlConnection GetConnection()
        {
            try
            {
                return new SqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la conexión a la base de datos", ex);
            }
        }
    }
}