namespace PrestacaoNuvem.Api.Controllers;

/// <summary>
/// Controller de produto
/// </summary>
[Route("api/[controller]")]
[ApiController, Authorize]
[Produces("application/json")]
[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
public class ProdutoController : MainController
{
    private readonly IProdutoService _produtoService;

    public ProdutoController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    /// <summary>
    /// Adicionar um produto
    /// </summary>
    /// <param name="produto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Add(ProdutoDto produto)
    {
        if (!ModelState.IsValid)        
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        

        MapearLogin(produto);

        var result = await _produtoService.CreateProduto(produto);

        if (result == null)
            return NoContent();

        return Ok(result);
    }

    [HttpPost("AddFromExcel")]
    public async Task<IActionResult> AddFromExcel(IFormFile file)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        if (file == null || file.Length == 0)
            return BadRequest("Arquivo não enviado");

        var produtosLot = await _produtoService.MapperProdutoLot(file);

        if (produtosLot == null)
            return BadRequest("Não foi possível mappear os produtos, verificar se existe produto no formato incorreto do exemplo");

        foreach (var produto in produtosLot)
            MapearLogin(produto);

        var result = await _produtoService.CreateProdutoLot(produtosLot);

        if (result == null)
            return NoContent();

        return Ok(result);

    }

    private void MapearLogin(ProdutoDto produto)
    {
        if (!produto.PrestadorId.HasValue)
            produto.PrestadorId = PrestadorId;

        produto.UsrCadastroDesc = UserName;
        produto.UsrCadastro = UserId;
    }

    /// <summary>
    /// Recuperar todos os produtos com parametros opcionais
    /// </summary>
    /// <param name="marca"></param>
    /// <param name="nome"></param>
    /// <param name="modelo"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll(string? marca, string? nome, string? modelo)
    {
        ProdutoDto filter = MapperFilter(marca, nome, modelo);

        var result = await _produtoService.GetAllProduto(filter);

        if (result == null || !result.Any())
            return NoContent();

        return Ok(result);
    }

    private ProdutoDto MapperFilter(string? marca, string? nome, string? modelo)
    {
        return new ProdutoDto() { Marca = marca, Nome = nome, Modelo = modelo, PrestadorId = PrestadorId, Valor_Compra = 0, Valor_Venda = 0 };
    }

    /// <summary>
    /// Recuperar um produto por Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetId(Guid id)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var result = await _produtoService.FindByIdProduto(id);

        if (result == null)
            return NoContent();

        return Ok(result);
    }

    /// <summary>
    /// Atualizar produto
    /// </summary>
    /// <param name="produto"></param>
    /// <returns></returns>
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

        var result = await _produtoService.UpdateProduto(produto);
        if (result == null)
            return NoContent();
        return Ok(result);
    }

    /// <summary>
    /// Desativar do produto
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut("DesativarProduto")]
    public async Task<IActionResult> DesativarProduto(Guid id)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);


        var result = await _produtoService.Desabled(id, PrestadorId);

        if (result == null)
            return NoContent();

        return Ok(result);
    }

    /// <summary>
    /// Deletar um produto
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<IActionResult> DeletarProduto(Guid id)
    {
        try
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            await _produtoService.Delete(id);
            return Ok("Deletado");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
