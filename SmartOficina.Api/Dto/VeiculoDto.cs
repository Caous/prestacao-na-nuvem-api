namespace SmartOficina.Api.Dto;

public class VeiculoDto : Base
{
    public required string Placa { get; set; } //Obri Character 8
    public required string Marca { get; set; } //Obri Character 80
    public required string Modelo { get; set; } //Obri Character 150
    public int Ano { get; set; } //obri
    public EVeiculoTipo Tipo { get; set; }// Obri
    public int Km { get; set; } //Obri
    public string? Chassi { get; set; }
    public string TipoCombustivel { get; set; } //hraracter 25
}
