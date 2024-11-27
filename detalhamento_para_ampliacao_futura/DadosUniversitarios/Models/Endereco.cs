using System.ComponentModel.DataAnnotations;

namespace DadosUniversitarios.Models
{
    public class Endereco
    {
        public int  Id { get; set; }
        [Required]
        public string Cep { get; set; }
        [Required]
        public string NomeRua { get; set; } 
        [Required]
        public string Bairro { get; set; }
        [Required]
        public string Cidade { get; set; }
        [Required]
        public string Estado { get; set; }

    }
}
