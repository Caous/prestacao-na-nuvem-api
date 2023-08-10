namespace SmartOficina.Seguranca.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AutenticacaoController : ControllerBase
{
    private readonly IAcessoManager _acessoManager;
    private readonly IMapper _mapper;

    public AutenticacaoController(IAcessoManager acessoManager, IMapper mapper)
    {
        _acessoManager = acessoManager;
        _mapper = mapper;
    }

    [HttpGet("PrestadorUser")]
    public async Task<IActionResult> GetPrestadorUser(string email, Guid? id, string? CpfCnpj)
    {

        UserModel user = await _acessoManager.GetUserPorEmail(email);

        if (user != null)
            return Ok(user);
        else
            return BadRequest("User not found!");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(UserModelDto user)
    {
        return Ok(await _acessoManager.ValidarCredenciais(user));
    }

    [HttpPost("RegistrarFornecedor")]
    public async Task<IActionResult> Post(UserModelDto userDto)
    {
        if (await _acessoManager.CriarPrestador(userDto))
            return Ok();

        return BadRequest();
    }

    [HttpPost("LoginPrestador")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(PrestadorLoginDto prestador)
    {
        var token = await _acessoManager.ValidarCredenciais(_mapper.Map<UserModelDto>(prestador));
        if (token.Authenticated)
            return Ok(token);

        return Forbid("Não autenticado");

    }
}
