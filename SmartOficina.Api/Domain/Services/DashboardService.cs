using static SmartOficina.Api.Domain.Model.Dashboards;

namespace SmartOficina.Api.Domain.Services;

public class DashboardService : IDashboardService
{
    private readonly IDashboardRepository _dashboardService;
    private readonly IMapper _mapper;

    public DashboardService(IDashboardRepository dashboardRepository, IMapper mapper)
    {
        _dashboardService = dashboardRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<DashboardReceitaDiariaDto>?> GetDailySales(Guid prestador)
    {
        ICollection<PrestacaoServico> resultDailySales = await _dashboardService.GetDailySales(prestador);

        if (resultDailySales == null)
            return null;

        var result = resultDailySales.GroupBy(x => x.DataConclusaoServico).ToList();

        List<FaturamentoDiario> sales = new();

        foreach (var item in result)
        {
            if (item != null && item.Key != null)
            {
                sales.Add(new FaturamentoDiario()
                {
                    DateRef = item.Key.Value,
                    Valor = (double)item.Sum(x => x.Servicos.Sum(y => y.Valor))
                });
            }
        }

        return MappearFaturamentoDiario(sales);
    }

    private ICollection<DashboardReceitaDiariaDto> MappearFaturamentoDiario(ICollection<FaturamentoDiario> result)
    {
        return _mapper.Map<ICollection<DashboardReceitaDiariaDto>>(result);
    }

    public async Task<ICollection<DashboardReceitaCategoriaDto>?> GetServicesGroupByCategoryService(Guid prestador)
    {
        ICollection<PrestacaoServico> result = await _dashboardService.GetServicesGroupByCategoryService(prestador);

        if (result == null)
            return null;

        List<CategoriaAgrupado> resultfinal = new();

        var xpto = result.Select(x => x.Servicos);

        foreach (var item in xpto)
        {
            if (item != null && item.Select(x => x.SubCategoriaServico.Categoria.Titulo).FirstOrDefault() != null)
            {
                resultfinal.Add(new CategoriaAgrupado
                {
                    Categoria = item.Select(x => x.SubCategoriaServico.Categoria.Titulo).FirstOrDefault(),
                    Valor = item.Select(x => x.SubCategoriaServico.ValorServico).Sum()
                });
            }
        }

        return MapperCategoriaAgrupado(resultfinal);
    }

    private ICollection<DashboardReceitaCategoriaDto> MapperCategoriaAgrupado(ICollection<CategoriaAgrupado> result)
    {
        return _mapper.Map<ICollection<DashboardReceitaCategoriaDto>>(result);
    }

    public async Task<ICollection<DashboardReceitaSubCaterogiaDto>?> GetServicesGroupBySubCategoryService(Guid prestador)
    {
        ICollection<PrestacaoServico> result = await _dashboardService.GetServicesGroupBySubCategoryService(prestador);

        if (result == null)
            return null;

        List<SubCategoriaAgrupado> resultfinal = new();

        var xpto = result.Select(x => x.Servicos);

        foreach (var item in xpto)
        {
            if (item != null && item.Select(x => x.SubCategoriaServico.Titulo).FirstOrDefault() != null)
            {
                resultfinal.Add(new SubCategoriaAgrupado
                {
                    Titulo = item.Select(x => x.SubCategoriaServico.Titulo).FirstOrDefault(),
                    Valor = item.Select(x => x.SubCategoriaServico.ValorServico).Sum()
                });
            }
        }

        return MapperSubCategoriaAgrupado(resultfinal);
    }

    private ICollection<DashboardReceitaSubCaterogiaDto> MapperSubCategoriaAgrupado(ICollection<SubCategoriaAgrupado> result)
    {

        return _mapper.Map<ICollection<DashboardReceitaSubCaterogiaDto>>(result);
    }

    public async Task<DashboardReceitaMesDto> GetSalesMonth(Guid prestador)
    {
        ICollection<PrestacaoServico> result = await _dashboardService.GetSalesMonth(prestador);

        if (result == null)
            return null;

        return MapperSalerMonth(result);
    }

    private DashboardReceitaMesDto MapperSalerMonth(ICollection<PrestacaoServico> result)
    {
        var valorService = result.Sum(x => x.Servicos.Sum(y => y.Valor));
        var valorProdutos = result.Sum(x => x.Produtos.Sum(y => y.Valor_Venda));
        return new DashboardReceitaMesDto() { Valor = (decimal)(valorProdutos + valorService) };
    }

    public async Task<DashboardClientesNovos> GetNewCustomerMonth(Guid prestador)
    {
        ICollection<Cliente> result = await _dashboardService.GetNewCustomerMonth(prestador);

        if (result == null)
            return null;

        return new DashboardClientesNovos() { Valor = result.Count() };
    }

    public async Task<DashboardProdutosNovos?> GetSalesProductMonth(Guid prestador)
    {
        ICollection<Produto> result = await _dashboardService.GetSalesProductMonth(prestador);

        if (result == null)
            return null;

        return new DashboardProdutosNovos() { Valor = result.Count() };
    }

    public async Task<DashboardOSMes?> GetOSMonth(Guid prestador)
    {
        ICollection<PrestacaoServico> result = await _dashboardService.GetOSMonth(prestador);

        if (result == null)
            return null;

        return new DashboardOSMes() { Valor = result.Count() };
    }
}
