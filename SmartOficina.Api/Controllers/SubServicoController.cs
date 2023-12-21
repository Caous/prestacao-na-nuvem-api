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
[ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
public class SubServicoController : MainController
{
    private readonly ISubServicoRepository _repository;
    private readonly IMapper _mapper;

    public SubServicoController(ISubServicoRepository subServicoRepository, IMapper mapper)
    {
        _repository = subServicoRepository;
        _mapper = mapper;
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

        var result = await _repository.Create(_mapper.Map<SubCategoriaServico>(subServico));

        if (result == null)
            NoContent();

        return Ok(_mapper.Map<SubCategoriaServicoDto>(result));
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
        var result = await _repository.GetAll(PrestadorId, new SubCategoriaServico() { Titulo = titulo, Desc = desc });
        if (result == null || !result.Any())
            NoContent();
        return Ok(_mapper.Map<ICollection<SubCategoriaServicoDto>>(result));

    }

    /// <summary>
    /// Recupera um sub-serviço por Id
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
        return Ok(_mapper.Map<SubCategoriaServicoDto>(result));

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

        var result = await _repository.Update(_mapper.Map<SubCategoriaServico>(subServico));
        if (result == null)
            NoContent();
        return Ok(_mapper.Map<SubCategoriaServicoDto>(result));

    }

    /// <summary>
    /// Desativar um sub-serviço
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut("DesativarSubServico")]
    public async Task<IActionResult> DesativarSubServico(Guid id)
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

        return Ok(_mapper.Map<SubCategoriaServicoDto>(result));

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
