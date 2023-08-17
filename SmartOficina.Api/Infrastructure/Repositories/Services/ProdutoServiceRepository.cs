namespace SmartOficina.Api.Infrastructure.Repositories.Services;
public class ProdutoRepository : GenericRepository<Produto>, IProdutoRepository
{
    private readonly OficinaContext _context;
    public ProdutoRepository(OficinaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<ICollection<Produto>> GetAll()
    {
        return _context.Produto.Where(x => x.PrestacaoServicoId == null).ToList();
    }
}
