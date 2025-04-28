using System;
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

        // Método para validar usuario
        public bool ValidarUsuario(string nombreUsuario, string contrasena)
        {
            if (string.IsNullOrEmpty(nombreUsuario))
            {
                throw new ArgumentException("El nombre de usuario no puede ser nulo o vacío", nameof(nombreUsuario));
            }

            try
            {
                using (SqlConnection connection = _databaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT * FROM Usuarios WHERE NombreUsuario = @NombreUsuario AND Contrasena = @Contrasena";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
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


        public bool RegistrarUsuario(string NombreUsuario, string contrasena)
        {
            try
            {
                using (SqlConnection connection = _databaseHelper.GetConnection())
                {
                    connection.Open();

                    // El rol por defecto puede ser "Usuario" (con RolId = 2)
                    int rolId = 2;  // Asegúrate de que este valor coincida con tu esquema de roles

                    // Consulta para insertar el nuevo usuario
                    string query = "INSERT INTO Usuarios (NombreUsuario, Contrasena, RolId) VALUES (@NombreUsuario, @Contrasena, @RolId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Asignar los parámetros a la consulta SQL
                        command.Parameters.AddWithValue("@NombreUsuario", NombreUsuario);  // Cambiado a NombreUsuario
                        command.Parameters.AddWithValue("@Contrasena", contrasena); // Asegúrate de encriptar la contraseña antes de insertarla
                        command.Parameters.AddWithValue("@RolId", rolId); // Asignar el RolId

                        // Ejecutar la consulta
                        int result = command.ExecuteNonQuery();

                        // Si el resultado es mayor que 0, la inserción fue exitosa
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Lanza una excepción en caso de error
                throw new Exception("Error al registrar el usuario", ex);
            }
        }
    }
}