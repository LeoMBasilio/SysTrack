using MySqlConnector;
using System;

namespace Monitoramento.Controllers
{
    internal class dataController
    {
        internal void Save(object ping, object desempenho, string user)
        {
            try
            {
                using (var cn = new MySqlConnection(new Models.Conn().ToString()))
                {
                    Controllers.mainController main = new Controllers.mainController();

                    cn.Open();
                    using (var cmd = new MySqlCommand())
                    {
                        cmd.Connection = cn;
                        cmd.CommandText = "INSERT INTO `monitoramento` (`ping`, `desempenho`) VALUES (@ping, @desempenho);";
                        cmd.Parameters.AddWithValue("@ping", ping);
                        cmd.Parameters.AddWithValue("@desempenho", desempenho);
                        cmd.Parameters.AddWithValue("@data", DateTime.Now);
                        cmd.Parameters.AddWithValue("@tipo_conexao", main.TipoConexao());
                        cmd.Parameters.AddWithValue("@entrada", main.Entrada(user));
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
