using System.ComponentModel.DataAnnotations;

namespace Monitoramento.Models
{
    internal class Usuario
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool Termos { get; set; }
    }
}
