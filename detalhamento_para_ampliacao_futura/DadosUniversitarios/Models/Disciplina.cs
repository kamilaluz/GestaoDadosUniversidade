using System.ComponentModel.DataAnnotations;

namespace DadosUniversitarios.Models
{
    public class Disciplina
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public List<Pessoa> Pessoas { get; set; }
        public List<Curso> Cursos { get; set; }
    }
}
