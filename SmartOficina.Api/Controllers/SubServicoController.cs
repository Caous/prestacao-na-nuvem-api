using SmartOficina.Api.Domain.Interfaces;

namespace SmartOficina.Api.Controllers;

/// <summary>
/// Controller de sub serviço
/// </summary>
[Route("api/[controller]")]
[ApiController, Authorize]
[Produces("application/json")]
[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
public class SubServicoController : MainController
{
    private readonly ISubCategoriaServicoService _subCategoriaServicoService;
    public SubServicoController(ISubCategoriaServicoService subCategoriaServicoService)
    {
        _subCategoriaServicoService = subCategoriaServicoService;
    }

    /// <summary>
    /// Adicionar um Sub serviço
    /// </summary>
    /// <param name="subServico"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> AddAsync(SubCategoriaServicoDto subServico)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        MapearLogin(subServico);

        var result = await _subCategoriaServicoService.CreateSubCategoria(subServico);

        if (result == null)
            return NoContent();

        return Ok(result);
    }

    private void MapearLogin(SubCategoriaServicoDto subServico)
    {
        if (!subServico.PrestadorId.HasValue)
            subServico.PrestadorId = PrestadorId;

        subServico.UsrCadastroDesc = UserName;
        subServico.UsrCadastro = UserId;
    }

    /// <summary>
    /// Recupera todos os sub-serviços com filtros opcionais
    /// </summary>
    /// <param name="titulo"></param>
    /// <param name="desc"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll(string? titulo, string? desc)
    {
        SubCategoriaServicoDto filter = MapperFilter(titulo, desc);
        MapearLogin(filter);
        var result = await _subCategoriaServicoService.GetAllSubCategoria(filter);
        if (result == null || !result.Any())
            return NoContent();
        return Ok(result);

    }

    private static SubCategoriaServicoDto MapperFilter(string? titulo, string? desc)
    {
        return new SubCategoriaServicoDto() { Titulo = titulo, Desc = desc };
    }

    /// <summary>
    /// Recupera um sub-serviço por Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetId(Guid id)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var result = await _subCategoriaServicoService.FindByIdSubCategoria(id);
        if (result == null)
            return NoContent();
        return Ok(result);

    }

    /// <summary>
    /// Atualizar um sub-serviço
    /// </summary>
    /// <param name="subServico"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> AtualizarSubServico(SubCategoriaServicoDto subServico)
    {
        if (!ModelState.IsValid || !subServico.Id.HasValue)
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        MapearLogin(subServico);

        var result = await _subCategoriaServicoService.UpdateSubCategoria(subServico);

        if (result == null)
            return NoContent();

        return Ok(result);

    }

    /// <summary>
    /// Desativar um sub-serviço
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut("DesativarSubServico")]
    public async Task<IActionResult> DesativarSubServico(Guid id, Guid userDesabled)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);


        var result = await _subCategoriaServicoService.Desabled(id, userDesabled);

        if (result == null)
            return NoContent();

        return Ok(result);

    }

    /// <summary>
    /// Deletar um sub-serviço
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<IActionResult> DeletarSubServico(Guid id)
    {
        try
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            await _subCategoriaServicoService.Delete(id);
            return Ok("Deletado");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
