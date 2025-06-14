namespace PrestacaoNuvem.Api.Dto;

public class FuncionarioPrestadorDto : Base
{
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? RG { get; set; }
    public string CPF { get; set; } = string.Empty;
    public string? Endereco { get; set; }
    public string? Cargo { get; set; }
    public Guid FilialId { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? EmailUser { get; set; }
    public string? Role { get; set; }
    public bool CreateUser { get; set; } = false;

}
