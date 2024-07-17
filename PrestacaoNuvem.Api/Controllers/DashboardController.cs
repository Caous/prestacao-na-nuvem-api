using static PrestacaoNuvem.Api.Dto.DashboardDto;

namespace PrestacaoNuvem.Api.Controllers;

/// <summary>
/// Controller de Dashboard
/// </summary>
[Route("api/[controller]")]
[ApiController, Authorize]
[Produces("application/json")]
[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
public class DashboardController : MainController
{
    private readonly IDashboardService _dashboardService;
    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    /// <summary>
    /// Recuperar faturamento diário
    /// </summary>
    /// <returns></returns>
    [HttpGet("DashboardFaturamentoDiario")]
    public async Task<IActionResult> GetDashboardFaturamentoDiario()
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var resultDash = await _dashboardService.GetDailySales(PrestadorId);

        if (resultDash == null || !resultDash.Any())
            return NoContent();

        object[] result = ResultMapperDiario(resultDash);

        return Ok(result);
    }

    private static object[] ResultMapperDiario(ICollection<DashboardReceitaDiariaDto> resultDash)
    {
        return resultDash.GroupBy(x => x.DateRef).Select(grupo => new { Key = grupo.Key, Count = grupo.Sum(x => x.Valor) }).ToArray();
    }

    /// <summary>
    /// Recuperar Faturamento Mês Por Categoria de Serviço
    /// </summary>
    /// <returns></returns>
    [HttpGet("DashboardCategoriaAgrupado")]
    public async Task<IActionResult> GetDashboardCategoriaAgrupado()
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var resultDash = await _dashboardService.GetServicesGroupByCategoryService(PrestadorId);

        if (resultDash == null || !resultDash.Any())
            return NoContent();

        object[] result = ResultMapperCategoriaServico(resultDash);

        return Ok(result);
    }

    /// <summary>
    /// Recuperar Faturamento Mês Por Nome Produto Agrupado
    /// </summary>
    /// <returns></returns>
    [HttpGet("DashboardProdutoNomeAgrupado")]
    public async Task<IActionResult> GetDashboardNomeAgrupado()
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var resultDash = await _dashboardService.GetProductGroupByProductNameService(PrestadorId);

        if (resultDash == null || !resultDash.Any())
            return NoContent();

        object[] result = ResultMapperProdutoNome(resultDash);

        return Ok(result);
    }

    /// <summary>
    /// Recuperar Faturamento Mês Por Nome Marca Agrupado
    /// </summary>
    /// <returns></returns>
    [HttpGet("DashboardProdutoMarcaAgrupado")]
    public async Task<IActionResult> GetDashboardMarcaAgrupado()
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var resultDash = await _dashboardService.GetProductGroupByProductMarcaService(PrestadorId);

        if (resultDash == null || !resultDash.Any())
            return NoContent();

        object[] result = ResultMapperProdutoMarca(resultDash);

        return Ok(result);
    }

    private static object[] ResultMapperProdutoNome(ICollection<DashboardReceitaNomeProdutoDto>? resultDash)
    {
        return resultDash.GroupBy(x => x.Nome).Select(grupo => new { Key = grupo.Key, Count = grupo.Sum(x => x.Valor).ToString() }).ToArray();
    }
    private static object[] ResultMapperProdutoMarca(ICollection<DashboardReceitaMarcaProdutoDto>? resultDash)
    {
        return resultDash.GroupBy(x => x.Marca).Select(grupo => new { Key = grupo.Key, Count = grupo.Sum(x => x.Valor).ToString() }).ToArray();
    }
    private static object[] ResultMapperCategoriaServico(ICollection<DashboardReceitaCategoriaDto>? resultDash)
    {
        return resultDash.GroupBy(x => x.Categoria).Select(grupo => new { Key = grupo.Key, Count = grupo.Sum(x => x.Valor).ToString() }).ToArray();
    }

    /// <summary>
    /// Recuperar Faturamento Mês Sub Categoria Agrupado
    /// </summary>
    /// <returns></returns>
    [HttpGet("DashboardSubCategoriaAgrupado")]
    public async Task<IActionResult> GetDashboardSubCategoriaAgrupado()
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var resultDash = await _dashboardService.GetServicesGroupBySubCategoryService(PrestadorId);

        if (resultDash == null || !resultDash.Any())
            return NoContent();

        object[] resultadoArray = ResultMapperSubCategoriaServico(resultDash);

        return Ok(resultadoArray);
    }

    private static object[] ResultMapperSubCategoriaServico(ICollection<DashboardReceitaSubCaterogiaDto>? resultDash)
    {
        return resultDash.GroupBy(x => x.Titulo).Select(grupo => new { Key = grupo.Key, Count = grupo.Sum(x => x.Valor) }).ToArray();
    }

    /// <summary>
    /// Recuperar Faturamento Mês
    /// </summary>
    /// <returns></returns>
    [HttpGet("DashboardReceitaMes")]
    public async Task<IActionResult> GetDashboardReceitaMes()
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var result = await _dashboardService.GetSalesMonth(PrestadorId);

        if (result == null)
            return NoContent();

        return Ok(result);
    }

    /// <summary>
    /// Recuperar Clientes Mês
    /// </summary>
    /// <returns></returns>
    [HttpGet("DashboardClientesNovosMes")]
    public async Task<IActionResult> GetDashboardClientesNovosMes()
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);


        var result = await _dashboardService.GetNewCustomerMonth(PrestadorId);

        if (result == null)
            return NoContent();

        return Ok(result);
    }

    /// <summary>
    /// Recupera as categorias agrupadas
    /// </summary>
    /// <returns></returns>
    [HttpGet("DashboardProdutosVendidosMes")]
    public async Task<IActionResult> GetDashboardProdutosVendidosMes()
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var result = await _dashboardService.GetSalesProductMonth(PrestadorId);

        if (result == null)
            return NoContent();

        return Ok(result);
    }

    /// <summary>
    /// Recupera as categorias agrupadas
    /// </summary>
    /// <returns></returns>
    [HttpGet("DashboardOSMes")]
    public async Task<IActionResult> GetDashboardServicosMes()
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var result = await _dashboardService.GetOSMonth(PrestadorId);

        if (result == null)
            return NoContent();

        return Ok(result);
    }

    /// <summary>
    /// Recupera faturamento por mês agrupado
    /// </summary>
    /// <returns></returns>
    [HttpGet("DashboardBarVendaMeses")]
    public async Task<IActionResult> GetDashboardBarMes()
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var resultDash = await _dashboardService.GetDailySalesGroupMonth(PrestadorId);

        if (resultDash == null || !resultDash.Any())
            return NoContent();

        object[] result = ResultMapperOrdemVendaMesAgrupado(resultDash);

        return Ok(result);
    }

    private object[] ResultMapperOrdemVendaMesAgrupado(ICollection<DashboardReceitaMesAgrupadoDto> resultDash)
    {
        return resultDash.GroupBy(x => x.DateRef).Select(grupo => new { Key = grupo.Key, Count = grupo.Sum(x => x.Valor) }).ToArray();
    }

    /// <summary>
    /// Retona os ultimos 10 serviços efetuados
    /// </summary>
    /// <returns></returns>
    [HttpGet("DashboardUltimosOs/{limit}")]
    public async Task<IActionResult> GetDashboardLastOS(int limit)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var resultDash = await _dashboardService.GetLastServices(PrestadorId, limit);

        if (resultDash == null || !resultDash.Any())
            return NoContent();       

        return Ok(resultDash);
    }

}
