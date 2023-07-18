namespace SmartOficina.Api.Infrastructure.Repositories.Interfaces;

public interface IPrestacaoServicoRepository : IGenericRepository<PrestacaoServico>
{
    Task ChangeStatus(Guid id, EPrestacaoServicoStatus status);
    Task<ICollection<PrestacaoServico>> GetByPrestador(Guid prestadorId);
}
