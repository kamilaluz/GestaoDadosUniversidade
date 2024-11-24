using System.ComponentModel.DataAnnotations;

namespace DadosUniversitarios.Models
{
    public class Disciplina
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public Professor Professor { get; set; }
        public List<Aluno> Alunos { get; set; }
    }
}
