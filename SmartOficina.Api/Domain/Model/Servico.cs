namespace SmartOficina.Api.Domain.Model;

public class Servico : Base
{
    public Servico()
    {
            
    }
    public Guid PrestadorId { get; set; }
    public Prestador Prestador { get; set; }
    public required string Descricao { get; set; }
    public float Valor { get; set; }
    public Guid SubServicoId { get; set; }
    public SubCategoriaServico? SubCategoriaServico { get; set; }
    public Guid PrestacaoServicoId { get; set; }
    public PrestacaoServico? PrestacaoServico { get; set; }

}
