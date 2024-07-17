using static PrestacaoNuvem.Api.Dto.DashboardDto;

namespace PrestacaoNuvem.Api.Domain.Interfaces;

public interface IDashboardService
{
    Task<ICollection<DashboardReceitaDiariaDto>?> GetDailySales(Guid prestador);
    Task<ICollection<DashboardReceitaCategoriaDto>?> GetServicesGroupByCategoryService(Guid prestador);
    Task<ICollection<DashboardReceitaNomeProdutoDto>?> GetProductGroupByProductNameService(Guid prestador);
    Task<ICollection<DashboardReceitaMarcaProdutoDto>?> GetProductGroupByProductMarcaService(Guid prestador);    
    Task<ICollection<DashboardReceitaSubCaterogiaDto>?> GetServicesGroupBySubCategoryService(Guid prestador);
    Task<DashboardReceitaMesDto?> GetSalesMonth(Guid prestador);
    Task<DashboardClientesNovos?> GetNewCustomerMonth(Guid prestador);
    Task<DashboardProdutosNovos?> GetSalesProductMonth(Guid prestador);
    Task<DashboardOSMesDto?> GetOSMonth(Guid prestador);
    Task<ICollection<DashboardReceitaMesAgrupadoDto>?> GetDailySalesGroupMonth(Guid prestador);
    Task<ICollection<DashboardLastServices>> GetLastServices(Guid prestador, int limit = 0);
}
