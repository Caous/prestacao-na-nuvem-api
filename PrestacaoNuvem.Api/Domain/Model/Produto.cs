﻿namespace PrestacaoNuvem.Api.Domain.Model;

public class Produto : Base
{
    public Produto()
    {

    }

    public ETipoMedidaItem TipoMedidaItem { get; set; }
    public required string Nome { get; set; }
    public required string Marca { get; set; }
    public string? Modelo { get; set; }
    public DateTime? Data_validade { get; set; }
    public string? Garantia { get; set; }
    public required float Valor_Compra { get; set; }
    public required float Valor_Venda { get; set; }
    public double? Peso { get; set; }
    public required Guid PrestadorId { get; set; }
    public Prestador Prestador { get; set; }
    public Guid? PrestacaoServicoId{ get; set; }
    public Guid? OrdemVendaId{ get; set; }
    public PrestacaoServico? PrestacaoServico { get; set; }
    public OrdemVenda? OrdemVenda { get; set; }
}
