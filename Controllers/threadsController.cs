using System.Diagnostics;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Monitoramento.Controllers
{
    internal class threadsController
    {
        internal void pool(string user)
        {
            Models.Ping objPing = new Models.Ping();
            Models.Desempenho objDesempenho = new Models.Desempenho();

            try
            {
                Task rede = Task.Run(() =>
                {
                    Ping ping = new Ping();
                    string host = "google.com";
                    string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                    byte[] buffer = Encoding.ASCII.GetBytes(data);
                    int timeout = 120;
                    PingOptions options = new PingOptions(114, true);

                    PingReply reply = ping.Send(host, timeout, buffer, options);

                    if (reply.Status == IPStatus.Success)
                    {
                        objPing.status = reply.Status.ToString();
                        objPing.addres = reply.Address.ToString();
                        if (reply != null && (int)reply.RoundtripTime < objPing.min)
                        {
                            objPing.min = (int)reply.RoundtripTime;
                        }
                        else if (reply != null && (int)reply.RoundtripTime > objPing.max)
                        {
                            objPing.max = (int)reply.RoundtripTime;
                        }

                    }
                });

                Task Desempenho = Task.Run(() =>
                {

                    ManagementObjectSearcher search = new ManagementObjectSearcher("root\\CIMV2", "SELECT LoadPercentage FROM WIN32_Processor");
                    foreach (var item in search.Get())
                    {
                        // Console.WriteLine("CPU Load: {0}%", item["LoadPercentage"]);
                        objDesempenho.CPU = (int)item["LoadPercentage"];
                    }


                    PerformanceCounter ram = new PerformanceCounter("memory", "available mbytes");
                    objDesempenho.RAM = (int)(100 - (100 * (decimal)ram.NextValue() / (decimal)ram.NextValue()));

                    PerformanceCounter disk = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
                    objDesempenho.disk = (int)disk.NextValue();
                });

                Task.WaitAll(rede, Desempenho);
                Controllers.dataController dados = new Controllers.dataController();
                Task save = Task.Run(() => dados.Save(objPing, objDesempenho, user));
                Thread.Sleep(360000);
                Task.WaitAll(save);
                pool(user);

            }
            catch (System.Exception)
            {
                throw;
            }

        }
    }
}
