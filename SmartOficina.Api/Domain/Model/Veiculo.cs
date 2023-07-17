namespace SmartOficina.Api.Domain.Model;

public class Veiculo : Base
{
    public required string Placa { get; set; }

    public required string Marca { get; set; }

    public required string Modelo { get; set; }

    public EVeiculoTipo Tipo { get; set; }

    public ICollection<PrestacaoServico>? Servicos { get; set; }
}
