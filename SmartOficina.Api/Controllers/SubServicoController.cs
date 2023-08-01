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
    public async Task<IActionResult> AddAsync(SubCategoriaServicoDto subServico)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }
        return Ok(await _repository.Create(_mapper.Map<SubCategoriaServico>(subServico)));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _repository.GetAllWithIncludes());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetId(Guid id)
    {
        if (!ModelState.IsValid || id == null)
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }
        return Ok(await _repository.FindById(id));
    }

    [HttpPut]
    public async Task<IActionResult> AtualizarSubServico(SubCategoriaServicoDto subServico)
    {
        if (!ModelState.IsValid || !subServico.Id.HasValue)
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }
        return Ok(await _repository.Update(_mapper.Map<SubCategoriaServico>(subServico)));
    }

    [HttpPut("DesativarSubServico")]
    public async Task<IActionResult> DesativarSubServico(Guid id)
    {
        if (!ModelState.IsValid || id == null)
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }
        return Ok(await _repository.Desabled(id));
    }

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
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
