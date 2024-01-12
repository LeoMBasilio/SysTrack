using Microsoft.Toolkit.Uwp.Notifications;
using MySqlConnector;
using System;
using System.Diagnostics;

namespace Monitoramento.Controllers
{
    internal class mainController
    {
        internal void Notify(string context, string msg)
        {
            ToastContentBuilder builder = new ToastContentBuilder()
                .AddText(context)
                .AddText(msg);
        }

        internal void started(string user)
        {
            threadsController poolThread = new threadsController();
            poolThread.pool(user);
        }

        internal int TipoConexao()
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd.exe", "/c netsh wlan show interfaces");
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.UseShellExecute = false;
            processStartInfo.CreateNoWindow = true;
            Process process = new Process();
            process.StartInfo = processStartInfo;
            process.Start();
            process?.WaitForExit();

            var output = process.StandardOutput.ReadToEnd().Split(":");

            //Tratar retorno para valida conexao
            if (output[12].Contains("Conectado") || output[12].Contains("Connected"))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        internal int Entrada(string user)
        {
            DateTime now = DateTime.Now;

            using (var cn = new MySqlConnection(new Models.Conn().ToString()))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("select `novo horario` from tb_base_ip where " + user + "; ", cn);
                cmd.ExecuteReader();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DateTime entrada = reader.GetDateTime(0);
                    if (entrada.Hour == now.Hour && entrada.Minute == now.Minute)
                    {
                        return 1;
                    }
                }
            }
            return 0;
        }

    }
}