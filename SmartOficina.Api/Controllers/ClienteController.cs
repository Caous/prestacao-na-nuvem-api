using SmartOficina.Api.Infrastructure.Repositories.Interfaces;

namespace SmartOficina.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClienteController : ControllerBase
{
    private readonly IClienteRepository _repository;
    private readonly IMapper _mapper;

    public ClienteController(IClienteRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(ClienteDto cliente)
    {
        return Ok(await _repository.Create(_mapper.Map<Cliente>(cliente)));
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
    public async Task<IActionResult> AtualizarCliente(ClienteDto cliente)
    {
        return Ok(await _repository.Update(_mapper.Map<Cliente>(cliente)));
    }

    [HttpPut("DesativarCliente")]
    public async Task<IActionResult> DesativarCliente(Guid id)
    {
        return Ok(await _repository.Desabled(id));
    }

    [HttpDelete]
    public async Task<IActionResult> DeletarCliente(Guid id)
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