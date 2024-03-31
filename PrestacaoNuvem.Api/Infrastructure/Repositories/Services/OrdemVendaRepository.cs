

using Microsoft.EntityFrameworkCore;

namespace PrestacaoNuvem.Api.Infrastructure.Repositories.Services;

public class OrdemVendaRepository : GenericRepository<OrdemVenda>, IOrdemVendaRepository
{
    private readonly OficinaContext _context;
    public OrdemVendaRepository(OficinaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<OrdemVenda> Create(OrdemVenda item)
    {
        await _context.OrdemVenda.AddAsync(item);
        return item;
    }

    public override async Task<ICollection<OrdemVenda>> GetAll(Guid id, OrdemVenda filter)
    {
        var result = await _context.OrdemVenda.
            Where(x=> x.PrestadorId == id)
            .Include(x=> x.Produtos)
            .Include(x=> x.Prestador)
            .Include(x=> x.Cliente).ToListAsync();

        return result;
    }

    public Task ChangeStatus(OrdemVenda ordemVenda, EOrdemVendaStatus status)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<OrdemVenda>> GetByPrestacoesServicosStatus(Guid prestadorId, ICollection<EOrdemVendaStatus> statusOrdemVenda)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<OrdemVenda>> GetByPrestador(Guid prestadorId)
    {
        throw new NotImplementedException();
    }
}
