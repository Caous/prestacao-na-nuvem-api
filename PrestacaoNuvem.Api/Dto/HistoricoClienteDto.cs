namespace PrestacaoNuvem.Api.Dto
{
    public class HistoricoClienteDto : Base
    {
        public string? Assunto { get; set; }

        public string? Descricao { get; set; }

        public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;

        // Relação com Cliente
        public Guid? ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
    }
}
