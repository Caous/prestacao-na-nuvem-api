namespace SmartOficina.Api.Infrastructure.Repositories;

public interface IPrestacaoServicoRepository : IGenericRepository<PrestacaoServico>
{
    Task<PrestacaoServico> Add(PrestacaoServico prestacaoServico);
    Task ChangeStatus(Guid id, PrestacaoServicoStatus status);
    Task<ICollection<PrestacaoServico>> GetByPrestador(Guid prestadorId);
}
