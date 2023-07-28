namespace SmartOficina.Api.Dto;

public class SubServicoDto : Base
{
    public string Titulo { get; set; }
    public string Desc { get; set; }
    public Guid? CategoriaId { get; set; }
    public CategoriaServicoDto? Categoria { get; set; }
}
