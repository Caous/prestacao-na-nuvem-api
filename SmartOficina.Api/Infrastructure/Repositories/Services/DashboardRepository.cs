namespace SmartOficina.Api.Infrastructure.Repositories.Services;

public class DashboardRepository : IDashboardRepository
{
    private readonly OficinaContext _context;
    public DashboardRepository(OficinaContext context)
    {
        _context = context;
    }

    public async Task<List<IGrouping<IEnumerable<IGrouping<CategoriaServico?, Servico>>, PrestacaoServico>>> GetServicosGroupByCategoriaServico(Guid prestador)
    {
        var result = _context.PrestacaoServico.Where(x => x.PrestadorId == prestador)
            .Include(i => i.Servicos)
                .ThenInclude(i => i.SubCategoriaServico)
                .ThenInclude(i => i.Categoria).ToList();

        await _context.DisposeAsync();

        return result.GroupBy(x => x.Servicos.GroupBy(y => y.SubCategoriaServico.Categoria)).ToList(); ;
    }
}
