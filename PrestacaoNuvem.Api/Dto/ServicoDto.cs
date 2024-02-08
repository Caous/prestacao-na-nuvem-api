namespace PrestacaoNuvem.Api.Dto;

public class ServicoDto : Base
{
    public required string Descricao { get; set; } //obgd
    public float Valor { get; set; }
    public Guid SubServicoId { get; set; }
    public SubCategoriaServicoDto? SubCategoriaServico { get; set; }
    public PrestacaoServicoDto? PrestacaoServico { get; set; }
}
