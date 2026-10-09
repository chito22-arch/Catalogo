using System.Windows;

namespace Catalogo
{
    public partial class Home : Window
    {
        // Constructor por defecto
        public Home()
        {
            InitializeComponent();
        }

        // Constructor sobrecargado que recibe el nombre de usuario
        public Home(string nombreUsuario) : this()
        {
            if (!string.IsNullOrEmpty(nombreUsuario))
            {
                lblUsuarioPerfil.Text = nombreUsuario;
            }
        }

        // Evento para cerrar la ventana actual y reabrir el Login
        private void BtnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new Login();
            loginWindow.Show();

            this.Close();
        }
    }
}