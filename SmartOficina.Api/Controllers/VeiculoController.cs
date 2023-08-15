namespace SmartOficina.Api.Controllers;

[Route("api/[controller]")]
[ApiController, Authorize]
public class VeiculoController : MainController
{
    private readonly IVeiculoRepository _repository;
    private readonly IMapper _mapper;

    public VeiculoController(IVeiculoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Add(VeiculoDto veiculo)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        MapearLogin(veiculo);

        var result = await _repository.Create(_mapper.Map<Veiculo>(veiculo));
        return Ok(_mapper.Map<VeiculoDto>(result));


    }

    private void MapearLogin(VeiculoDto veiculo)
    {
        if (!veiculo.PrestadorId.HasValue)
            veiculo.PrestadorId = PrestadorId;

        veiculo.UsrCadastroDesc = UserName;
        veiculo.UsrCadastro = UserId;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _repository.GetAll();
        return Ok(_mapper.Map<ICollection<VeiculoDto>>(result));

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
        return Ok(_mapper.Map<VeiculoDto>(result));

    }

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

        var result = await _repository.Update(_mapper.Map<Veiculo>(veiculo));
        return Ok(_mapper.Map<VeiculoDto>(result));

    }

    [HttpPut("DesativarVeiculo")]
    public async Task<IActionResult> DesativarVeiculo(Guid id)
    {
        if (!ModelState.IsValid || id == null)
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        var result = await _repository.Desabled(id);
        return Ok(_mapper.Map<VeiculoDto>(result));
    }

    [HttpDelete]
    public async Task<IActionResult> DeletarVeiculo(Guid id)
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
