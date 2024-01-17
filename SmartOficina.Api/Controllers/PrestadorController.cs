using SmartOficina.Api.Domain.Interfaces;

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
    private readonly IPrestadorService _prestadorService;
    private readonly IFuncionarioService _funcionarioSerive;

    public PrestadorController(IPrestadorService prestadorService, IFuncionarioService funcionarioSerive)
    {
        _prestadorService = prestadorService;
        _funcionarioSerive = funcionarioSerive;
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
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        MapearLogin(prestador);

        var result = await _prestadorService.CreatePrestador(prestador);

        if (result == null)
            return NoContent();

        return Ok(result);
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
        var result = await _prestadorService.GetAllPrestador(new PrestadorDto() { PrestadorId = PrestadorId });
        if (result == null)
            return NoContent();
        return Ok(result);
    }

    /// <summary>
    /// Recuperar prestador de serviço id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetId(Guid id)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var result = await _prestadorService.FindByIdPrestador(id);

        if (result == null)
            return NoContent();

        return Ok(result);
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

        var result = await _prestadorService.UpdatePrestador(prestador);
        if (result == null)
            return NoContent();
        return Ok(result);
    }

    /// <summary>
    /// Desativa um prestador
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut("DesativarPrestador")]
    public async Task<IActionResult> DesativarPrestadorServico(Guid id, Guid userDesabled)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var result = await _prestadorService.Desabled(id, userDesabled);
        if (result == null)
            return NoContent();
        return Ok(result);
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
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            await _prestadorService.Delete(id);
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
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);        

        MapearLoginFuncionario(func);

        var result = await _funcionarioSerive.CreateFuncionario(func);

        if (result == null)
            return NoContent();

        return Ok(result);
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
        FuncionarioPrestadorDto filter = MapperFilter(cpf, email, nome);

        var result = await _funcionarioSerive.GetAllFuncionario(filter);
        if (result == null || !result.Any())
            return NoContent();
        return Ok(result);
    }

    private static FuncionarioPrestadorDto MapperFilter(string? cpf, string? email, string? nome)
    {
        return new FuncionarioPrestadorDto() { Cargo = string.Empty, CPF = cpf, Email = email, Nome = nome, RG = string.Empty, Telefone = string.Empty, Endereco = string.Empty };
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
        FuncionarioPrestadorDto filter = new FuncionarioPrestadorDto() { Cargo = string.Empty, CPF = _cpf, Email = _email, Nome = _nome, RG = string.Empty, Telefone = string.Empty, Endereco = string.Empty };

        var result = await _funcionarioSerive.GetAllFuncionario(filter);
        if (result == null || !result.Any())
            return NoContent();
        return Ok(result);
    }

    /// <summary>
    /// Recuperar funcionário por Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("Funcionario/{id}")]
    public async Task<IActionResult> GetIdFuncionario(Guid id)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        

        var result = await _funcionarioSerive.FindByIdFuncionario(id);

        if (result == null)
            return NoContent();

        return Ok(result);
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

        var result = await _funcionarioSerive.UpdateFuncionario(func);
        if (result == null)
            return NoContent();
        return Ok(result);
    }

    /// <summary>
    /// Desativar um funcionário
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut("DesativarFuncionario")]
    public async Task<IActionResult> DesativarFuncionario(Guid id, Guid userDesabled)
    {
        if (!ModelState.IsValid || id == null)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        

        var result = await _funcionarioSerive.Desabled(id, userDesabled);
        if (result == null)
            return NoContent();
        return Ok(result);

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
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);


            await _funcionarioSerive.Delete(id);

            return Ok("Deletado");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    #endregion

}
