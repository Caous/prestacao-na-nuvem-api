namespace PrestacaoNuvem.Api.Domain.Model;

public class PrestacaoServico : Base
{
    public PrestacaoServico()
    {

    }

    #region Tabela Principal

    public float? PrecoOrdem { get; set; }
    public float? PrecoDescontado { get; set; }
    public double? DescontoPercentual { get; set; }
    public EFormaPagamento FormaPagamento { get; set; }
    public DateTime? DataPagamento { get; set; }

    #endregion

    #region Tabelas relacionadas

    public string? Referencia { get; set; }
    public EPrestacaoServicoStatus Status { get; set; }
    public Prestador? Prestador { get; set; }
    public Guid? FuncionarioPrestadorId { get; set; }
    public FuncionarioPrestador? FuncionarioPrestador { get; set; }
    public required Guid PrestadorId { get; set; }
    public Cliente? Cliente { get; set; }
    public Guid? ClienteId { get; set; }
    public Veiculo? Veiculo { get; set; }
    public Guid? VeiculoId { get; set; }
    public DateTime? DataConclusaoServico { get; set; }
    public ICollection<Servico>? Servicos { get; set; }
    public ICollection<Produto>? Produtos { get; set; }

    #endregion

}
