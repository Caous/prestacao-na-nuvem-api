namespace SmartOficina.Api.Domain;

public class CategoriaServico : Base
{
    public string Titulo { get; set; }
    public string Desc { get; set; }
    public ICollection<SubServico>? SubServicos { get; set; }
}
