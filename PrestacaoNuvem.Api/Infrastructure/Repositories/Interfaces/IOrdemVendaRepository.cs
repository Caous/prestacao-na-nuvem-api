namespace PrestacaoNuvem.Api.Infrastructure.Repositories.Interfaces;

public interface IOrdemVendaRepository : IGenericRepository<OrdemVenda>
{
    Task ChangeStatus(OrdemVenda ordemVenda, EOrdemVendaStatus status);
    Task<ICollection<OrdemVenda>> GetByPrestador(Guid prestadorId);
    Task<OrdemVenda> Update(OrdemVenda item);
    Task<ICollection<OrdemVenda>> GetByPrestacoesServicosStatus(Guid prestadorId, ICollection<EOrdemVendaStatus> statusOrdemVenda);
}
