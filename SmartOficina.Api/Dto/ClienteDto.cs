namespace SmartOficina.Api.Dto;

public class ClienteDto : Base
{
    public ClienteDto()
    {

    }
    public required string Nome { get; set; } //Obri Character 125
    public required string Telefone { get; set; } //Obri Character 14 
    public required string Email { get; set; } //Obri Character 250
    public string? Rg { get; set; } //Op Character 9
    public required string CPF { get; set; } //Obri Character 11
    public string? Endereco { get; set; } //Obri Character 250
}
