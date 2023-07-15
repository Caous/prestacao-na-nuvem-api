namespace SmartOficina.Api.Domain
{
    public class PrestacaoServico : Base
    {
        public PrestacaoServicoStatus Status { get; set; }
        public required Prestador Prestador { get; set; }
        public required Guid PrestadorId { get; set; }

        public required Cliente Cliente { get; set; }
        public required Guid ClienteId { get; set; }

        public ICollection<Servico>? Servicos { get; set; }

    }
}
