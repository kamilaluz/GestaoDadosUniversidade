using System.ComponentModel.DataAnnotations;

namespace DadosUniversitarios.Models
{
    public class Empresa
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string CNPJ { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string Telefone { get; set; }
        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; }
        public int Numero { get; set; }
        public string NomeServico { get; set; }
        public List<Contrato> Contratos { get; set; }


    }
}
