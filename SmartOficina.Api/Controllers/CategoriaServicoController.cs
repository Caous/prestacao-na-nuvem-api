namespace SmartOficina.Api.Controllers;

/// <summary>
/// Controller Categoria Serviço
/// </summary>
[Route("api/[controller]")]
[ApiController, Authorize]
[Produces("application/json")]
[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
public class CategoriaServicoController : MainController
{
    private readonly ICategoriaServicoRepository _repository;
    private readonly IMapper _mapper;

    public CategoriaServicoController(ICategoriaServicoRepository categoriaServicoRepository, IMapper mapper)
    {
        _repository = categoriaServicoRepository;
        _mapper = mapper;
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

        var result = await _repository.Create(_mapper.Map<CategoriaServico>(categoriaServico));

        return Ok(_mapper.Map<CategoriaServicoDto>(result));
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

        var result = await _repository.GetAll(PrestadorId, new CategoriaServico() { Desc = desc, Titulo = titulo, PrestadorId = PrestadorId });

        return Ok(_mapper.Map<ICollection<CategoriaServicoDto>>(result));
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
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return BadRequest(ModelState.First().Value);
        }

        var result = await _repository.FindById(id);

        return Ok(_mapper.Map<CategoriaServicoDto>(result));
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

        var result = await _repository.Update(_mapper.Map<CategoriaServico>(categoriaServico));

        return Ok(_mapper.Map<CategoriaServicoDto>(result));
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
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        var result = await _repository.Desabled(id);

        return Ok(_mapper.Map<CategoriaServicoDto>(result));
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
