namespace DadosUniversitarios.Models
{
    public class Fornecedor : Empresa
    {
        public int Id { get; set; }
        public int NumeroContrato { get; set; }
        public int Periodicidade { get; set; }
        public double ValorServico { get; set; }
        public DateOnly DataPagamento { get; set; }
        public DateOnly VencimentoContrato { get; set; }

    }
}
