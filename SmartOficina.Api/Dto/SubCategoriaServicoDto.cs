namespace SmartOficina.Api.Dto;

public class SubCategoriaServicoDto : Base
{
    public string Titulo { get; set; }
    public string Desc { get; set; }
    public Guid? CategoriaId { get; set; }
    public CategoriaServicoDto? Categoria { get; set; }
}
