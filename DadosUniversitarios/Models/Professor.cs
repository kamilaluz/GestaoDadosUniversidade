namespace DadosUniversitarios.Models
{
    public class Professor : PessoaFisica
    {
        public int Id { get; set; }
        public int NumeroMatricula { get; set; }
        public List<Curso> NomeCurso { get; set; }
        public List<Disciplina> Disciplinas { get; set; }
    }
}
