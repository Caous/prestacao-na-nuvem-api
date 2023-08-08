namespace SmartOficina.Seguranca.Controllers;

[ApiVersion("1.0")]
[Route("/v{version:apiVersion}/api/[controller]")]
[ApiController]
public class AutenticacaoController : ControllerBase
{
    private readonly IAcessoManager _acessoManager;

    public AutenticacaoController(IAcessoManager acessoManager)
    {
        _acessoManager = acessoManager;
    }

    [HttpGet("PrestadorUser")]
    public async Task<IActionResult> GetPrestadorUser(string email, Guid? id, string? CpfCnpj)
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok();
    }

    [HttpPost("RegistrarFornecedor")]
    public async Task<IActionResult> Post(UserModelDto userDto)
    {
        return Ok(userDto);
    }

    [HttpPost("LoginPrestador")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(UserModelDto user)
    {
        var token = await _acessoManager.ValidarCredenciais(user);
        if (token.Authenticated)
            return Ok(token);

        return Forbid("Não autenticado");

    }
}
