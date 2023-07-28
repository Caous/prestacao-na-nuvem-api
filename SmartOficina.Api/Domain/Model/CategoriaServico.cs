namespace SmartOficina.Api.Domain.Model;

public class CategoriaServico : Base
{
    public CategoriaServico()
    {
        
    }
    public required string Titulo { get; set; }
    public required string Desc { get; set; }
    public ICollection<SubServico>? SubServicos { get; set; }
}
