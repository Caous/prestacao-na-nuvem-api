namespace SmartOficina.Api.Infrastructure.Repositories;

public interface IPrestacaoServicoRepository : IGenericRepository<PrestacaoServico>
{
    Task ChangeStatus(Guid id, PrestacaoServicoStatus status);
}
