namespace PrestacaoNuvem.Api.Infrastructure.Repositories.Services;

public class SubServicoRepository : GenericRepository<SubCategoriaServico>, ISubServicoRepository
{
    private readonly OficinaContext _context;
    public SubServicoRepository(OficinaContext context) : base(context)
    {
        _context = context;
    }
    public async override Task<ICollection<SubCategoriaServico>> GetAll(Guid id, SubCategoriaServico filter)
    {
        var result = await _context.SubCategoriaServico.Include(i => i.Categoria).Where(x => x.Categoria.PrestadorId == id && x.DataDesativacao == null).ToArrayAsync();
        await _context.DisposeAsync();

        if (filter != null && result.Any())
        {
            if (!filter.Desc.IsMissing())
                result = result.Where(x => x.Desc == filter.Desc).ToArray();
            if (!filter.Titulo.IsMissing())
                result = result.Where(x => x.Titulo == filter.Titulo).ToArray();
        }

        return result;
    }
    
}
