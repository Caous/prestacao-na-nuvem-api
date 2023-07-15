namespace SmartOficina.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VeiculoController : ControllerBase
{
    private readonly IVeiculoRepository _repository;
    public VeiculoController(IVeiculoRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> Add(Veiculo veiculo)
    {
        return Ok(await _repository.Create(veiculo));
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
    public async Task<IActionResult> AtualizarVeiculo(Veiculo veiculo)
    {
        return Ok(await _repository.Update(veiculo));
    }

    [HttpPut("Desativar_Cliente")]
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
