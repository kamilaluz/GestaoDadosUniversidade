namespace DadosUniversitarios.Models
{
    public class PessoaFisica
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateOnly DataNascimento { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public Endereco Endereco { get; set; }


    }
}
