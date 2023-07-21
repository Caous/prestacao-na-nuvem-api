namespace SmartOficina.Api.Dto;

public class PrestacaoServicoDto
{
    public string? Referencia { get; set; }
    public EPrestacaoServicoStatus Status { get; set; }
    public PrestadorDto? Prestador { get; set; }
    public required Guid PrestadorId { get; set; }
    public ClienteDto? Cliente { get; set; }
    public Guid? ClienteId { get; set; }
    public VeiculoDto? Veiculo { get; set; }
    public Guid? VeiculoId { get; set; }
    public Guid? Id { get; set; }
    public ICollection<ServicoDto>? Servicos { get; set; }
}
