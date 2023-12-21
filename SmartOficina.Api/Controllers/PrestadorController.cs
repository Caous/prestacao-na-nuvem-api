namespace SmartOficina.Api.Controllers;

/// <summary>
/// Controller de prestador de serviço e funcionário
/// </summary>
[Route("api/[controller]")]
[ApiController, Authorize]
[Produces("application/json")]
[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
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
    /// <summary>
    /// Adicionar um prestador de serviço
    /// </summary>
    /// <param name="prestador"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Recuperar uma lista de prestadores
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _repository.GetAll(PrestadorId, new Prestador());
        return Ok(_mapper.Map<ICollection<PrestadorDto>>(result));
    }

    /// <summary>
    /// Recuperar prestador de serviço id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Atualizar prestador de serviço
    /// </summary>
    /// <param name="prestador"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Desativa um prestador
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Deletar um prestador
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Adicionar um funcionário
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    [HttpPost("Funcionario")]
    public async Task<IActionResult> AddFuncionario(FuncionarioPrestadorDto func)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        MapearLoginFuncionario(func);

        var result = await _repositoryFuncionario.Create(_mapper.Map<FuncionarioPrestador>(func));

        if (result == null)
            NoContent();

        return Ok(_mapper.Map<FuncionarioPrestadorDto>(result));
    }

    /// <summary>
    /// Recuperar todos os funcionários
    /// </summary>
    /// <param name="cpf"></param>
    /// <param name="email"></param>
    /// <param name="nome"></param>
    /// <returns></returns>
    [HttpGet("Funcionario")]
    public async Task<IActionResult> GetAllFuncionario(string? cpf, string? email, string? nome)
    {
        var result = await _repositoryFuncionario.GetAll(PrestadorId, new FuncionarioPrestador() { Cargo = string.Empty, CPF = cpf, Email = email, Nome = nome, RG = string.Empty, Telefone = string.Empty });
        if (result == null || !result.Any())
            NoContent();
        return Ok(_mapper.Map<ICollection<FuncionarioPrestadorDto>>(result));
    }

    /// <summary>
    /// Recuperar todos os funcionários por prestador
    /// </summary>
    /// <param name="id"></param>
    /// <param name="_cpf"></param>
    /// <param name="_email"></param>
    /// <param name="_nome"></param>
    /// <returns></returns>
    [HttpGet("Funcionario/Prestador/id")]
    public async Task<IActionResult> GetAllFuncionarioPorPrestador(Guid id, [FromBody] string _cpf, string _email, string _nome)
    {
        var result = await _repositoryFuncionario.GetListaFuncionarioPrestadorAsync(id, new FuncionarioPrestador() { Cargo = string.Empty, CPF = _cpf, Email = _email, Nome = _nome, RG = string.Empty, Telefone = string.Empty });
        if (result == null || !result.Any())
            NoContent();
        return Ok(_mapper.Map<ICollection<FuncionarioPrestadorDto>>(result));
    }

    /// <summary>
    /// Recuperar funcionário por Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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

        if (result == null)
            NoContent();

        return Ok(_mapper.Map<FuncionarioPrestadorDto>(result));
    }

    /// <summary>
    /// Atualizar funcionario
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
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
        if (result == null)
            NoContent();
        return Ok(_mapper.Map<FuncionarioPrestadorDto>(result));
    }

    /// <summary>
    /// Desativar um funcionário
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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
        if (result == null)
            NoContent();
        return Ok(_mapper.Map<FuncionarioPrestadorDto>(result));

    }

    /// <summary>
    /// Deletar um funcionário
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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
