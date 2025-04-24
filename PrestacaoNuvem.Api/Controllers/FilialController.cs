using PrestacaoNuvem.Api.Domain.Model;

namespace PrestacaoNuvem.Api.Controllers;

[Route("api/[controller]")]
[ApiController, Authorize]
[Produces("application/json")]
[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
public class FilialController : MainController
{
    private readonly IFilialService _filialService;

    public FilialController(IFilialService filialService)
    {
        _filialService = filialService;
    }
    /// <summary>
    /// Adicionar uma filial
    /// </summary>
    /// <param name="filial"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> AddAsync(FilialDto request)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        MapearLogin(request);

        var result = await _filialService.CreateFilial(request);

        if (result == null)
            return NoContent();

        return Ok(result);
    }

    private void MapearLogin(FilialDto request)
    {
        if (!request.PrestadorId.HasValue)
            request.PrestadorId = PrestadorId;

        request.UsrCadastroDesc = UserName;
        request.UsrCadastro = UserId;
    }

    /// <summary>
    /// Recupera todos as filiais do prestador e também filtra
    /// </summary>
    /// <param name="cpf"></param>
    /// <param name="nome"></param>
    /// <param name="email"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll(string? logradouro, string? nome)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        FilialDto request = MapearDto(logradouro, nome);

        var result = await _filialService.GetAllFilial(request, IsAdminLogged);

        if (result == null || !result.Any())
            return NoContent();

        return Ok(result);
    }

    private FilialDto MapearDto(string? logradouro, string? nome)
    {
        return new FilialDto() { Logradouro = logradouro, Nome = nome, PrestadorId = PrestadorId };
    }

    /// <summary>
    /// Recupera uma filial especifício
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetId(Guid id)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);


        var result = await _filialService.FindByIdFilial(id);

        if (result == null)
            return NoContent();

        return Ok(result);
    }

    /// <summary>
    /// Atualiar uma filial
    /// </summary>
    /// <param name="filial"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> AtualizarFilial(FilialDto request)
    {
        if (!ModelState.IsValid || !request.Id.HasValue)
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        MapearLogin(request);

        var result = await _filialService.UpdateFilial(request);

        if (result == null)
            return NoContent();

        return Ok(result);
    }

    /// <summary>
    /// Desativa uma filial
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut("DesativarFilial")]
    public async Task<IActionResult> DesativarFilial(Guid id)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var result = await _filialService.Desabled(id, PrestadorId);

        if (result == null)
            return NoContent();

        return Ok(result);
    }


    /// <summary>
    /// Deletar uma filial
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<IActionResult> DeletarFilial(Guid id)
    {
        try
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            await _filialService.Delete(id);
            return Ok("Deletado");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
