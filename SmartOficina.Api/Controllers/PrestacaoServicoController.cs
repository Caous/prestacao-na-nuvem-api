namespace SmartOficina.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrestacaoServicoController : ControllerBase
{
    private readonly IPrestacaoServicoRepository _repository;
    private readonly IMapper _mapper;

    public PrestacaoServicoController(IPrestacaoServicoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Add(PrestacaoServicoDto prestacaoServico)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        if (prestacaoServico.Produtos != null)
        {
            foreach (var item in prestacaoServico.Produtos)
            {
                item.PrestadorId = prestacaoServico.PrestadorId.Value;
            }
        }
        if (prestacaoServico.Veiculo != null)
            prestacaoServico.Veiculo.PrestadorId = prestacaoServico.PrestadorId.Value;

        if (prestacaoServico.Cliente != null)
            prestacaoServico.Cliente.PrestadorId = prestacaoServico.PrestadorId.Value;

        if (prestacaoServico.Servicos != null)
        {

            foreach (var item in prestacaoServico.Servicos)
            {
                item.PrestadorId = prestacaoServico.PrestadorId.Value;
            }
        }




        return Ok(await _repository.Create(_mapper.Map<PrestacaoServico>(prestacaoServico)));
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

    [HttpGet("PrestacaoServicoEnriquecidoPrestador/{id}")]
    public async Task<IActionResult> GetByPrestacaoServicoEnriquecidoPrestador(Guid id)
    {
        if (!ModelState.IsValid || id == null)
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }
        return Ok(_mapper.Map<ICollection<PrestacaoServicoDto>>(await _repository.GetByPrestador(id)));
    }

    [HttpPut]
    public async Task<IActionResult> AtualizarPrestacaoServico(PrestacaoServicoDto prestacaoServico)
    {
        if (!ModelState.IsValid || !prestacaoServico.Id.HasValue)
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        if (prestacaoServico.Produtos != null)
        {
            foreach (var item in prestacaoServico.Produtos)
            {
                item.PrestadorId = prestacaoServico.PrestadorId.Value;
            }
        }
        prestacaoServico.Veiculo.PrestadorId = prestacaoServico.PrestadorId.Value;

        if (prestacaoServico.Servicos != null)
        {

            foreach (var item in prestacaoServico.Servicos)
            {
                item.PrestadorId = prestacaoServico.PrestadorId.Value;
            }
        }


        prestacaoServico.Cliente.PrestadorId = prestacaoServico.PrestadorId.Value;



        return Ok(await _repository.Update(_mapper.Map<PrestacaoServico>(prestacaoServico)));
    }

    [HttpPut("DesativarPrestacao")]
    public async Task<IActionResult> DesativarPrestadorServico(Guid id)
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
    public async Task<IActionResult> DeletarPrestador(Guid id)
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

    [HttpPut("status/{id}/{status}")]
    public async Task<IActionResult> ChangeStatus(Guid id, EPrestacaoServicoStatus status)
    {
        if (!ModelState.IsValid || id == null)
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }
        await _repository.ChangeStatus(id, status);
        return Ok();
    }

}
