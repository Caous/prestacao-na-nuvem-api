namespace SmartOficina.Api.Controllers;

/// <summary>
/// Controller de cliente
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

    private void MapearLogin(CategoriaServicoDto categoriaServico)
    {
        if (!categoriaServico.PrestadorId.HasValue)
            categoriaServico.PrestadorId = PrestadorId;

        categoriaServico.UsrCadastroDesc = UserName;
        categoriaServico.UsrCadastro = UserId;

    }

    /// <summary>
    /// Recupera as categorias agrupadas
    /// </summary>
    /// <param name="titulo"></param>
    /// <param name="desc"></param>
    /// <returns></returns>
    [HttpGet("DashboardCategoriaAgrupado")]
    public async Task<IActionResult> GetDashboardCategoriaAgrupado()
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        //var resultadoxpto = _dashboardService.ListarServicosAgrupados(PrestadorId);

        var listaxpto = new List<CategoriaServicoDto>() { new CategoriaServicoDto() { Desc = "1", Titulo = "Categoria 1" }, new CategoriaServicoDto() { Desc = "2", Titulo = "Categoria 2" }, new CategoriaServicoDto() { Desc = "3", Titulo = "Categoria 3" }, new CategoriaServicoDto() { Desc = "1", Titulo = "Categoria 1" }, }.GroupBy(x => x.Titulo);

        // Crie um dicionário para armazenar os resultados
        var resultado = new Dictionary<string, string>();

        // Preencha o dicionário com os dados da lista agrupada
        foreach (var grupo in listaxpto)
        {
            resultado[grupo.Key] = grupo.Count().ToString();
        }


        if (resultado == null || !resultado.Any())
            return NoContent();

        return Ok(resultado);
    }

    /// <summary>
    /// Recupera as categorias agrupadas
    /// </summary>
    /// <param name="titulo"></param>
    /// <param name="desc"></param>
    /// <returns></returns>
    [HttpGet("DashboardSubCategoriaAgrupado")]
    public async Task<IActionResult> GetDashboardSubCategoriaAgrupado()
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        //var resultadoxpto = _dashboardService.ListarServicosAgrupados(PrestadorId);

        var listaxpto = new List<SubCategoriaServico>() { new SubCategoriaServico() { Desc = "1", Titulo = "Categoria 1" }, new SubCategoriaServico() { Desc = "2", Titulo = "Categoria 2" }, new SubCategoriaServico() { Desc = "3", Titulo = "Categoria 3" }, new SubCategoriaServico() { Desc = "1", Titulo = "Categoria 1" }, }.GroupBy(x => x.Titulo);

        // Crie um dicionário para armazenar os resultados
        var resultado = new Dictionary<string, string>();

        // Preencha o dicionário com os dados da lista agrupada
        foreach (var grupo in listaxpto)
        {
            resultado[grupo.Key] = grupo.Count().ToString();
        }


        if (resultado == null || !resultado.Any())
            return NoContent();

        return Ok(resultado);
    }

    /// <summary>
    /// Recupera as categorias agrupadas
    /// </summary>
    /// <param name="titulo"></param>
    /// <param name="desc"></param>
    /// <returns></returns>
    [HttpGet("DashboardReceitaMes")]
    public async Task<IActionResult> GetDashboardReceitaMes()
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        //var resultadoxpto = _dashboardService.ListarServicosAgrupados(PrestadorId);
        Random random = new Random();
        decimal valorMinimo = 1000.0m;
        decimal valorMaximo = 8000.0m;

        // Gera um valor decimal aleatório entre 1000.00 e 8000.00
        decimal resultado = (decimal)(random.NextDouble() * (double)(valorMaximo - valorMinimo) + (double)valorMinimo);

        if (resultado == null)
            return NoContent();

        return Ok(new DashboardReceitaMesDto() { valor = resultado });
    }

    /// <summary>
    /// Recupera as categorias agrupadas
    /// </summary>
    /// <param name="titulo"></param>
    /// <param name="desc"></param>
    /// <returns></returns>
    [HttpGet("DashboardClientesNovosMes")]
    public async Task<IActionResult> GetDashboardClientesNovosMes()
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        //var resultadoxpto = _dashboardService.ListarServicosAgrupados(PrestadorId);
        Random random = new Random();

        var resultado = random.NextInt64(5000);

        if (resultado == null)
            return NoContent();

        return Ok(new DashboardClientesNovos() { valor = resultado });
    }

    /// <summary>
    /// Recupera as categorias agrupadas
    /// </summary>
    /// <param name="titulo"></param>
    /// <param name="desc"></param>
    /// <returns></returns>
    [HttpGet("DashboardProdutosVendidosMes")]
    public async Task<IActionResult> GetDashboardProdutosVendidosMes()
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        //var resultadoxpto = _dashboardService.ListarServicosAgrupados(PrestadorId);
        Random random = new Random();

        var resultado = random.NextInt64(300);

        if (resultado == null)
            return NoContent();

        return Ok(new DashboardProdutosNovos() { valor = resultado });
    }

    /// <summary>
    /// Recupera as categorias agrupadas
    /// </summary>
    /// <param name="titulo"></param>
    /// <param name="desc"></param>
    /// <returns></returns>
    [HttpGet("DashboardServicosMes")]
    public async Task<IActionResult> GetDashboardServicosMes()
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        //var resultadoxpto = _dashboardService.ListarServicosAgrupados(PrestadorId);
        Random random = new Random();

        var resultado = random.NextInt64(4000);

        if (resultado == null)
            return NoContent();

        return Ok(new DashboardServicosMes() { valor = resultado });
    }
}
