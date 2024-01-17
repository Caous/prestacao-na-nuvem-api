using SmartOficina.Api.Domain.Interfaces;

namespace SmartOficina.Api.Controllers;

/// <summary>
/// Controller de veículo
/// </summary>
[Route("api/[controller]")]
[ApiController, Authorize]
[Produces("application/json")]
[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
public class VeiculoController : MainController
{
    private readonly IVeiculoService _veiculoService;

    public VeiculoController(IVeiculoService veiculoService)
    {
        _veiculoService = veiculoService;
    }

    /// <summary>
    /// Adicionar um veículo
    /// </summary>
    /// <param name="veiculo"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Add(VeiculoDto veiculo)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        MapearLogin(veiculo);

        var result = await _veiculoService.CreateVeiculos(veiculo);
        if (result == null)
            return NoContent();
        return Ok(result);
    }

    private void MapearLogin(VeiculoDto veiculo)
    {
        if (!veiculo.PrestadorId.HasValue)
            veiculo.PrestadorId = PrestadorId;

        veiculo.UsrCadastroDesc = UserName;
        veiculo.UsrCadastro = UserId;
    }

    /// <summary>
    /// Recuperar todos os veículos
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        VeiculoDto filter = MapperFilter();

        var result = await _veiculoService.GetAllVeiculos(filter);
        if (result == null || !result.Any())
            return NoContent();
        return Ok(result);

    }

    private static VeiculoDto MapperFilter()
    {
        return new VeiculoDto() { Marca = string.Empty, Modelo = string.Empty, Placa = string.Empty };
    }

    /// <summary>
    /// Recuperar um veículo por Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetId(Guid id)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var result = await _veiculoService.FindByIdVeiculos(id);
        if (result == null)
            return NoContent();
        return Ok(result);

    }

    /// <summary>
    /// Atualizar um veículo
    /// </summary>
    /// <param name="veiculo"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> AtualizarVeiculo(VeiculoDto veiculo)
    {
        if (!ModelState.IsValid || !veiculo.Id.HasValue)
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        MapearLogin(veiculo);

        var result = await _veiculoService.UpdateVeiculos(veiculo);
        if (result == null)
            return NoContent();
        return Ok(result);

    }

    /// <summary>
    /// Desativar um veículo
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut("DesativarVeiculo")]
    public async Task<IActionResult> DesativarVeiculo(Guid id, Guid userDesabled)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);


        var result = await _veiculoService.Desabled(id, userDesabled);
        if (result == null)
            return NoContent();
        return Ok(result);
    }

    /// <summary>
    /// Deletar um veículo
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<IActionResult> DeletarVeiculo(Guid id)
    {
        try
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            await _veiculoService.Delete(id);
            return Ok("Deletado");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
