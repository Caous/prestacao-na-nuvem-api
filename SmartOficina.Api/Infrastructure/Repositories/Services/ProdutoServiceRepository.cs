namespace SmartOficina.Api.Infrastructure.Repositories.Services;
public class ProdutoRepository : GenericRepository<Produto>, IProdutoRepository
{
    private readonly OficinaContext _context;
    public ProdutoRepository(OficinaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<ICollection<Produto>> GetAll(Guid id)
    {
        var result = await _context.Produto.Where(x => x.PrestacaoServicoId == null && x.PrestadorId == id).ToArrayAsync();
        await _context.DisposeAsync();
        return result;
    }
}
