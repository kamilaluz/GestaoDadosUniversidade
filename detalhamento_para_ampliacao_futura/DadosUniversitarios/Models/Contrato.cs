using System.ComponentModel.DataAnnotations;

namespace DadosUniversitarios.Models
{
    public class Contrato
    {
        public int Id { get; set; }
        [Required]
        public int NumeroContrato { get; set; }
        [Required]
        public int PeriodicidadeId { get; set; }
        public Periodicidade Periodicidade { get; set; }
        [Required]        
        public double ValorServico { get; set; }
        public int DataPagamento { get; set; }
        public DateOnly VencimentoContrato { get; set; }
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
        public int ServicoId { get; set; }
        public Servico Servico { get; set; }

    }
}
