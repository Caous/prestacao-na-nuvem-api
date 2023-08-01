namespace SmartOficina.Api.Dto;

public class ServicoDto : Base
{
    public required string Descricao { get; set; }
    public float Valor { get; set; }
    public Guid SubServicoId { get; set; }
    public SubCategoriaServicoDto? SubServico { get; set; }
    public PrestacaoServicoDto? PrestacaoServico { get; set; }
}
