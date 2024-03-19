

namespace PrestacaoNuvem.Api.Infrastructure.Repositories.Services;

public class OrdemVendaRepository : GenericRepository<OrdemVenda>, IOrdemVendaRepository
{
    public OrdemVendaRepository(OficinaContext context) : base(context)
    {
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
