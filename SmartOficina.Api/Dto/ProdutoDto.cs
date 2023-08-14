namespace SmartOficina.Api.Dto;

public class ProdutoDto : Base
{
    public required string Nome { get; set; }
    public required string Marca { get; set; }
    public ETipoMedidaItemDto TipoMedidaItem { get; set; }
    public string? Modelo { get; set; }
    public DateTime? Data_validade { get; set; }
    public string? Garantia { get; set; }
    public required float Valor_Compra { get; set; }
    public required float Valor_Venda { get; set; }
    public int Qtd { get; set; }
    public Guid? PrestadorId { get; set; }
    public Guid? PrestacaoServicoId { get; set; }
    public PrestacaoServicoDto? PrestacaoServico { get; set; }
}
