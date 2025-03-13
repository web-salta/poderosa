using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace proyecto_poderosa_documento.Models
{
	public class LoginService
	{
        private readonly DatabaseHelper _databaseHelper;

        public LoginService()
        {
            _databaseHelper = new DatabaseHelper();
        }
        public bool ValidarUsuario(string usuario, string contrasena)
        {
            try
            {
                using (SqlConnection connection = _databaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = "SELECT * FROM usuarios WHERE Usuario = @Usuario AND Contrasena = @Contrasena";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Usuario", usuario);
                        command.Parameters.AddWithValue("@Contrasena", contrasena);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            return reader.HasRows;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al realizar la consulta", ex);
            }
        }

        public bool RegistrarUsuario(string usuario, string contrasena)
        {
            try
            {
                using (SqlConnection connection = _databaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = "INSERT INTO usuarios (Usuario, Contrasena) VALUES (@Usuario, @Contrasena)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Usuario", usuario);
                        command.Parameters.AddWithValue("@Contrasena", contrasena);

                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar el usuario", ex);
            }
        }
    }
}