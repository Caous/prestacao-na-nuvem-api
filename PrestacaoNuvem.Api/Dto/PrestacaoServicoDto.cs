﻿namespace PrestacaoNuvem.Api.Dto;

public class PrestacaoServicoDto : Base
{

    public string? Referencia { get; set; }
    public EPrestacaoServicoStatus Status { get; set; }
    public PrestadorDto? Prestador { get; set; }
    public Guid? FuncionarioPrestadorId { get; set; }
    public FuncionarioPrestadorDto? FuncionarioPrestador { get; set; }
    public ClienteDto? Cliente { get; set; }
    public Guid? ClienteId { get; set; }
    public DateTime? DataConclusaoServico { get; set; }
    public VeiculoDto? Veiculo { get; set; }
    public Guid? VeiculoId { get; set; }
    public ICollection<ServicoDto>? Servicos { get; set; }
    public ICollection<ProdutoDto>? Produtos { get; set; }
    public float? PrecoOrdem { get; set; }
    public float? PrecoDescontado { get; set; }
    public double? DescontoPercentual { get; set; }
    public EFormaPagamento FormaPagamento { get; set; }
    public DateTime? DataPagamento { get; set; }

}
