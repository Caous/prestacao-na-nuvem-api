﻿namespace SmartOficina.Seguranca.Infrastructure.Repositories.Services;

public class AcessoManager : IAcessoManager
{
    private readonly UserManager<UserModel> _userManager;
    private readonly SignInManager<UserModel> _signInManager;
    private readonly TokenConfigurations _tokenConfigurations;
    private readonly SigningConfigurations _signingConfigurations;
    private readonly IMapper _mapper;

    public AcessoManager(UserManager<UserModel> userManager,
            SignInManager<UserModel> signInManager,
            TokenConfigurations tokenConfigurations,
            SigningConfigurations signingConfigurations,
            IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenConfigurations = tokenConfigurations;
        _signingConfigurations = signingConfigurations;
        _mapper = mapper;
    }
    public async Task<bool> CriarFornecedor(UserModelDto user)
    {
        if (string.IsNullOrEmpty(user.UsrCadastroDesc))
            user.UsrCadastroDesc = "system";

        var userModel = _mapper.Map<UserModel>(user);
        var result = await _userManager.CreateAsync(userModel, user.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(userModel, Roles.Cliente);
            return true;
        }
        else return false;
    }

    public async Task<bool> CriarFuncionario(UserModelDto user)
    {
        if (string.IsNullOrEmpty(user.UsrCadastroDesc))
            user.UsrCadastroDesc = "system";

        var userModel = _mapper.Map<UserModel>(user);
        var result = await _userManager.CreateAsync(
           userModel, user.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRolesAsync(userModel, new string[] { Roles.Cliente, Roles.Funcionario });
            return true;
        }
        else return false;
    }

    public async Task<UserModel> GetUserPorEmail(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<Token> ValidarCredenciais(UserModelDto user)
    {
        UserModel usuario;

        if (!string.IsNullOrEmpty(user.UserName))
            usuario = await _userManager.FindByNameAsync(user.UserName);
        else
            usuario = await _userManager.FindByEmailAsync(user.Email);


        if (usuario != null)
        {

            var signResult = await _signInManager.CheckPasswordSignInAsync(usuario, user.Password, false);
            if (signResult.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(usuario);
                return GenerateToken(usuario, roles.First());
            }
        }

        return new() { Authenticated = false, Message = "Invalid" };
    }

    private Token GenerateToken(UserModel user, string role)
    {
        ClaimsIdentity identity = new(
            new GenericIdentity(user.Id!, "Login"),
            new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Id!),
                        new Claim(ClaimTypes.Role, role),
            }
        );

        DateTime dataCriacao = DateTime.Now;
        DateTime dataExpiracao = dataCriacao +
            TimeSpan.FromMinutes(_tokenConfigurations.Minutes);


        var handler = new JwtSecurityTokenHandler();
        var securityToken = handler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _tokenConfigurations.Issuer,
            Audience = _tokenConfigurations.Audience,
            SigningCredentials = _signingConfigurations.SigningCredentials,
            Subject = identity,
            NotBefore = dataCriacao,
            Expires = dataExpiracao
        });
        var token = handler.WriteToken(securityToken);

        return new()
        {
            Authenticated = true,
            Created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
            Expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
            AccessToken = token,
            Message = "OK"
        };
    }
}