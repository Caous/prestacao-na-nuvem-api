namespace PrestacaoNuvem.Api.Dto;

public class OrdemVendaDto : Base
{
    public string? Referencia { get; set; }
    public EPrestacaoServicoStatus Status { get; set; }
    public PrestadorDto? Prestador { get; set; }
    public Guid? FuncionarioPrestadorId { get; set; }
    public FuncionarioPrestadorDto? FuncionarioPrestador { get; set; }
    public ICollection<ProdutoDto>? Produtos { get; set; }
    public string? CPF { get; set; }
}
