namespace SmartOficina.Api.Dto;

public class SubCategoriaServicoDto : Base
{
    public required string Titulo { get; set; }
    public required string Desc { get; set; }
    public double ValorServico { get; set; } //Opicional colocar 0 padrão
    public Guid? CategoriaId { get; set; }
    public CategoriaServicoDto? Categoria { get; set; }
}
