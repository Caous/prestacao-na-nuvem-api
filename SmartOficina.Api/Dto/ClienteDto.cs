namespace SmartOficina.Api.Dto;

public class ClienteDto
{
    public required string Nome { get; set; }
    public required string? Telefone { get; set; }
    public required string? Email { get; set; }
    public string? RG { get; set; }
    public required string CPF { get; set; }
    public string? Endereco { get; set; }
    public Guid? Id { get; set; }
    public required Guid PrestadorId { get; set; }
}
