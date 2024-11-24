using System.ComponentModel.DataAnnotations;

namespace DadosUniversitarios.Models
{
    public class Professor : PessoaFisica
    {
        public int Id { get; set; }
        [Required]
        public int NumeroMatricula { get; set; }
    }
}
