namespace SmartOficina.Api.Infrastructure.Repositories.Services;

public class SubServicoRepository : GenericRepository<SubCategoriaServico>, ISubServicoRepository
{
    private readonly OficinaContext _context;
    public SubServicoRepository(OficinaContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ICollection<SubCategoriaServico>> GetAllWithIncludes()
    {
        var result = await _context.SubCategoriaServico.Include(i => i.Categoria).ToArrayAsync();
        await _context.DisposeAsync();
        return result;
    }
}
