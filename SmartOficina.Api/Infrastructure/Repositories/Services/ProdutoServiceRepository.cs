namespace SmartOficina.Api.Infrastructure.Repositories.Services;
public class ProdutoRepository : GenericRepository<Produto>, IProdutoRepository
{
    public ProdutoRepository(OficinaContext context) : base(context)
    {
    }
}
