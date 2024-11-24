namespace DadosUniversitarios.Models
{
    public class Endereco
    {
        public int  Id { get; set; }
        public string Cep { get; set; }
        public string NomeRua { get; set; }        
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

    }
}
