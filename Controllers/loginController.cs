namespace Monitoramento.Controllers
{
    internal class loginController
    {
        private Models.Usuario usuario = new Models.Usuario();

        internal bool login(string username, string password)
        {
            if (usuario.Username == "admin" && usuario.Password == "admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
