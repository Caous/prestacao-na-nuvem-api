namespace SmartOficina.Api.Infrastructure.Repositories.Services;

public class CategoriaServicoRepository : GenericRepository<CategoriaServico>, ICategoriaServicoRepository
{
    private readonly OficinaContext _context;
    public CategoriaServicoRepository(OficinaContext context) : base(context)
    {
        _context = context;
    }

    public async override Task<ICollection<CategoriaServico>> GetAll(Guid id)
    {
        var result = await _context.CategoriaServico.Where(x => x.PrestadorId == id).ToArrayAsync();
        await _context.DisposeAsync();
        return result;
    }
}
