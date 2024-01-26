namespace SmartOficina.Api.Infrastructure.Repositories.Services;

public class DashboardRepository : IDashboardRepository
{
    private readonly OficinaContext _context;

    public DashboardRepository(OficinaContext context)
    {
        _context = context;
    }

    public async Task<ICollection<PrestacaoServico>> GetDailySales(Guid prestador)
    {
        var result = _context.PrestacaoServico.Where(x => x.PrestadorId == prestador && x.Status == EPrestacaoServicoStatus.Concluido)
           .Include(i => i.Servicos).ToList();

        await _context.DisposeAsync();

        return result;
    }

    public async Task<ICollection<Cliente>> GetNewCustomerMonth(Guid prestador)
    {
        var result = _context.Cliente.Where(x => x.PrestadorId == prestador && x.DataCadastro.Month == DateTime.Now.Month)
           .ToList();

        await _context.DisposeAsync();
        return result;
    }

    public async Task<ICollection<PrestacaoServico>> GetOSMonth(Guid prestador)
    {
        var result = _context.PrestacaoServico
            .Where(x => x.PrestadorId == prestador && x.Status == EPrestacaoServicoStatus.Concluido).ToList();

        await _context.DisposeAsync();

        return result;
    }

    public async Task<ICollection<PrestacaoServico>> GetSalesMonth(Guid prestador)
    {
        var result = _context.PrestacaoServico
            .Where(x => x.PrestadorId == prestador && x.Status == EPrestacaoServicoStatus.Concluido)
                .Include(i => i.Servicos)
                .Include(i => i.Produtos).ToList();

        await _context.DisposeAsync();

        return result;
    }

    public async Task<ICollection<Produto>> GetSalesProductMonth(Guid prestador)
    {
        var result = _context.Produto.Where(x => x.PrestadorId == prestador && x.DataCadastro.Month == DateTime.Now.Month).ToList();

        await _context.DisposeAsync();

        return result;
    }

    public async Task<ICollection<PrestacaoServico>?> GetServicesGroupByCategoryService(Guid prestador)
    {
        var result = _context.PrestacaoServico
            .Where(x => x.PrestadorId == prestador && x.Status == EPrestacaoServicoStatus.Concluido)
            .Include(i => i.Servicos)
                .ThenInclude(i => i.SubCategoriaServico)
                .ThenInclude(i => i.Categoria).ToList();

        await _context.DisposeAsync();

        return result;
    }

    public async Task<ICollection<PrestacaoServico>> GetServicesGroupBySubCategoryService(Guid prestador)
    {
        var result = _context.PrestacaoServico
            .Where(x => x.PrestadorId == prestador && x.Status == EPrestacaoServicoStatus.Concluido)
            .Include(i => i.Servicos)
                .ThenInclude(i => i.SubCategoriaServico).ToList();

        await _context.DisposeAsync();

        return result;
    }
}
