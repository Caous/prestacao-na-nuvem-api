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
    public OrdemVendaController()
    {

    }
    
    /// <summary>
    /// Adicionar prestação de serviço
    /// </summary>
    /// <param name="prestacaoServico"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Add(OrdemVendaDto request)
    {
        //if (!ModelState.IsValid)
        //    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        //MapearLogin(prestacaoServico);
        //var result = await _repository.CreatePrestacaoServico(prestacaoServico);

        //if (result == null)
        //    return NoContent();

        return Ok();
    }

    private void MapearLogin(OrdemVendaDto request)
    {
        if (!request.PrestadorId.HasValue)
            request.PrestadorId = PrestadorId;

        request.UsrCadastroDesc = UserName;
        request.UsrCadastro = UserId;
    }

    /// <summary>
    /// Recupera todas prestação de serviço
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        //PrestacaoServicoDto filter = new PrestacaoServicoDto() { PrestadorId = PrestadorId };

        //var result = await _repository.GetAllPrestacaoServico(filter);

        return Ok();
    }

    /// <summary>
    /// Recupera uma prestação de serviço por Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetId(Guid id)
    {
        //if (!ModelState.IsValid)
        //    return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        //var result = await _repository.FindByIdPrestacaoServico(id);

        //if (result == null)
        //    return NoContent();

        return Ok();
    }
}
