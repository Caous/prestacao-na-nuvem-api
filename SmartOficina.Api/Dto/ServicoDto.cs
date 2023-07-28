namespace SmartOficina.Api.Dto;

public class ServicoDto : Base
{
    public required string Nome { get; set; }
    public float Valor { get; set; }
    public Guid SubServicoId { get; set; }
    public SubServicoDto? SubServico { get; set; }
    public PrestacaoServicoDto? PrestacaoServico { get; set; }
}
