namespace SmartOficina.Api.Domain;

public class Cliente : Base
{
    public required string Nome { get; set; }
    public string? Telefone { get; set; }
    public string? Email { get; set; }

    public ICollection<PrestacaoServico>? Servicos { get; set; }

}