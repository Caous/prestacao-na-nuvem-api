namespace PrestacaoNuvem.Api.Controllers;

/// <summary>
/// Controller Categoria Serviço
/// </summary>
[Route("api/[controller]")]
[ApiController, Authorize]
[Produces("application/json")]
[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
public class CategoriaServicoController : MainController
{
    private readonly ICategoriaService _categoriaService;

    public CategoriaServicoController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;

    }

    private void MapearLogin(CategoriaServicoDto categoriaServico)
    {
        if (!categoriaServico.PrestadorId.HasValue)
            categoriaServico.PrestadorId = PrestadorId;

        categoriaServico.UsrCadastroDesc = UserName;
        categoriaServico.UsrCadastro = UserId;

    }

    /// <summary>
    /// Adicionar categoria de serviço
    /// </summary>
    /// <param name="categoriaServico"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> AddAsync(CategoriaServicoDto categoriaServico)
    {

        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        MapearLogin(categoriaServico);

        var result = await _categoriaService.CreateCategoria(categoriaServico);

        if (result == null)
            return NoContent();

        return Ok(result);
    }

    /// <summary>
    /// Recupera todos as Categorias de Serviços
    /// </summary>
    /// <param name="titulo"></param>
    /// <param name="desc"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll(string? titulo, string? desc)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        CategoriaServicoDto filter = MapperFilter(titulo, desc);

        var result = await _categoriaService.GetAllCategoria(filter);

        if (result == null || !result.Any())
            return NoContent();

        return Ok(result);
    }

    private CategoriaServicoDto MapperFilter(string titulo, string desc)
    {
        return new CategoriaServicoDto() { Desc = desc, Titulo = titulo, PrestadorId = PrestadorId };
    }

    /// <summary>
    /// Recuperar Categoria por ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetId(Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.First().Value);

        var result = await _categoriaService.FindByIdCategoria(id);
        if (result == null)
            return NoContent();

        return Ok(result);
    }

    /// <summary>
    /// Atualizar Categoria
    /// </summary>
    /// <param name="categoriaServico"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> AtualizarCategoria(CategoriaServicoDto categoriaServico)
    {

        if (!ModelState.IsValid || !categoriaServico.Id.HasValue)
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return BadRequest(ModelState.First().Value);
        }

        MapearLogin(categoriaServico);

        var result = await _categoriaService.UpdateCategoria(categoriaServico);

        if (result == null)
            return NoContent();

        return Ok(result);
    }

    /// <summary>
    /// Desativar uma categoria
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut("DesativarCategoria")]
    public async Task<IActionResult> DesativarCategoria(Guid id)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);


        var result = await _categoriaService.Desabled(id, PrestadorId);

        if (result == null)
            return NoContent();
        return Ok(result);
    }

    /// <summary>
    /// Deletar uma categoria
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<IActionResult> DeletarCategoria(Guid id)
    {
        try
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            await _categoriaService.Delete(id);
            return Ok("Deletado");
        }
        catch (Exception ex)
        {
            if (ex.Message == "Indice não encontrado")
                return NoContent();

            return BadRequest(ex.Message);
        }
    }
}
