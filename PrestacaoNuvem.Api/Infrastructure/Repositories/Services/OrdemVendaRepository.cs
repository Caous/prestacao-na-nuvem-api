namespace PrestacaoNuvem.Api.Infrastructure.Repositories.Services;

public class OrdemVendaRepository : GenericRepository<OrdemVenda>, IOrdemVendaRepository
{
    private readonly OficinaContext _context;
    public OrdemVendaRepository(OficinaContext context) : base(context)
    {
        _context = context;
    }

    public async Task<OrdemVenda> FindById(Guid id)
    {

        var result = await _context.OrdemVenda
               .Where(f => f.Id == id)
               .Include(i => i.Prestador)
               .Include(i => i.Cliente)
               .Include(i => i.Produtos)
               .FirstOrDefaultAsync();
        return result;
    }

    public override async Task<OrdemVenda> Create(OrdemVenda item)
    {
        await _context.OrdemVenda.AddAsync(item);
        return item;
    }

    public override async Task<ICollection<OrdemVenda>> GetAll(Guid id, OrdemVenda filter, bool admin)
    {
        ICollection<OrdemVenda> result = [];
        if (admin)
            result = await _context.OrdemVenda.
             Include(x => x.Produtos)
            .Include(x => x.Prestador)
            .Include(x => x.Cliente).ToListAsync();
        else
            result = await _context.OrdemVenda.
            Where(x => x.PrestadorId == id)
            .Include(x => x.Produtos)
            .Include(x => x.Prestador)
            .Include(x => x.Cliente).ToListAsync();

        return result;
    }

    public Task ChangeStatus(OrdemVenda ordemVenda, EOrdemVendaStatus status)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<OrdemVenda>> GetByPrestacoesServicosStatus(Guid prestadorId, ICollection<EOrdemVendaStatus> statusOrdemVenda)
    {
        var result = await _context.OrdemVenda.
             Where(x => x.PrestadorId == prestadorId && statusOrdemVenda.Contains(x.Status))
             .Include(x => x.Produtos)
             .Include(x => x.Prestador)
             .Include(x => x.Cliente).ToListAsync();

        return result;
    }

    public Task<ICollection<OrdemVenda>> GetByPrestador(Guid prestadorId)
    {
        throw new NotImplementedException();
    }
}
