using System.ComponentModel.DataAnnotations;

namespace DadosUniversitarios.Models
{
    public class Curso
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public List<Pessoa> Pessoas { get; set; }
        public List<Disciplina> Disciplinas { get; set; }
    }
}
