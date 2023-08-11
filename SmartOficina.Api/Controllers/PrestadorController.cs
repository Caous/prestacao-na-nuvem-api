namespace SmartOficina.Api.Controllers;

[Route("api/[controller]")]
[ApiController, Authorize]
public class PrestadorController : MainController
{
    private readonly IPrestadorRepository _repository;
    private readonly IMapper _mapper;

    public PrestadorController(IPrestadorRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    #region Controller Prestador
    [HttpPost]
    public async Task<IActionResult> Add(PrestadorDto prestador)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }
        return Ok(await _repository.Create(_mapper.Map<Prestador>(prestador)));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _repository.GetAll());
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
        return Ok(await _repository.FindById(id));
    }

    [HttpPut]
    public async Task<IActionResult> AtualizarPrestador(PrestadorDto prestador)
    {
        if (!ModelState.IsValid || !prestador.Id.HasValue)
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }
        return Ok(await _repository.Update(_mapper.Map<Prestador>(prestador)));
    }

    [HttpPut("DesativarPrestador")]
    public async Task<IActionResult> DesativarPrestadorServico(Guid id)
    {
        if (!ModelState.IsValid || id == null)
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }
        return Ok(await _repository.Desabled(id));
    }

    [HttpDelete]
    public async Task<IActionResult> DeletarPrestador(Guid id)
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
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    #endregion

    #region Controller Funcionario Prestador
    [HttpPost("Funcionario")]
    public async Task<IActionResult> AddFuncionario(FuncionarioPrestadorDto func)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }
        return Ok();
    }

    [HttpGet("Funcionario")]
    public async Task<IActionResult> GetAllFuncionario()
    {
        return Ok();
    }

    [HttpGet("Funcionario/{id}")]
    public async Task<IActionResult> GetIdFuncionario(Guid id)
    {
        if (!ModelState.IsValid || id == null)
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }
        return Ok();
    }

    [HttpPut("Funcionario")]
    public async Task<IActionResult> AtualizarFuncionario(FuncionarioPrestadorDto func)
    {
        if (!ModelState.IsValid || !func.Id.HasValue)
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }
        return Ok();
    }

    [HttpPut("DesativarFuncionario")]
    public async Task<IActionResult> DesativarFuncionario(Guid id)
    {
        if (!ModelState.IsValid || id == null)
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }
        return Ok(await _repository.Desabled(id));
    }

    [HttpDelete("Funcionario")]
    public async Task<IActionResult> DeletarFuncionario(Guid id)
    {
        try
        {
            if (!ModelState.IsValid || id == null)
            {
                if (ModelState.ErrorCount < 1)
                    ModelState.AddModelError("error", "Id invalid");

                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    #endregion
    
}
