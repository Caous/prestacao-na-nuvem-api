namespace SmartOficina.Api.Controllers;

[Route("api/[controller]")]
[ApiController, Authorize]
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
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        return Ok(await _repository.Create(_mapper.Map<CategoriaServico>(categoriaServico)));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        //var xpto = User.Claims.FirstOrDefault(x=> x.Type == "PrestadorId").Value;
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }
        return Ok(await _repository.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetId(Guid id)
    {
        if (!ModelState.IsValid || id ==  null)
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return BadRequest(ModelState.First().Value);
        }
        return Ok(await _repository.FindById(id));
    }

    [HttpPut]
    public async Task<IActionResult> AtualizarCategoria(CategoriaServicoDto categoriaServico)
    {

        if (!ModelState.IsValid || !categoriaServico.Id.HasValue)
        {
            if(ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return BadRequest(ModelState.First().Value);
        }
        return Ok(await _repository.Update(_mapper.Map<CategoriaServico>(categoriaServico)));
    }

    [HttpPut("DesativarCategoria")]
    public async Task<IActionResult> DesativarCategoria(Guid id)
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
    public async Task<IActionResult> DeletarCategoria(Guid id)
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
