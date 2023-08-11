namespace SmartOficina.Api.Controllers;

[Route("api/[controller]")]
[ApiController, Authorize]
public class ProdutoController : MainController
{
    private readonly IMapper _mapper;
    private readonly IProdutoRepository _repository;

    public ProdutoController(IMapper mapper, IProdutoRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> Add(ProdutoDto produto)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }
        return Ok(await _repository.Create(_mapper.Map<Produto>(produto)));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _repository.GetAll());
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
    public async Task<IActionResult> AtualizarProduto(ProdutoDto produto)
    {
        if (!ModelState.IsValid || !produto.Id.HasValue)
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }
        return Ok(await _repository.Update(_mapper.Map<Produto>(produto)));
    }

    [HttpPut("DesativarPrestador")]
    public async Task<IActionResult> DesativarProduto(Guid id)
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
    public async Task<IActionResult> DeletarProduto(Guid id)
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
            return Ok("Deleted with success");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
