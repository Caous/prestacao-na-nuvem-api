namespace PrestacaoNuvem.Api.Controllers;

[Route("api/[controller]")]
[ApiController, Authorize]
[Produces("application/json")]
[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
public class OrdemVendaController : MainController
{
    private readonly IOrdemVendaService _service;

    public OrdemVendaController(IOrdemVendaService service)
    {
        _service = service;
    }

    /// <summary>
    /// Adicionar ordem de venda
    /// </summary>
    /// <param name="ordemvendadto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Add(OrdemVendaDto request)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        MapearLogin(request);

        var result = await _service.CreateOrdemVenda(request);

        if (result == null)
            return NoContent();

        return Ok(result);
    }

    private void MapearLogin(OrdemVendaDto request)
    {
        if (!request.PrestadorId.HasValue)
            request.PrestadorId = PrestadorId;

        request.FuncionarioPrestadorId = UserId;

        request.UsrCadastroDesc = UserName;
        request.UsrCadastro = UserId;
    }

    /// <summary>
    /// Recupera todas ordem de venda
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        OrdemVendaDto filter = new() { PrestadorId = PrestadorId };

        var result = await _service.GetAllOrdemVenda(filter);

        return Ok(result);
    }

    /// <summary>
    /// Recupera uma odem de venda por Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetId(Guid id)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var result = await _service.FindByIdOrdemVenda(id);

        if (result == null)
            return NoContent();

        return Ok(result);
    }
}
