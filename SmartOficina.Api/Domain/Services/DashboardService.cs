
namespace SmartOficina.Api.Domain.Services;

public class DashboardService : IDashboardService
{
    private readonly IDashboardRepository _dashboardService;
    public DashboardService(IDashboardRepository dashboardRepository)
    {
        _dashboardService = dashboardRepository;
    }
    public async Task<CategoriaServicoDto> ListarServicosAgrupados(Guid prestador)
    {
        var result = await _dashboardService.GetServicosGroupByCategoriaServico(prestador);

        return new CategoriaServicoDto();
    }
}
