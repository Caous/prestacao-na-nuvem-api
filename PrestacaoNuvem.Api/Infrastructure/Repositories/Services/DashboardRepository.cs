using PrestacaoNuvem.Api.Domain.Model.Dashboard;
using static PrestacaoNuvem.Api.Domain.Model.Dashboards;

namespace PrestacaoNuvem.Api.Infrastructure.Repositories.Services;

public class DashboardRepository : IDashboardRepository
{
    private readonly OficinaContext _context;

    public DashboardRepository(OficinaContext context)
    {
        _context = context;
    }

    public async Task<ICollection<DashboardReceitaDiaria>> GetDailySales(Guid prestador)
    {
        var result = _context.PrestacaoServico.Where(x => x.PrestadorId == prestador && x.Status == EPrestacaoServicoStatus.Concluido)
           .Include(i => i.Servicos).ToList();

        if (result != null && result.Any())
        {
            var resultDaily = MapperOrdemServicoDashBoardDailySale(result);
            return MapperReceita(resultDaily);
        }
        else
        {
            var resultOV = _context.OrdemVenda
                  .Where(x => x.PrestadorId == prestador && x.Status == EOrdemVendaStatus.Concluido)
                        .Include(i => i.Produtos).ToList();

            var resultDaily = MapperOrdemVendaDashBoardDailySale(resultOV);
            return MapperReceita(resultDaily);
        }
    }

    private static ICollection<DashboardReceitaDiaria> MapperReceita(ICollection<FaturamentoDiario> resultDaily)
    {
        ICollection<DashboardReceitaDiaria> result = [];
        foreach (var item in resultDaily)
        {
            result.Add(new DashboardReceitaDiaria() { DateRef = item.DateRef, Valor = item.Valor });
        }
        return result;
    }

    private ICollection<FaturamentoDiario> MapperOrdemVendaDashBoardDailySale(List<OrdemVenda> resultOV)
    {
        resultOV.ForEach(resultOV =>
        {
            resultOV.DataCadastro = resultOV.DataCadastro.Date;
        });
        var resultDaily = resultOV.GroupBy(x => x.DataCadastro).OrderBy(x => x.Key).ToList();

        List<FaturamentoDiario> sales = new();

        foreach (var item in resultDaily)
        {
            if (item != null && item.Key != null)
            {
                sales.Add(new FaturamentoDiario()
                {
                    DateRef = item.Key.ToString("dd/MM/yyyy"),
                    Valor = (double)item.Sum(x => x.Produtos.Sum(y => y.Valor_Venda))
                });
            }
        }

        return sales;
    }

    private ICollection<FaturamentoDiario> MapperOrdemServicoDashBoardDailySale(List<PrestacaoServico> result)
    {
        var resultDaily = result.GroupBy(x => x.DataConclusaoServico).OrderBy(x => x.Key).ToList();

        List<FaturamentoDiario> sales = new();

        foreach (var item in resultDaily)
        {
            if (item != null && item.Key != null)
            {
                sales.Add(new FaturamentoDiario()
                {
                    DateRef = item.Key.Value.ToString("dd/MM/yyyy"),
                    Valor = (double)item.Sum(x => x.Servicos.Sum(y => y.Valor))
                });
            }
        }

        return sales;
    }

    public async Task<ICollection<Cliente>> GetNewCustomerMonth(Guid prestador)
    {
        var result = _context.Cliente.Where(x => x.PrestadorId == prestador && x.DataCadastro.Month == DateTime.Now.Month)
           .ToList();

        await _context.DisposeAsync();
        return result;
    }

    public async Task<DashboardOSMes> GetOSOVMonth(Guid prestador)
    {
        DashboardOSMes dashboard = new DashboardOSMes();

        var result = _context.PrestacaoServico
            .Where(x => x.PrestadorId == prestador && x.Status == EPrestacaoServicoStatus.Concluido && x.DataConclusaoServico.HasValue ? x.DataConclusaoServico.Value.Month == DateTime.Now.Month : x.DataConclusaoServico != null).ToList();

        if (result != null && result.Any())
        {
            dashboard = MapperOrdemServicoDashBoardOsMes(result);
        }
        else
        {
            var resultOV = _context.OrdemVenda
                  .Where(x => x.PrestadorId == prestador && x.Status == EOrdemVendaStatus.Concluido && x.DataCadastro.Month == DateTime.Now.Month).ToList();

            dashboard = MapperOrdemVendaDashBoardOsMes(resultOV);
        }
        await _context.DisposeAsync();

        return dashboard;
    }

    private DashboardOSMes MapperOrdemVendaDashBoardOsMes(List<OrdemVenda> resultOV)
    {
        return new DashboardOSMes() { Valor = resultOV.Count };
    }

    private DashboardOSMes MapperOrdemServicoDashBoardOsMes(List<PrestacaoServico> result)
    {
        return new DashboardOSMes() { Valor = result.Count };
    }

    public async Task<DashboardReceitaMes> GetSalesMonth(Guid prestador)
    {
        DashboardReceitaMes profit = new DashboardReceitaMes();

        var result = _context.PrestacaoServico
            .Where(x => x.PrestadorId == prestador && x.Status == EPrestacaoServicoStatus.Concluido && x.DataConclusaoServico.HasValue ? x.DataConclusaoServico.Value.Month == DateTime.Now.Month : x.DataConclusaoServico != null)
                .Include(i => i.Servicos)
                .Include(i => i.Produtos).ToList();

        if (result != null && result.Any())
        {
            profit = MapperSalerMonthService(result);
        }
        else
        {
            var resultOrdemVenda = _context.OrdemVenda.
            Where(x => x.PrestadorId == prestador && x.Status == EOrdemVendaStatus.Concluido && x.DataCadastro.Month == DateTime.Now.Month)
                .Include(i => i.Produtos).ToList();

            if (resultOrdemVenda != null && resultOrdemVenda.Any())
                profit = MapperSalerMonthProduct(resultOrdemVenda);
        }

        await _context.DisposeAsync();

        return profit;
    }
    private DashboardReceitaMes MapperSalerMonthService(ICollection<PrestacaoServico> result)
    {
        var valorService = result.Sum(x => x.Servicos.Sum(y => y.Valor));
        var valorProdutos = result.Sum(x => x.Produtos.Sum(y => y.Valor_Venda));
        return new DashboardReceitaMes() { Valor = (decimal)(valorProdutos + valorService) };
    }
    private DashboardReceitaMes MapperSalerMonthProduct(ICollection<OrdemVenda> result)
    {
        var valorProdutos = result.Sum(x => x.Produtos.Sum(y => y.Valor_Venda));
        return new DashboardReceitaMes() { Valor = (decimal)valorProdutos };
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

    public async Task<ICollection<OrdemVenda>?> GetOrdemVendaProductListAll(Guid prestador)
    {
        var result = _context.OrdemVenda
            .Where(x => x.PrestadorId == prestador && x.Status == EOrdemVendaStatus.Concluido)
            .Include(i => i.Produtos).ToList();

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
