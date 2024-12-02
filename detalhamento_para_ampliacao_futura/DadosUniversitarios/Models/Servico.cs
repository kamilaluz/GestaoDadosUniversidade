namespace DadosUniversitarios.Models
{
    public class Servico
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<Contrato> Contratos { get; set; }
        public List<Empresa> Empresas { get; set; }
    }
}