using SmartOficina.Api.Domain;

namespace SmartOficina.Api.Infrastructure.Repositories
{
    public interface IPrestacaoServicoRepository
    {
        Task<PrestacaoServico> Add(PrestacaoServico prestacaoServico);
        Task ChangeStatus(Guid id, PrestacaoServicoStatus status);
        Task<ICollection<PrestacaoServico>> GetByPrestador(Guid prestadorId);
    }
}
