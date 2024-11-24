using System.ComponentModel.DataAnnotations;

namespace DadosUniversitarios.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
        [Required]
        public string ConfirmacaoSenha { get; set; }
    }
}
