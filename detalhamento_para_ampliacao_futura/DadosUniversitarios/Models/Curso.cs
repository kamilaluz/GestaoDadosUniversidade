using System.ComponentModel.DataAnnotations;

namespace DadosUniversitarios.Models
{
    public class Curso
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public List<Professor> Professores { get; set; }
        public List<Aluno> Alunos { get; set; }
        public List<Disciplina> Disciplinas { get; set; }
    }
}
