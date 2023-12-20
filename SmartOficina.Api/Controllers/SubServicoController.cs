using System.Drawing;

namespace SmartOficina.Api.Controllers;

[Route("api/[controller]")]
[ApiController, Authorize]
public class SubServicoController : MainController
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

        MapearLogin(subServico);

        var result = await _repository.Create(_mapper.Map<SubCategoriaServico>(subServico));

        return Ok(_mapper.Map<SubCategoriaServicoDto>(result));
    }

    private void MapearLogin(SubCategoriaServicoDto subServico)
    {
        if (!subServico.PrestadorId.HasValue)
            subServico.PrestadorId = PrestadorId;

        subServico.UsrCadastroDesc = UserName;
        subServico.UsrCadastro = UserId;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(string? titulo, string? desc)
    {
        var result = await _repository.GetAll(PrestadorId, new SubCategoriaServico() { Titulo = titulo, Desc = desc});
        return Ok(_mapper.Map<ICollection<SubCategoriaServicoDto>>(result));

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
        return Ok(_mapper.Map<SubCategoriaServicoDto>(result));

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

        MapearLogin(subServico);

        var result = await _repository.Update(_mapper.Map<SubCategoriaServico>(subServico));
        return Ok(_mapper.Map<SubCategoriaServicoDto>(result));

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

        var result = await _repository.Desabled(id);
        return Ok(_mapper.Map<SubCategoriaServicoDto>(result));

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
            return Ok("Deletado");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
