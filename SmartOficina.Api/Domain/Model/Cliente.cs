namespace SmartOficina.Api.Domain.Model;

public class Cliente : Base
{
    public required string Nome { get; set; }
    public string? Telefone { get; set; }
    public string? Email { get; set; }
    public string RG { get; set; }
    public string CPF { get; set; }
    public string Endereco { get; set; }
    public ICollection<PrestacaoServico>? Servicos { get; set; }

}