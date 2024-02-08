namespace PrestacaoNuvem.Api.Infrastructure.Repositories.Interfaces;

public interface IDashboardRepository
{
    Task<ICollection<PrestacaoServico>> GetServicesGroupByCategoryService(Guid prestador);
    Task<ICollection<PrestacaoServico>> GetServicesGroupBySubCategoryService(Guid prestador);
    Task<ICollection<PrestacaoServico>> GetDailySales(Guid prestador);
    Task<ICollection<PrestacaoServico>> GetSalesMonth(Guid prestador);
    Task<ICollection<Cliente>> GetNewCustomerMonth(Guid prestador);
    Task<ICollection<Produto>> GetSalesProductMonth(Guid prestador);
    Task<ICollection<PrestacaoServico>> GetOSMonth(Guid prestador);
}