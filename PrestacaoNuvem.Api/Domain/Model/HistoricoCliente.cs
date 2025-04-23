namespace PrestacaoNuvem.Api.Domain.Model
{
    public class HistoricoCliente : Base
    {
        public string? Assunto { get; set; }

        public required string Descricao { get; set; }

        public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;

        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; }
    }
}
