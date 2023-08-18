namespace SmartOficina.Api.Controllers;

[Route("api/[controller]")]
[ApiController, Authorize]
public class PrestacaoServicoController : MainController
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

        if (!prestacaoServico.PrestadorId.HasValue)
            prestacaoServico.PrestadorId = PrestadorId;

        if (prestacaoServico.Produtos != null)
        {
            foreach (var item in prestacaoServico.Produtos)
            {
                item.PrestadorId = prestacaoServico.PrestadorId.Value; 
                item.UsrCadastro = UserId;
                item.UsrCadastroDesc = UserName;
            }
        }

        if (prestacaoServico.Veiculo != null)
        {
            prestacaoServico.Veiculo.PrestadorId = prestacaoServico.PrestadorId.Value;
            prestacaoServico.UsrCadastro = UserId;
            prestacaoServico.UsrCadastroDesc = UserName;
        }

        if (prestacaoServico.Cliente != null)
        {
            prestacaoServico.Cliente.PrestadorId = prestacaoServico.PrestadorId.Value;
            prestacaoServico.UsrCadastro = UserId;
            prestacaoServico.UsrCadastroDesc = UserName;
        }

        if (prestacaoServico.Servicos != null)
        {
            foreach (var item in prestacaoServico.Servicos)
            {
                item.PrestadorId = prestacaoServico.PrestadorId.Value; 
                item.UsrCadastro = UserId;
                item.UsrCadastroDesc = UserName;
            }
        }


        prestacaoServico.UsrCadastroDesc = UserName;
        prestacaoServico.UsrCadastro = UserId;


        var produtos = new List<ProdutoDto>();

        if (prestacaoServico.Produtos != null)
        {
            foreach (var prod in prestacaoServico.Produtos)
            {
                for (int i = 0; i < prod.Qtd; i++)
                {
                    produtos.Add(prod);
                }
            }
        }

        prestacaoServico.Produtos = produtos;

        var result = await _repository.Create(_mapper.Map<PrestacaoServico>(prestacaoServico));

        return Ok(_mapper.Map<PrestacaoServicoDto>(result));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _repository.GetAll();

        return Ok(_mapper.Map<ICollection<PrestacaoServicoDto>>(result));
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

        return Ok(_mapper.Map<PrestacaoServicoDto>(result));
    }

    [HttpGet("PrestacaoServicoFechadosPrestador")]
    public async Task<IActionResult> GetByPrestacaoServicoFechadosPrestador()
    {
        if (!ModelState.IsValid)
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        List<EPrestacaoServicoStatus> status = new List<EPrestacaoServicoStatus>() { EPrestacaoServicoStatus.Concluido, EPrestacaoServicoStatus.Rejeitado };
        return Ok(_mapper.Map<ICollection<PrestacaoServicoDto>>(await _repository.GetByPrestacoesServicosStatus(PrestadorId, status)));
    }

    [HttpGet("PrestacaoServicoAbertoPrestador")]
    public async Task<IActionResult> GetByPrestacaoServicoAbertosPrestador()
    {
        if (!ModelState.IsValid)
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        List<EPrestacaoServicoStatus> status = new List<EPrestacaoServicoStatus>() { EPrestacaoServicoStatus.Aberto, EPrestacaoServicoStatus.Analise, EPrestacaoServicoStatus.Andamento, EPrestacaoServicoStatus.Aprovado, EPrestacaoServicoStatus.Teste};
        return Ok(_mapper.Map<ICollection<PrestacaoServicoDto>>(await _repository.GetByPrestacoesServicosStatus(PrestadorId, status)));
    }

    [HttpGet("PrestacaoServicoEnriquecidoPrestador")]
    public async Task<IActionResult> GetByPrestacaoServicoEnriquecidoPrestador()
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        return Ok(_mapper.Map<ICollection<PrestacaoServicoDto>>(await _repository.GetByPrestador(PrestadorId)));
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

        if (!prestacaoServico.PrestadorId.HasValue)
            prestacaoServico.PrestadorId = PrestadorId;

        if (prestacaoServico.Produtos != null)
        {
            foreach (var item in prestacaoServico.Produtos)
            {
                item.PrestadorId = prestacaoServico.PrestadorId.Value;
                item.UsrCadastro = UserId;
                item.UsrCadastroDesc = UserName;
            }
        }

        if (prestacaoServico.Servicos != null)
        {

            foreach (var item in prestacaoServico.Servicos)
            {
                item.PrestadorId = prestacaoServico.PrestadorId.Value;
                item.UsrCadastro = UserId;
                item.UsrCadastroDesc = UserName;
            }
        }

        if (prestacaoServico.Veiculo != null)
            prestacaoServico.Veiculo.PrestadorId = prestacaoServico.PrestadorId.Value;

        if (prestacaoServico.Cliente != null)
            prestacaoServico.Cliente.PrestadorId = prestacaoServico.PrestadorId.Value;


        var produtos = new List<ProdutoDto>();

        if (prestacaoServico.Produtos != null)
        {
            foreach (var prod in prestacaoServico.Produtos)
            {
                if (prod.Qtd == 0)
                    produtos.Add(prod);

                for (int i = 0; i < prod.Qtd; i++)
                {
                    produtos.Add(prod);
                }
            }
        }

        prestacaoServico.Produtos = produtos;

        var result = await _repository.Update(_mapper.Map<PrestacaoServico>(prestacaoServico));

        return Ok(_mapper.Map<PrestacaoServicoDto>(result));
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
        var result = await _repository.Desabled(id);
        return Ok(_mapper.Map<PrestacaoServicoDto>(result));
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
            return Ok("Deletado");
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
