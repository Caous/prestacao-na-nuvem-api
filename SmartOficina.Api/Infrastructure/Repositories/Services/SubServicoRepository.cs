namespace SmartOficina.Api.Infrastructure.Repositories.Services;

public class SubServicoRepository : GenericRepository<SubServico>, ISubServicoRepository
{
    private readonly OficinaContext _context;
    public SubServicoRepository(OficinaContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ICollection<SubServico>> GetAllWithIncludes()
    {
        var result = await _context.SubServico.Include(i => i.Categoria).ToArrayAsync();
        await _context.DisposeAsync();
        return result;
    }
}
