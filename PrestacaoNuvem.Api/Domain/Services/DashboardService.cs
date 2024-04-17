using PrestacaoNuvem.Api.Domain.Model.Dashboard;
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
        ICollection<DashboardReceitaDiaria> resultDailySales = await _repository.GetDailySales(prestador);

        if (resultDailySales == null)
            return null;

        return MappearFaturamentoDiario(resultDailySales);
    }

    private ICollection<DashboardReceitaDiariaDto> MappearFaturamentoDiario(ICollection<DashboardReceitaDiaria> result)
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
        DashboardReceitaMes result = await _repository.GetSalesMonth(prestador);

        return _mapper.Map<DashboardReceitaMesDto>(result);
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

    public async Task<DashboardOSMesDto?> GetOSMonth(Guid prestador)
    {
        DashboardOSMes result = await _repository.GetOSOVMonth(prestador);

        if (result == null)
            return null;

        return _mapper.Map<DashboardOSMesDto>(result);
    }

    public async Task<ICollection<DashboardReceitaNomeProdutoDto>?> GetProductGroupByProductNameService(Guid prestador)
    {
        ICollection<OrdemVenda> result = await _repository.GetOrdemVendaProductListAll(prestador);

        if (result == null)
            return null;

        List<ProdutoAgrupado> resultfinal = new();

        var xpto = result.Select(x => x.Produtos);

        foreach (var item in xpto)
        {
            if (item != null && item.Select(x => x.Nome).FirstOrDefault() != null)
            {
                resultfinal.Add(new ProdutoAgrupado
                {
                    Nome = item.Select(x => x.Nome).FirstOrDefault(),
                    Valor = item.Select(x => x.Valor_Venda).Sum()
                });
            }
        }

        return MapperNomeProdutoAgrupado(resultfinal);
    }

    private ICollection<DashboardReceitaNomeProdutoDto>? MapperNomeProdutoAgrupado(List<ProdutoAgrupado> resultfinal)
    {
        return _mapper.Map<ICollection<DashboardReceitaNomeProdutoDto>>(resultfinal);
    }

    public async Task<ICollection<DashboardReceitaMarcaProdutoDto>?> GetProductGroupByProductMarcaService(Guid prestador)
    {
        ICollection<OrdemVenda> result = await _repository.GetOrdemVendaProductListAll(prestador);

        if (result == null)
            return null;

        List<ProdutoAgrupadoMarca> resultfinal = new();

        var xpto = result.Select(x => x.Produtos);

        foreach (var item in xpto)
        {
            if (item != null && item.Select(x => x.Marca).FirstOrDefault() != null)
            {
                resultfinal.Add(new ProdutoAgrupadoMarca
                {
                    Marca = item.Select(x => x.Marca).FirstOrDefault(),
                    Valor = item.Select(x => x.Valor_Venda).Sum()
                });
            }
        }

        return MapperMarcaProdutoAgrupado(resultfinal);
    }

    private ICollection<DashboardReceitaMarcaProdutoDto>? MapperMarcaProdutoAgrupado(List<ProdutoAgrupadoMarca> resultfinal)
    {
        return _mapper.Map<ICollection<DashboardReceitaMarcaProdutoDto>>(resultfinal);
    }

    public async Task<ICollection<DashboardReceitaMesAgrupadoDto>?> GetDailySalesGroupMonth(Guid prestador)
    {
        ICollection<OrdemVenda> result = await _repository.GetOrdemVendaProductListAll(prestador);

        if (result == null)
            return null;

        List<FaturamentoMes> resultfinal = new();

        foreach (var item in result)
        {
            if (item != null && item.Produtos != null)
            {
                resultfinal.Add(new FaturamentoMes
                {
                    DateRef = MappperMes(item.DataCadastro.Month.ToString()),
                    Valor = item.Produtos.Sum(y=> y.Valor_Venda)
                });
            }
        }

        return MapperSalesMonthGroup(resultfinal);
    }

    private ICollection<DashboardReceitaMesAgrupadoDto>? MapperSalesMonthGroup(List<FaturamentoMes> resultfinal)
    {
        return _mapper.Map<ICollection<DashboardReceitaMesAgrupadoDto>>(resultfinal);
    }

    private string MappperMes(string mes)
    {
        switch (mes)
        {
            case "1":
                return "Janeiro";
            case "2":
                return "Feveiro";
            case "3":
                return "Março";
            case "4":
                return "Abril";
            case "5":
                return "Maio";
            case "6":
                return "Junho";
            case "7":
                return "Julho";
            case "8":
                return "Agosto";
            case "9":
                return "Setembro";
            case "10":
                return "Outubro";
            case "11":
                return "Novembro";
            case "12":
                return "Dezembro";
            default:
                break;
        }

        return "Não encontrado";
    }
}
