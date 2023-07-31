namespace SmartOficina.Api.Domain.Model;

public class ProdutoPrestacaoServico : Base
{
    public ProdutoPrestacaoServico()
    {
            
    }

    public required string Nome { get; set; }
    public required string Marca { get; set; }
    public string? Modelo { get; set; }
    public DateTime? Data_validade { get; set; }
    public string Garantia { get; set; }
    public required float Valor_Venda { get; set; }
    public required int QtdVenda { get; set; }
    public Guid? IdProdutoEstoque { get; set; }
    public Guid PrestacaoServicoId { get; set; }
    public PrestacaoServico? PrestacaoServico { get; set; }

}
