namespace SmartOficina.Api.Domain.Interfaces;

public interface IDashboardService
{
    Task<CategoriaServicoDto> ListarServicosAgrupados(Guid prestador);
}
