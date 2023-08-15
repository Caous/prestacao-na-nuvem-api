using SmartOficina.Api.Domain.Model;

namespace SmartOficina.Api.Controllers;

[Route("api/[controller]")]
[ApiController, Authorize]
public class CategoriaServicoController : MainController
{
    private readonly ICategoriaServicoRepository _repository;
    private readonly IMapper _mapper;

    public CategoriaServicoController(ICategoriaServicoRepository categoriaServicoRepository, IMapper mapper)
    {
        _repository = categoriaServicoRepository;
        _mapper = mapper;
    }

    private void MapearLogin(CategoriaServicoDto categoriaServico)
    {
        if (!categoriaServico.PrestadorId.HasValue)
            categoriaServico.PrestadorId = PrestadorId;

        categoriaServico.UsrCadastroDesc = UserName;
        categoriaServico.UsrCadastro = UserId;

    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(CategoriaServicoDto categoriaServico)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        MapearLogin(categoriaServico);

        var result = await _repository.Create(_mapper.Map<CategoriaServico>(categoriaServico));

        return Ok(_mapper.Map<CategoriaServicoDto>(result));
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        var result = await _repository.GetAll();

        return Ok(_mapper.Map<ICollection<CategoriaServicoDto>>(result));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetId(Guid id)
    {
        if (!ModelState.IsValid || id == null)
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return BadRequest(ModelState.First().Value);
        }

        var result = await _repository.FindById(id);

        return Ok(_mapper.Map<CategoriaServicoDto>(result));
    }

    [HttpPut]
    public async Task<IActionResult> AtualizarCategoria(CategoriaServicoDto categoriaServico)
    {

        if (!ModelState.IsValid || !categoriaServico.Id.HasValue)
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return BadRequest(ModelState.First().Value);
        }

        MapearLogin(categoriaServico);

        var result = await _repository.Update(_mapper.Map<CategoriaServico>(categoriaServico));

        return Ok(_mapper.Map<CategoriaServicoDto>(result));
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
                
        var result = await _repository.Desabled(id);

        return Ok(_mapper.Map<CategoriaServicoDto>(result));
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
            return Ok("Deletado");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
