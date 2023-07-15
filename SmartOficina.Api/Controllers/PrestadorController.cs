namespace SmartOficina.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrestadorController : ControllerBase
{
    private readonly IPrestadorRepository _repository;
    public PrestadorController(IPrestadorRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> Add(Prestador prestador)
    {
        return Ok(await _repository.Create(prestador));
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

    [HttpPut]
    public async Task<IActionResult> AtualizarPrestador(Prestador prestador)
    {
        return Ok(await _repository.Update(prestador));
    }

    [HttpPut("Desativar_Cliente")]
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
}
