namespace SmartOficina.Api.Domain;

public class Prestador : Base
{
    public required string Nome { get; set; }

    public ICollection<PrestacaoServico>? Servicos { get; set; }
}
