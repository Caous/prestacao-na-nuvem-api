namespace SmartOficina.Api.Dto;

public class PrestacaoServicoDto
{
    public PrestadorDto? Prestador { get; set; }
    public required Guid PrestadorId { get; set; }
    public ClienteDto? Cliente { get; set; }
    public required Guid ClienteId { get; set; }
    public VeiculoDto? Veiculo { get; set; }
    public required Guid VeiculoId { get; set; }
}
