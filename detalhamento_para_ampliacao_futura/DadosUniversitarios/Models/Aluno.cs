using System.ComponentModel.DataAnnotations;

namespace DadosUniversitarios.Models
{
    public class Aluno : PessoaFisica
    {
        public int Id { get; set; }
        [Required]
        public int NumeroMatricula { get; set; }
        public List<Disciplina> Disciplinas { get; set; }

    }
}
