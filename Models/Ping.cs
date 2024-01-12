namespace Monitoramento.Models
{
    internal class Ping
    {
        public int min { get; set; } = 999;
        public int max { get; set; }
        public int conexao { get; set; } = 0;
        public string status { get; set; }
        public string addres { get; set; }
    }
}
