using SmartOficina.Api.Util;

namespace SmartOficina.Api.Controllers;

/// <summary>
/// Controller de cliente
/// </summary>
[Route("api/[controller]")]
[ApiController, Authorize]
[Produces("application/json")]
[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
public class ClienteController : MainController
{
    private readonly IClienteRepository _repository;
    private readonly IMapper _mapper;

    public ClienteController(IClienteRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    private void TratarDto(ClienteDto cliente)
    {
        cliente.CPF = CpfValidations.CpfSemPontuacao(cliente.CPF);
        cliente.Rg = RgValidations.RemoverPontuacaoRg(cliente.Rg);
        cliente.Telefone = TelefoneValidations.RemoverPontuacaoTelefone(cliente.Telefone);
    }

    /// <summary>
    /// Adicionar um cliente
    /// </summary>
    /// <param name="cliente"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> AddAsync(ClienteDto cliente)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        TratarDto(cliente);

        MapearLogin(cliente);

        var result = await _repository.Create(_mapper.Map<Cliente>(cliente));

        if (result == null)
            NoContent();

        return Ok(_mapper.Map<ClienteDto>(result));
    }

    private void MapearLogin(ClienteDto cliente)
    {
        if (!cliente.PrestadorId.HasValue)
            cliente.PrestadorId = PrestadorId;

        cliente.UsrCadastroDesc = UserName;
        cliente.UsrCadastro = UserId;
    }

    /// <summary>
    /// Recupera todos os cliente do prestador e também filtra
    /// </summary>
    /// <param name="cpf"></param>
    /// <param name="nome"></param>
    /// <param name="email"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll(string? cpf, string? nome, string? email)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        var result = await _repository.GetAll(PrestadorId, new Cliente() { CPF = cpf, Email = email, Nome = nome, PrestadorId = PrestadorId, Telefone = string.Empty });

        if (result == null || !result.Any())
            NoContent();

        return Ok(_mapper.Map<ICollection<ClienteDto>>(result));
    }

    /// <summary>
    /// Recupera um cliente especifício
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

        if (result == null)
            NoContent();

        return Ok(_mapper.Map<ClienteDto>(result));
    }

    /// <summary>
    /// Atualiar um cliente
    /// </summary>
    /// <param name="cliente"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> AtualizarCliente(ClienteDto cliente)
    {
        if (!ModelState.IsValid || !cliente.Id.HasValue)
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        MapearLogin(cliente);

        var result = await _repository.Update(_mapper.Map<Cliente>(cliente));

        if (result == null)
            NoContent();

        return Ok(_mapper.Map<ClienteDto>(result));
    }

    /// <summary>
    /// Desativa um cliente
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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

        if (result == null)
            NoContent();

        return Ok(_mapper.Map<ClienteDto>(result));
    }


    /// <summary>
    /// Deletar um cliente
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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