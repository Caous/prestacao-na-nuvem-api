namespace SmartOficina.Api.Domain.Model;

public class CategoriaServico : Base
{
    public CategoriaServico()
    {
        
    }
    public required string Titulo { get; set; }
    public required string Desc { get; set; }
    public required Guid PrestadorId { get; set; }
    public Prestador Prestador { get; set; }
    public ICollection<SubCategoriaServico>? SubCategoriaServicos { get; set; }

    internal bool Any()
    {
        throw new NotImplementedException();
    }
}
