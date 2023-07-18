namespace SmartOficina.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubServicoController : ControllerBase
{
    private readonly ISubServicoRepository _repository;
    private readonly IMapper _mapper;

    public SubServicoController(ISubServicoRepository subServicoRepository, IMapper mapper)
    {
        _repository = subServicoRepository;
        _mapper = mapper;
    }
    [HttpPost]
    public async Task<IActionResult> AddAsync(SubServicoDto subServico)
    {
        return Ok(await _repository.Create(_mapper.Map<SubServico>(subServico)));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _repository.GetAllWithIncludes());
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetId(Guid id)
    {
        return Ok(await _repository.FindById(id));
    }

    [HttpPut]
    public async Task<IActionResult> AtualizarSubServico(SubServicoDto subServico)
    {
        return Ok(await _repository.Update(_mapper.Map<SubServico>(subServico)));
    }

    [HttpPut("DesativarSubServico")]
    public async Task<IActionResult> DesativarSubServico(Guid id)
    {
        return Ok(await _repository.Desabled(id));
    }

    [HttpDelete]
    public async Task<IActionResult> DeletarSubServico(Guid id)
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
