namespace SmartOficina.Api.Dto;

public class ProdutoPrestacaoServicoDto : Base
{
    public required string Nome { get; set; }
    public required string Marca { get; set; }
    public string? Modelo { get; set; }
    public DateTime? Data_validade { get; set; }
    public string Garantia { get; set; }
    public required float Valor_Venda { get; set; }
    public required int QtdVenda { get; set; }
    public Guid? IdProdutoEstoque { get; set; }
    public Guid? IdPrestacaoServico { get; set; }
}
