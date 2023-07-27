namespace SmartOficina.Api.Dto;
public class PrestadorDto
{
    public Guid? Id { get; set; }
    public required string Nome { get; set; }
    public string CPF { get; set; }
    public string CNPJ { get; set; }
    public string Razao_Social { get; set; }
    public string? Nome_Fantasia { get; set; }
    public string? Representante { get; set; }
    public string? Telefone { get; set; }
    public string? Email { get; set; }
    public string Endereco { get; set; }
}
