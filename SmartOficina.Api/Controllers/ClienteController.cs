namespace SmartOficina.Api.Controllers;

[Route("api/[controller]")]
[ApiController, Authorize]
public class ClienteController : MainController
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
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        if (!cliente.PrestadorId.HasValue)
            cliente.PrestadorId = PrestadorId;

        cliente.UsrCadastroDesc = UserName;
        cliente.UsrCadastro = UserId;

        var result = await _repository.Create(_mapper.Map<Cliente>(cliente));

        return Ok(_mapper.Map<ClienteDto>(result));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }
        var result = await _repository.GetAll();
        return Ok(_mapper.Map<ICollection<ClienteDto>>(result));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetId(Guid id)
    {
        if (!ModelState.IsValid || id == null)
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }
        var result = await _repository.FindById(id);
        return Ok(_mapper.Map<ClienteDto>(result));
    }

    [HttpPut]
    public async Task<IActionResult> AtualizarCliente(ClienteDto cliente)
    {
        if (!ModelState.IsValid || !cliente.Id.HasValue)
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        var result = await _repository.Update(_mapper.Map<Cliente>(cliente));

        return Ok(_mapper.Map<ClienteDto>(result));
    }

    [HttpPut("DesativarCliente")]
    public async Task<IActionResult> DesativarCliente(Guid id)
    {
        if (!ModelState.IsValid || id == null)
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        var result = await _repository.Desabled(id);

        return Ok(_mapper.Map<ClienteDto>(result));
    }

    [HttpDelete]
    public async Task<IActionResult> DeletarCliente(Guid id)
    {
        try
        {
            if (!ModelState.IsValid || id == null)
            {
                if (ModelState.ErrorCount < 1)
                    ModelState.AddModelError("error", "Id invalid");

                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            await _repository.Delete(id);
            return Ok("Deletado");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}