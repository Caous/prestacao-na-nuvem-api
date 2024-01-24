namespace SmartOficina.Api.Infrastructure.Repositories.Interfaces;

public interface IDashboardRepository
{
    Task<List<IGrouping<IEnumerable<IGrouping<CategoriaServico?, Servico>>, PrestacaoServico>>> GetServicosGroupByCategoriaServico(Guid prestador);
}
