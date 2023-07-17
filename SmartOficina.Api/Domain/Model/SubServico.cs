namespace SmartOficina.Api.Domain.Model;

public class SubServico : Base
{
    public string Titulo { get; set; }
    public string Desc { get; set; }
    public Guid CategoriaId { get; set; }
    public CategoriaServico? Categoria { get; set; }
    public ICollection<Servico>? Servicos { get; set; }
}
