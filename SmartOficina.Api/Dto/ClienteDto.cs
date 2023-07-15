namespace SmartOficina.Api.Dto;

public class ClienteDto
{
    public required string Nome { get; set; }
    public string? Telefone { get; set; }
    public string? Email { get; set; }
}
