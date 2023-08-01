namespace SmartOficina.Api.Dto; 

public class FuncionarioPrestadorDto : Base
{
    public required string Nome { get; set; }
    public required string Telefone { get; set; }
    public required string Email { get; set; }
    public required string RG { get; set; }
    public required string CPF { get; set; }
    public required string Endereco { get; set; }
    public required string Cargo { get; set; }
}
