namespace SmartOficina.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrestadorController : ControllerBase
{
    private readonly IPrestadorRepository _repository;
    private readonly IMapper _mapper;

    public PrestadorController(IPrestadorRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Add(PrestadorDto prestador)
    {
        return Ok(await _repository.Create(_mapper.Map<Prestador>(prestador)));
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
    public async Task<IActionResult> AtualizarPrestador(PrestadorDto prestador)
    {
        return Ok(await _repository.Update(_mapper.Map<Prestador>(prestador)));
    }

    [HttpPut("DesativarPrestador")]
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
