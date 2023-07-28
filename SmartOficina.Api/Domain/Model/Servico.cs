namespace SmartOficina.Api.Domain.Model;

public class Servico : Base
{
    public Servico()
    {
            
    }
    public Guid? PrestadorId { get; set; }
    public required string Nome { get; set; }
    public float Valor { get; set; }
    public Guid SubServicoId { get; set; }
    public SubServico? SubServico { get; set; }
    public Guid PrestacaoServicoId { get; set; }
    public PrestacaoServico? PrestacaoServico { get; set; }

}
