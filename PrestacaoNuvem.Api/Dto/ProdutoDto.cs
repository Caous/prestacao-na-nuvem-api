using PrestacaoNuvem.Api.Enumerations;

namespace PrestacaoNuvem.Api.Dto;

public class ProdutoDto : Base
{
    public required string Nome { get; set; } //Obri Character 150
    public required string Marca { get; set; } //Obri Character 150
    public ETipoMedidaItemDto TipoMedidaItem { get; set; }//Obri
    public string? Modelo { get; set; } //Opicional Character 200
    public DateTime? Data_validade { get; set; } //Opi
    public string? Garantia { get; set; } //Opi Character 200
    public required float Valor_Compra { get; set; } //Obri
    public required float Valor_Venda { get; set; } //Obri
    public int Qtd { get; set; } //Obri
    public double Peso { get; set; }
    public Guid? PrestadorId { get; set; }
    public Guid? PrestacaoServicoId { get; set; }
    public PrestacaoServicoDto? PrestacaoServico { get; set; }
}
