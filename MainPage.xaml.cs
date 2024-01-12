using Monitoramento.Controllers;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x416

namespace Monitoramento
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Username_TextChanged(object sender, TextChangedEventArgs e)
        {
            var username = Username.Text;
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var password = passwordBox.Password;
        }

        private void loguinBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Controllers.loginController login = new Controllers.loginController();
                if (login.login(Username.Text, passwordBox.Password))
                {
                    mainController main = new mainController();
                    main.Notify("Login", "Login efetuado com sucesso!");

                    this.Title.Text = "Monitorando";

                    this.loginPage.Visibility = Visibility.Collapsed;

                    try
                    {
                        Task.Run(() => main.started(Username.Text));
                    }
                    catch (System.Exception)
                    {
                        throw;
                    }
                }
                else
                {
                    mainController main = new mainController();
                    main.Notify("Login", "Login falhou!, verifique suas credenciais e tente novamente");
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        private void termosRb_Checked(object sender, RoutedEventArgs e)
        {
            Models.Usuario termos = new Models.Usuario();
            termos.Termos = true;
        }

        private void termosRb_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}