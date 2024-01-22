namespace SmartOficina.Api.Infrastructure.Repositories.Services;

public class CategoriaServicoRepository : GenericRepository<CategoriaServico>, ICategoriaServicoRepository
{
    private readonly OficinaContext _context;
    public CategoriaServicoRepository(OficinaContext context) : base(context)
    {
        _context = context;
    }

    public async override Task<ICollection<CategoriaServico>> GetAll(Guid id, CategoriaServico filter)
    {
        var result = await _context.CategoriaServico.Where(x => x.PrestadorId == id && x.DataDesativacao == null).ToArrayAsync();
        await _context.DisposeAsync();

        if (filter != null && result.Any())
        {
            if (!filter.Titulo.IsMissing())
                result = result.Where(x => x.Titulo == filter.Titulo).ToArray();
            if (!filter.Desc.IsMissing())
                result = result.Where(x => x.Desc == filter.Desc).ToArray();
        }

        return result;
    }
}
