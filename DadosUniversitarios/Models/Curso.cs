namespace DadosUniversitarios.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<Professor> Professores { get; set; }
        public List<Aluno> Alunos { get; set; }
        public List<Disciplina> Disciplinas { get; set; }
    }
}
