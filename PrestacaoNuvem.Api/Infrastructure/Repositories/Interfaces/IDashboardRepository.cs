using PrestacaoNuvem.Api.Domain.Model.Dashboard;

namespace PrestacaoNuvem.Api.Infrastructure.Repositories.Interfaces;

public interface IDashboardRepository
{
    Task<ICollection<PrestacaoServico>> GetServicesGroupByCategoryService(Guid prestador);
    Task<ICollection<OrdemVenda>?> GetOrdemVendaProductListAll(Guid prestador, bool filtroMesAtual);
    Task<ICollection<PrestacaoServico>> GetServicesGroupBySubCategoryService(Guid prestador);
    Task<ICollection<DashboardReceitaDiaria>> GetDailySales(Guid prestador);
    Task<DashboardReceitaMes> GetSalesMonth(Guid prestador);
    Task<ICollection<Cliente>> GetNewCustomerMonth(Guid prestador);
    Task<ICollection<Produto>> GetSalesProductMonth(Guid prestador);
    Task<DashboardOSMes> GetOSOVMonth(Guid prestador);
}