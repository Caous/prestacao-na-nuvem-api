namespace SmartOficina.Api.Domain.Model;

public class CategoriaServico : Base
{
    //ToDo: Colocar required
    public string Titulo { get; set; }
    //ToDo: Colocar required
    public string Desc { get; set; }
    public ICollection<SubServico>? SubServicos { get; set; }
}
