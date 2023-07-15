namespace SmartOficina.Api.Domain
{
    public class Servico
    {
        public Guid Id { get; set; }
        public required string Nome { get; set; }
        public float Valor { get; set; }

        public Guid PrestacaoServicoId { get; set; }
        public required PrestacaoServico PrestacaoServico { get; set; }
    }
}
