using SmartOficina.Api.Domain.Interfaces;

namespace SmartOficina.Api.Controllers;

/// <summary>
/// Controller de prestação de serviço
/// </summary>
[Route("api/[controller]")]
[ApiController, Authorize]
[Produces("application/json")]
[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
public class PrestacaoServicoController : MainController
{
    private readonly IPrestacaoServicoService _repository;

    public PrestacaoServicoController(IPrestacaoServicoService repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Adicionar prestação de serviço
    /// </summary>
    /// <param name="prestacaoServico"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Add(PrestacaoServicoDto prestacaoServico)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var result = await _repository.CreatePrestacaoServico(prestacaoServico);

        if (result == null)
            return NoContent();

        return Ok(result);
    }

    /// <summary>
    /// Recupera todas prestação de serviço
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        PrestacaoServicoDto filter = new PrestacaoServicoDto() { PrestadorId = PrestadorId };

        var result = await _repository.GetAllPrestacaoServico(filter);

        return Ok(result);
    }

    /// <summary>
    /// Recupera uma prestação de serviço por Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetId(Guid id)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var result = await _repository.FindByIdPrestacaoServico(id);

        if (result == null)
            return NoContent();

        return Ok(result);
    }

    /// <summary>
    /// Recupera ordens de serviço fechadas
    /// </summary>
    /// <returns></returns>
    [HttpGet("PrestacaoServicoFechadosPrestador")]
    public async Task<IActionResult> GetByPrestacaoServicoFechadosPrestador()
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        List<EPrestacaoServicoStatus> status = new List<EPrestacaoServicoStatus>() { EPrestacaoServicoStatus.Concluido, EPrestacaoServicoStatus.Rejeitado };
        var result = await _repository.GetByPrestacoesServicosStatus(PrestadorId, status);
        if (result == null)
            return NoContent();
        return Ok(result);
    }

    /// <summary>
    /// Recupera ordens de serviço abertas
    /// </summary>
    /// <returns></returns>
    [HttpGet("PrestacaoServicoAbertoPrestador")]
    public async Task<IActionResult> GetByPrestacaoServicoAbertosPrestador()
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);


        List<EPrestacaoServicoStatus> status = new List<EPrestacaoServicoStatus>() { EPrestacaoServicoStatus.Aberto, EPrestacaoServicoStatus.Analise, EPrestacaoServicoStatus.Andamento, EPrestacaoServicoStatus.Aprovado, EPrestacaoServicoStatus.Teste };

        var result = await _repository.GetByPrestacoesServicosStatus(PrestadorId, status);

        if (result == null)
            return NoContent();

        return Ok(result);
    }

    /// <summary>
    /// Recupera prestação de serviços com vários dados
    /// </summary>
    /// <returns></returns>
    [HttpGet("PrestacaoServicoEnriquecidoPrestador")]
    public async Task<IActionResult> GetByPrestacaoServicoEnriquecidoPrestador()
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var result = await _repository.GetByPrestador(PrestadorId);

        if (result == null)
            return NoContent();

        return Ok(result);
    }

    /// <summary>
    /// Atualiza uma prestação de serviço
    /// </summary>
    /// <param name="prestacaoServico"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> AtualizarPrestacaoServico(PrestacaoServicoDto prestacaoServico)
    {
        if (!ModelState.IsValid || !prestacaoServico.Id.HasValue)
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }     

        var result = await _repository.UpdatePrestacaoServico(prestacaoServico);

        if (result == null)
            return NoContent();

        return Ok(result);
    }

    /// <summary>
    /// Desativar prestação de serviço
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut("DesativarPrestacao")]
    public async Task<IActionResult> DesativarPrestadorServico(Guid id)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var result = await _repository.Desabled(id);

        if (result == null) 
            return NoContent();

        return Ok(result);
    }

    /// <summary>
    /// Deletar uma prestação de serviço
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<IActionResult> DeletarPrestador(Guid id)
    {
        try
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            
            await _repository.Delete(id);

            return Ok("Deletado");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Mudar status de uma ordem de serviço
    /// </summary>
    /// <param name="id"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    [HttpPut("status/{id}/{status}")]
    public async Task<IActionResult> ChangeStatus(Guid id, EPrestacaoServicoStatus status)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        
        await _repository.ChangeStatus(id, status);
        return Ok();
    }

}
