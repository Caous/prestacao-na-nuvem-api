namespace PrestacaoNuvem.Api.Domain.Model;

public class OrdemVenda : Base
{
    public string? CPF { get; set; }
    public string? Referencia { get; set; }
    public EOrdemVendaStatus Status { get; set; }
    public Prestador? Prestador { get; set; }
    public Guid? FuncionarioPrestadorId { get; set; }
    public FuncionarioPrestador? FuncionarioPrestador { get; set; }
    public required Guid PrestadorId { get; set; }
    public Cliente? Cliente { get; set; }
    public Guid? ClienteId { get; set; }
    public ICollection<Produto>? Produtos { get; set; }
}
