using System.Windows;
using System.Windows.Media;
using MySql.Data.MySqlClient;

namespace Catalogo
{
    public partial class Login : Window
    {
        // Práctica 3: se elimina la matriz en memoria de la Práctica 2.
        // Las credenciales se validan contra la tabla Usuarios de MySQL (DatabaseHelper).

        private const string MSG_ERROR_CONEXION =
            "No se ha podido conectar con la base de datos.\n" +
            "Compruebe que el servicio de MySQL está iniciado e inténtelo de nuevo.";

        public Login()
        {
            InitializeComponent();
        }

        private bool CamposRellenos(out string usuario, out string contrasena)
        {
            usuario = txtUsuario.Text.Trim();
            contrasena = txtPassword.Password;

            // TC 01: campos vacíos -> mensaje por ventana y NO se consulta la BD
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasena))
            {
                MostrarMensaje("Introduzca datos", Brushes.OrangeRed);
                MessageBox.Show("Introduzca datos", "Campos vacíos",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!CamposRellenos(out string usuario, out string contrasena)) return;

            try
            {
                // TC 03 / TC 04 / TC 07 / TC 09: consulta parametrizada
                if (DatabaseHelper.ValidarUsuario(usuario, contrasena))
                {
                    MostrarMensaje("Inicio de sesión exitoso", Brushes.LightGreen);
                    MessageBox.Show($"Bienvenido, {usuario}", "Inicio de sesión exitoso",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    Home ventanaHome = new Home(usuario);
                    ventanaHome.Show();
                    this.Close();
                }
                else
                {
                    MostrarMensaje("Usuario o contraseña incorrectos", Brushes.IndianRed);
                    MessageBox.Show("Usuario o contraseña incorrectos", "Error de inicio de sesión",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (MySqlException)
            {
                // TC 08: MySQL no disponible -> mensaje claro, la aplicación sigue abierta
                MostrarErrorConexion();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error inesperado", Brushes.Red);
                MessageBox.Show("Se ha producido un error inesperado:\n" + ex.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnNuevoUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (!CamposRellenos(out string usuario, out string contrasena)) return;

            try
            {
                // TC 06: aviso de duplicado
                if (DatabaseHelper.ExisteUsuario(usuario) ||
                    !DatabaseHelper.InsertarUsuario(usuario, contrasena))
                {
                    MostrarMensaje("El usuario ya existe", Brushes.OrangeRed);
                    MessageBox.Show("El usuario ya existe", "Alta de usuario",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // TC 05: alta correcta
                MostrarMensaje($"Usuario '{usuario}' dado de alta correctamente", Brushes.LightGreen);
                MessageBox.Show($"Usuario '{usuario}' dado de alta correctamente", "Alta de usuario",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                txtPassword.Clear();
            }
            catch (MySqlException)
            {
                MostrarErrorConexion();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error inesperado", Brushes.Red);
                MessageBox.Show("Se ha producido un error inesperado:\n" + ex.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // TC 02: Salir de la aplicación
        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MostrarErrorConexion()
        {
            MostrarMensaje("Error de conexión con la base de datos", Brushes.Red);
            MessageBox.Show(MSG_ERROR_CONEXION, "Error de conexión",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void MostrarMensaje(string texto, Brush color)
        {
            lblMensaje.Text = texto;
            lblMensaje.Foreground = color;
        }
    }
}
