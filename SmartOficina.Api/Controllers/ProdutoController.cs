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

        MapearLogin(produto);

        var result = await _repository.Create(_mapper.Map<Produto>(produto));

        return Ok(_mapper.Map<ProdutoDto>(result));
    }

    private void MapearLogin(ProdutoDto produto)
    {
        if (!produto.PrestadorId.HasValue)
            produto.PrestadorId = PrestadorId;

        produto.UsrCadastroDesc = UserName;
        produto.UsrCadastro = UserId;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(string? marca, string? nome, string? modelo)
    {
        var result = await _repository.GetAll(PrestadorId, new Produto() { Marca = marca, Nome = nome, Modelo = modelo, PrestadorId = PrestadorId, Valor_Compra = 0, Valor_Venda = 0 });
        return Ok(_mapper.Map<ICollection<ProdutoDto>>(result));
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

        var result = await _repository.FindById(id);

        return Ok(_mapper.Map<ProdutoDto>(result));
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

        MapearLogin(produto);

        var result = await _repository.Update(_mapper.Map<Produto>(produto));

        return Ok(_mapper.Map<ProdutoDto>(result));
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

        var result = await _repository.Desabled(id);

        return Ok(_mapper.Map<ProdutoDto>(result));
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
            return Ok("Deletado");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
