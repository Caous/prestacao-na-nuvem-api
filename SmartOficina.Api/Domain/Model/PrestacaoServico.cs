namespace SmartOficina.Api.Domain.Model;

public class PrestacaoServico : Base
{
    public PrestacaoServico()
    {
        
    }
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


}
