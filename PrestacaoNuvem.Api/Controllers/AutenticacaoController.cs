namespace PrestacaoNuvem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AutenticacaoController : MainController
{
    private readonly IAcessoManager _acessoManager;
    private readonly IMapper _mapper;

    public AutenticacaoController(IAcessoManager acessoManager, IMapper mapper)
    {
        _acessoManager = acessoManager;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet("BuscarPrestador")]
    public async Task<IActionResult> GetPrestadorUser(string? email, Guid? id)
    {
        if (!string.IsNullOrEmpty(email))
        {
            UserModel? user = await _acessoManager.GetUserPorEmail(email);
            if (user != null)
                return Ok(user);
        }
        else
        {
            UserModel? user = await _acessoManager.GetUserPorId(id.Value);
            if (user != null)
            {
                return Ok(user);
            }
        }
        return BadRequest("User not found!");
    }

    [Authorize]
    [HttpPost("RegistrarPrestador")]
    public async Task<IActionResult> Post(PrestadorCadastroDto prestadorDto)
    {
        if (prestadorDto.UsrCadastro == Guid.Empty || string.IsNullOrEmpty(prestadorDto.UsrCadastroDesc))
        {
            UserModel? user = await _acessoManager.GetUserPorEmail(prestadorDto.Email);

            if (user != null)
            {
                prestadorDto.UsrCadastroDesc = user.UserName ?? string.Empty;
                prestadorDto.UsrCadastro = new Guid(user.Id);
            }
        }

        if (await _acessoManager.CriarPrestador(prestadorDto))
            return Ok();

        return BadRequest();
    }

    [AllowAnonymous]
    [HttpPost("LoginPrestador")]
    public async Task<IActionResult> LoginPrestador(PrestadorLoginDto prestador)
    {
        if (string.IsNullOrEmpty(prestador.Email) && string.IsNullOrEmpty(prestador.UserName))
            return BadRequest($"{PrestadorConst.PrestadorEmailVazio} ou {PrestadorConst.PrestadorNomeUsarioVazio}");

        var token = await _acessoManager.ValidarCredenciais(_mapper.Map<UserModelDto>(prestador));

        if (token.Authenticated)
            return Ok(token);

        return Forbid("Não autenticado");

    }
}
