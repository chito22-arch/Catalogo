using System.Windows;
using System.Windows.Media;

namespace Catalogo
{
    public partial class Login : Window
    {
        // REQUISITO TÉCNICO PDF: Simulación de usuarios en memoria (string[,])
        private readonly string[,] usuariosRegistrados = new string[,]
        {
            { "admin", "1234" },
            { "jugador1", "pass2024" },
            { "profesor", "dam123" }
        };

        public Login()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtPassword.Password;

            // 1. Verificación de campos vacíos
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasena))
            {
                MostrarMensaje("Debe completar todos los campos.", Brushes.IndianRed);
                return;
            }

            // 2. Validación de credenciales recorriendo la matriz string[,]
            bool autenticado = false;
            for (int i = 0; i < usuariosRegistrados.GetLength(0); i++)
            {
                if (usuariosRegistrados[i, 0] == usuario && usuariosRegistrados[i, 1] == contrasena)
                {
                    autenticado = true;
                    break;
                }
            }

            if (autenticado)
            {
                MostrarMensaje("¡Acceso concedido! Redirigiendo...", Brushes.LightGreen);

                // Abrir pantalla principal pasando el usuario autenticado
                Home ventanaHome = new Home(usuario);
                ventanaHome.Show();

                this.Close();
            }
            else
            {
                MostrarMensaje("Usuario o contraseña incorrectos.", Brushes.IndianRed);
            }
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            // Cierra toda la aplicación
            Application.Current.Shutdown();
        }

        private void MostrarMensaje(string texto, Brush color)
        {
            lblMensaje.Text = texto;
            lblMensaje.Foreground = color;
        }
    }
}