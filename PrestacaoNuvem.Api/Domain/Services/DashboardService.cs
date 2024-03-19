using NuGet.Protocol.Core.Types;
using static PrestacaoNuvem.Api.Domain.Model.Dashboards;
using static PrestacaoNuvem.Api.Dto.DashboardDto;

namespace PrestacaoNuvem.Api.Domain.Services;

public class DashboardService : IDashboardService
{
    private readonly IDashboardRepository _repository;
    private readonly IMapper _mapper;

    public DashboardService(IDashboardRepository dashboardRepository, IMapper mapper)
    {
        _repository = dashboardRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<DashboardReceitaDiariaDto>?> GetDailySales(Guid prestador)
    {
        ICollection<PrestacaoServico> resultDailySales = await _repository.GetDailySales(prestador);

        if (resultDailySales == null)
            return null;

        var result = resultDailySales.GroupBy(x => x.DataConclusaoServico).OrderBy(x=> x.Key).ToList();

        List<FaturamentoDiario> sales = new();

        foreach (var item in result)
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

        return MappearFaturamentoDiario(sales);
    }

    private ICollection<DashboardReceitaDiariaDto> MappearFaturamentoDiario(ICollection<FaturamentoDiario> result)
    {
        return _mapper.Map<ICollection<DashboardReceitaDiariaDto>>(result);
    }

    public async Task<ICollection<DashboardReceitaCategoriaDto>?> GetServicesGroupByCategoryService(Guid prestador)
    {
        ICollection<PrestacaoServico> result = await _repository.GetServicesGroupByCategoryService(prestador);

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
        ICollection<PrestacaoServico> result = await _repository.GetServicesGroupBySubCategoryService(prestador);

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
        ICollection<PrestacaoServico> result = await _repository.GetSalesMonth(prestador);

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
        ICollection<Cliente> result = await _repository.GetNewCustomerMonth(prestador);

        if (result == null)
            return null;

        return new DashboardClientesNovos() { Valor = result.Count() };
    }

    public async Task<DashboardProdutosNovos?> GetSalesProductMonth(Guid prestador)
    {
        ICollection<Produto> result = await _repository.GetSalesProductMonth(prestador);

        if (result == null)
            return null;

        return new DashboardProdutosNovos() { Valor = result.Count() };
    }

    public async Task<DashboardOSMes?> GetOSMonth(Guid prestador)
    {
        ICollection<PrestacaoServico> result = await _repository.GetOSMonth(prestador);

        if (result == null)
            return null;

        return new DashboardOSMes() { Valor = result.Count() };
    }
}
