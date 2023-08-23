namespace SmartOficina.Api.Infrastructure.Repositories.Services;

public class SubServicoRepository : GenericRepository<SubCategoriaServico>, ISubServicoRepository
{
    private readonly OficinaContext _context;
    public SubServicoRepository(OficinaContext context) : base(context)
    {
        _context = context;
    }
    public async override Task<ICollection<SubCategoriaServico>> GetAll(Guid id)
    {
        var result = await _context.SubCategoriaServico.Include(i => i.Categoria).Where(x => x.Categoria.PrestadorId == id).ToArrayAsync();
        await _context.DisposeAsync();
        return result;
    }
    
}
