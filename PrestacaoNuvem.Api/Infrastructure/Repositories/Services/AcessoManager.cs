﻿using System.Security.Claims;
using System.Security.Principal;
using PrestacaoNuvem.Api.Infrastructure.Configurations.PrestadorConfiguration;

namespace PrestacaoNuvem.Api.Infrastructure.Repositories.Services;

public class AcessoManager : IAcessoManager
{
    private readonly UserManager<UserModel> _userManager;
    private readonly SignInManager<UserModel> _signInManager;
    private readonly TokenConfigurations _tokenConfigurations;
    private readonly SigningConfigurations _signingConfigurations;
    private readonly IOptions<PrestadorConfigurations> _prestadorConfigurations;

    private readonly IMapper _mapper;

    public AcessoManager(UserManager<UserModel> userManager,
            SignInManager<UserModel> signInManager,
            TokenConfigurations tokenConfigurations,
            SigningConfigurations signingConfigurations,
            IOptions<PrestadorConfigurations> prestadorConfigurations,
            IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenConfigurations = tokenConfigurations;
        _signingConfigurations = signingConfigurations;
        _mapper = mapper;
        _prestadorConfigurations = prestadorConfigurations;
    }
    public async Task<bool> CriarPrestador(PrestadorCadastroDto user)
    {
        var userModel = _mapper.Map<UserModel>(user);

        userModel.Id = Guid.NewGuid().ToString();

        var result = await _userManager.CreateAsync(userModel, user.Password);

        if (result.Succeeded)
        {
            var allowedRoles = new List<string>
            {
                Roles.Cliente,
                Roles.Fornecedor,
                Roles.Funcionario,
                Roles.Administrador
            };

            var roleToAssign = !string.IsNullOrEmpty(user.Role) && allowedRoles.Contains(user.Role)
                ? user.Role
                : Roles.Fornecedor;

            await _userManager.AddToRoleAsync(userModel, roleToAssign);

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

    public async Task<UserModel?> GetUserPorEmail(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<Token> ValidarCredenciais(UserModelDto user)
    {
        UserModel? usuario;

        if (!string.IsNullOrEmpty(user.UserName))
            usuario = await _userManager.FindByNameAsync(user.UserName.ToUpper());
        else
            usuario = await _userManager.FindByEmailAsync(user.Email.ToUpper());


        if (usuario != null)
        {

            var signResult = await _signInManager.CheckPasswordSignInAsync(usuario, user.Password, false);
            if (signResult.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(usuario);
                return GenerateToken(usuario, roles.ToArray());
            }
        }

        return new() { Authenticated = false, Message = "Invalid" };
    }

    private Token GenerateToken(UserModel user, string[] roles)
    {
        if (user != null && user.UserName == _prestadorConfigurations.Value.DefaultAdminName)
        {
            user.PrestadorId = _prestadorConfigurations.Value.DefaultPrestadorId;
        }
        Claim[] authClaims = new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Id!),
                        new Claim("PrestadorId", user.PrestadorId?.ToString() ?? ""),
                        new Claim("UserName", user.UserName?.ToString() ?? ""),
                        new Claim("FuncionarioId", user.FuncionarioId?.ToString() ?? ""),
                        new Claim("IdUserLogin", user.Id!),
                        new Claim("IsADM", user.UserName == _prestadorConfigurations.Value.DefaultAdminName ? "true" : "false"),
            };
        foreach (var role in roles)
        {
            authClaims = authClaims.Append(new Claim(ClaimTypes.Role, role)).ToArray();
        }
        ClaimsIdentity identity = new(
            new GenericIdentity(user.Id!, "Login"),
           authClaims
        );

        DateTime dataCriacao = DateTime.Now;
        DateTime dataExpiracao = dataCriacao +
            TimeSpan.FromDays(_tokenConfigurations.Days);


        var handler = new JwtSecurityTokenHandler();
        var securityToken = handler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _tokenConfigurations.Issuer,
            Audience = _tokenConfigurations.Audience,
            SigningCredentials = _signingConfigurations.SigningCredentials,
            Subject = identity,
            NotBefore = dataCriacao,
            Expires = dataExpiracao,

        });
        var token = handler.WriteToken(securityToken);

        return new()
        {
            Authenticated = true,
            Created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
            Expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
            AccessToken = token,
            Message = "OK",
            Roles = roles,
            UserName = user.UserName
        };
    }

    public async Task<UserModel?> GetUserPorId(Guid id)
    {
        return await _userManager.FindByIdAsync(id.ToString());
    }
}
