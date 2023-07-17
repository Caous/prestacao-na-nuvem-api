namespace SmartOficina.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VeiculoController : ControllerBase
{
    private readonly IVeiculoRepository _repository;
    private readonly IMapper _mapper;

    public VeiculoController(IVeiculoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Add(VeiculoDto veiculo)
    {
        return Ok(await _repository.Create(_mapper.Map<Veiculo>(veiculo)));
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
    public async Task<IActionResult> AtualizarVeiculo(VeiculoDto veiculo)
    {
        return Ok(await _repository.Update(_mapper.Map<Veiculo>(veiculo)));
    }

    [HttpPut("DesativarVeiculo")]
    public async Task<IActionResult> DesativarVeiculo(Guid id)
    {
        return Ok(await _repository.Desabled(id));
    }

    [HttpDelete]
    public async Task<IActionResult> DeletarVeiculo(Guid id)
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
