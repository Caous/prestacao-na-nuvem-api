using static PrestacaoNuvem.Api.Dto.DashboardDto;

namespace PrestacaoNuvem.Api.Domain.Interfaces;

public interface IDashboardService
{
    Task<ICollection<DashboardReceitaDiariaDto>?> GetDailySales(Guid prestador);
    Task<ICollection<DashboardReceitaCategoriaDto>?> GetServicesGroupByCategoryService(Guid prestador);
    Task<ICollection<DashboardReceitaSubCaterogiaDto>?> GetServicesGroupBySubCategoryService(Guid prestador);
    Task<DashboardReceitaMesDto?> GetSalesMonth(Guid prestador);
    Task<DashboardClientesNovos?> GetNewCustomerMonth(Guid prestador);
    Task<DashboardProdutosNovos?> GetSalesProductMonth(Guid prestador);
    Task<DashboardOSMes?> GetOSMonth(Guid prestador);
}
