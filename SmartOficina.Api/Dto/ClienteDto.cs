namespace SmartOficina.Api.Dto;

public class ClienteDto : Base
{
    public ClienteDto()
    {

    }
    public required string Nome { get; set; }
    public required string? Telefone { get; set; }
    public required string? Email { get; set; }
    public string? Rg { get; set; }
    public required string CPF { get; set; }
    public string? Endereco { get; set; }
}
