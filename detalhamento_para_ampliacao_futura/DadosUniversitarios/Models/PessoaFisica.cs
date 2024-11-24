using System.ComponentModel.DataAnnotations;

namespace DadosUniversitarios.Models
{
    public class PessoaFisica
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        [DataType("DateOnly")]
        public DateOnly DataNascimento { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Telefone { get; set; }
        public Endereco Endereco { get; set; }


    }
}
