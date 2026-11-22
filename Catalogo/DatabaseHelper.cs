using MySql.Data.MySqlClient;

namespace Catalogo
{
    /// <summary>
    /// Centraliza la cadena de conexión y la apertura/cierre de la conexión con MySQL.
    /// </summary>
    public static class DatabaseHelper
    {
        // Usuario de MySQL creado en database.sql con permisos SELECT e INSERT sobre Usuarios
        private const string CadenaConexion =
            "Server=localhost;Port=3306;Database=catalogo_db;" +
            "Uid=catalogo_app;Pwd=Catalogo2026!;" +
            "Connection Timeout=5;SslMode=Preferred;";

        /// <summary>Crea y abre una conexión. Lanza MySqlException si el servidor no responde.</summary>
        public static MySqlConnection AbrirConexion()
        {
            var conexion = new MySqlConnection(CadenaConexion);
            conexion.Open();
            return conexion;
        }

        /// <summary>Cierra y libera la conexión si está abierta.</summary>
        public static void CerrarConexion(MySqlConnection? conexion)
        {
            if (conexion == null) return;
            if (conexion.State != System.Data.ConnectionState.Closed)
                conexion.Close();
            conexion.Dispose();
        }

        /// <summary>Comprueba con una consulta parametrizada si existen el usuario y la contraseña.</summary>
        public static bool ValidarUsuario(string usuario, string password)
        {
            MySqlConnection? conexion = null;
            try
            {
                conexion = AbrirConexion();
                const string sql =
                    "SELECT COUNT(*) FROM Usuarios " +
                    "WHERE BINARY nombre_usuario = @usuario AND BINARY password = @password";
                using var cmd = new MySqlCommand(sql, conexion);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@password", password);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
            finally
            {
                CerrarConexion(conexion);
            }
        }

        /// <summary>Indica si ya existe un usuario con ese nombre.</summary>
        public static bool ExisteUsuario(string usuario)
        {
            MySqlConnection? conexion = null;
            try
            {
                conexion = AbrirConexion();
                const string sql = "SELECT COUNT(*) FROM Usuarios WHERE nombre_usuario = @usuario";
                using var cmd = new MySqlCommand(sql, conexion);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
            finally
            {
                CerrarConexion(conexion);
            }
        }

        /// <summary>Inserta un usuario nuevo. Devuelve false si ya existía (UNIQUE).</summary>
        public static bool InsertarUsuario(string usuario, string password)
        {
            MySqlConnection? conexion = null;
            try
            {
                conexion = AbrirConexion();
                const string sql =
                    "INSERT INTO Usuarios (nombre_usuario, password) VALUES (@usuario, @password)";
                using var cmd = new MySqlCommand(sql, conexion);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@password", password);
                return cmd.ExecuteNonQuery() == 1;
            }
            catch (MySqlException ex) when (ex.Number == 1062) // Duplicate entry
            {
                return false;
            }
            finally
            {
                CerrarConexion(conexion);
            }
        }
    }
}
