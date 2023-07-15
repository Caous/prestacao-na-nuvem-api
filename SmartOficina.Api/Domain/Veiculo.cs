namespace SmartOficina.Api.Domain
{
    public class Veiculo : Base 
    {
        public string? Placa { get; set; }

        public required string Marca { get; set; }

        public required string Modelo { get; set; }

        public VeiculoTipo Tipo { get; set; }

        public ICollection<PrestacaoServico>? Servicos { get; set; }
    }
}
