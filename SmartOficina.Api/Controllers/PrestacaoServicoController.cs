namespace SmartOficina.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrestacaoServicoController : ControllerBase
{
    private readonly IPrestacaoServicoRepository _repository;
    public PrestacaoServicoController(IPrestacaoServicoRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> Add(PrestacaoServico prestacaoServico)
    {
        return Ok(await _repository.Create(prestacaoServico));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _repository.GetAll());
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetId(Guid id)
    {
        return Ok(await _repository.FindById(id));
    }

    [HttpGet("PrestacaoServicoEnriquecidoPrestador/{id}")]
    public async Task<IActionResult> GetByPrestacaoServicoEnriquecidoPrestador(Guid id)
    {
        return Ok(await _repository.GetByPrestador(id));
    }

    [HttpPut]
    public async Task<IActionResult> AtualizarPrestacaoServico(PrestacaoServico prestacaoServico)
    {
        return Ok(await _repository.Update(prestacaoServico));
    }

    [HttpPut("DesativarPrestacao")]
    public async Task<IActionResult> DesativarPrestadorServico(Guid id)
    {
        return Ok(await _repository.Desabled(id));
    }

    [HttpDelete]
    public async Task<IActionResult> DeletarPrestador(Guid id)
    {
        try
        {
            await _repository.Delete(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("status/{id}/{status}")]
    public async Task<IActionResult> ChangeStatus(Guid id, EPrestacaoServicoStatus status)
    {
        await _repository.ChangeStatus(id, status);
        return Ok();
    }

}
