namespace SmartOficina.Api.Dto;

public class VeiculoDto : Base
{
    public required string Placa { get; set; }
    public required string Marca { get; set; }
    public required string Modelo { get; set; }
    public EVeiculoTipo Tipo { get; set; }
    public string? Chassi { get; set; }
}
