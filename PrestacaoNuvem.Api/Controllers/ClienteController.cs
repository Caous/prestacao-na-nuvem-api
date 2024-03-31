using PrestacaoNuvem.Api.Domain.Model;

namespace PrestacaoNuvem.Api.Controllers;

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
    private readonly IClienteService _clienteService;
    private readonly IValidator<ClienteDto> _validator;

    public ClienteController(IClienteService clienteService, IValidator<ClienteDto> validator)
    {
        _clienteService = clienteService;
        _validator = validator;
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
        var resultValitor = await _validator.ValidateAsync(cliente);

        if (resultValitor != null && !resultValitor.IsValid)
        {
            List<ErrosValidationsResponse> errors = new();

            foreach (var item in resultValitor.Errors)
                errors.Add(new ErrosValidationsResponse() { ErrorMensagem = item.ErrorMessage });

            return StatusCode(StatusCodes.Status400BadRequest, errors);
        }

        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);


        TratarDto(cliente);

        MapearLogin(cliente);

        var result = await _clienteService.CreateCliente(cliente);

        if (result == null)
            return NoContent();

        return Ok(result);
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
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        ClienteDto clienteDto = MapearDto(cpf, nome, email);

        clienteDto.CPF = CpfValidations.CpfSemPontuacao(clienteDto.CPF);

        var result = await _clienteService.GetAllCliente(clienteDto);

        if (result == null || !result.Any())
            return NoContent();

        return Ok(result);
    }

    private ClienteDto MapearDto(string? cpf, string? nome, string? email)
    {
        return new ClienteDto() { CPF = cpf, Email = email, Nome = nome, PrestadorId = PrestadorId, Telefone = string.Empty };
    }

    /// <summary>
    /// Recupera um cliente especifício
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetId(Guid id)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);


        var result = await _clienteService.FindByIdCliente(id);

        if (result == null)
            return NoContent();

        return Ok(result);
    }

    /// <summary>
    /// Atualiar um cliente
    /// </summary>
    /// <param name="cliente"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> AtualizarCliente(ClienteDto cliente)
    {

        var resultValitor = await _validator.ValidateAsync(cliente);

        if (resultValitor != null && !resultValitor.IsValid)
        {
            List<ErrosValidationsResponse> errors = new();

            foreach (var item in resultValitor.Errors)
                errors.Add(new ErrosValidationsResponse() { ErrorMensagem = item.ErrorMessage });

            return StatusCode(StatusCodes.Status400BadRequest, errors);
        }

        if (!ModelState.IsValid || !cliente.Id.HasValue)
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        TratarDto(cliente);

        MapearLogin(cliente);

        var result = await _clienteService.UpdateCliente(cliente);

        if (result == null)
            return NoContent();

        return Ok(result);
    }

    /// <summary>
    /// Desativa um cliente
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut("DesativarCliente")]
    public async Task<IActionResult> DesativarCliente(Guid id)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var result = await _clienteService.Desabled(id, PrestadorId);

        if (result == null)
            return NoContent();

        return Ok(result);
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
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            await _clienteService.Delete(id);
            return Ok("Deletado");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}