namespace SmartOficina.Api.Controllers;

[Route("api/[controller]")]
[ApiController, Authorize]
public class PrestadorController : MainController
{
    private readonly IPrestadorRepository _repository;
    private readonly IFuncionarioPrestadorRepository _repositoryFuncionario;
    private readonly IMapper _mapper;

    public PrestadorController(IPrestadorRepository repository, IFuncionarioPrestadorRepository repositoryFuncionario, IMapper mapper)
    {
        _repository = repository;
        _repositoryFuncionario = repositoryFuncionario;
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

        MapearLogin(prestador);

        var result = await _repository.Create(_mapper.Map<Prestador>(prestador));

        return Ok(_mapper.Map<PrestadorDto>(result));
    }

    private void MapearLogin(PrestadorDto prestador)
    {
        prestador.UsrCadastro = UserId;
        prestador.UsrCadastroDesc = UserName;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _repository.GetAll();
        return Ok(_mapper.Map<ICollection<PrestadorDto>>(result));
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

        return Ok(_mapper.Map<PrestadorDto>(result));
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

        MapearLogin(prestador);

        var result = await _repository.Update(_mapper.Map<Prestador>(prestador));

        return Ok(_mapper.Map<PrestadorDto>(result));
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

        var result = await _repository.Desabled(id);

        return Ok(_mapper.Map<PrestadorDto>(result));
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
            return Ok("Deletado");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    #endregion

    #region Controller Funcionario Prestador

    private void MapearLoginFuncionario(FuncionarioPrestadorDto func)
    {
        if (!func.PrestadorId.HasValue)
            func.PrestadorId = PrestadorId;

        func.UsrCadastroDesc = UserName;
        func.UsrCadastro = UserId;
    }

    [HttpPost("Funcionario")]
    public async Task<IActionResult> AddFuncionario(FuncionarioPrestadorDto func)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        MapearLoginFuncionario(func);

        var result = await _repositoryFuncionario.Create(_mapper.Map<FuncionarioPrestador>(func));

        return Ok(_mapper.Map<FuncionarioPrestadorDto>(result));
    }

    [HttpGet("Funcionario")]
    public async Task<IActionResult> GetAllFuncionario()
    {
        var result = await _repositoryFuncionario.GetAll();
        return Ok(_mapper.Map<ICollection<FuncionarioPrestadorDto>>(result));
    }

    [HttpGet("Funcionario/Prestador/id")]
    public async Task<IActionResult> GetAllFuncionarioPorPrestador(Guid id)
    {
        var result = await _repositoryFuncionario.GetListaFuncionarioPrestadorAsync(id);
        return Ok(_mapper.Map<ICollection<FuncionarioPrestadorDto>>(result));
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

        var result = await _repositoryFuncionario.FindById(id);

        return Ok(_mapper.Map<FuncionarioPrestadorDto>(result));
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
        MapearLoginFuncionario(func);

        var result = await _repositoryFuncionario.Update(_mapper.Map<FuncionarioPrestador>(func));

        return Ok(_mapper.Map<FuncionarioPrestadorDto>(result));
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

        var result = await _repositoryFuncionario.Desabled(id);

        return Ok(_mapper.Map<FuncionarioPrestadorDto>(result));

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

            await _repositoryFuncionario.Delete(id);

            return Ok("Deletado");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    #endregion

}
