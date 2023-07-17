namespace SmartOficina.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriaServicoController : ControllerBase
{
    private readonly ICategoriaServicoRepository _repository;
    private readonly IMapper _mapper;

    public CategoriaServicoController(ICategoriaServicoRepository categoriaServicoRepository, IMapper mapper)
    {
        _repository = categoriaServicoRepository;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(CategoriaServicoDto categoriaServico)
    {
        return Ok(await _repository.Create(_mapper.Map<CategoriaServico>(categoriaServico)));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _repository.GetAll());
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetId(Guid id)
    {
        return Ok(await _repository.FindById(id));
    }

    [HttpPut]
    public async Task<IActionResult> AtualizarCategoria(CategoriaServicoDto categoriaServico)
    {
        return Ok(await _repository.Update(_mapper.Map<CategoriaServico>(categoriaServico)));
    }

    [HttpPut("DesativarCategoria")]
    public async Task<IActionResult> DesativarCategoria(Guid id)
    {
        return Ok(await _repository.Desabled(id));
    }

    [HttpDelete]
    public async Task<IActionResult> DeletarCategoria(Guid id)
    {
        try
        {
            await _repository.Delete(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
