﻿namespace SmartOficina.Api.Infrastructure.Configurations.Security;

public class IdentityInitializer
{
    private readonly OficinaContext _context;
    private readonly UserManager<UserAutentication> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public IdentityInitializer(OficinaContext context,
            UserManager<UserAutentication> userManager,
            RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public void Initialize()
    {
        if (_context.Database.EnsureCreated())
        {

            if (!_roleManager.RoleExistsAsync(Roles.Cliente).Result)
            {
                var resultado = _roleManager.CreateAsync(
                    new IdentityRole(Roles.Cliente)).Result;
                if (!resultado.Succeeded)
                {
                    throw new Exception(
                        $"Erro durante a criação da role {Roles.Cliente}.");
                }
            }

            if (!_roleManager.RoleExistsAsync(Roles.Funcionario).Result)
            {
                var resultado = _roleManager.CreateAsync(
                    new IdentityRole(Roles.Funcionario)).Result;
                if (!resultado.Succeeded)
                {
                    throw new Exception(
                        $"Erro durante a criação da role {Roles.Funcionario}.");
                }
            }

            CreateUser(
                new UserAutentication()
                {
                    UserName = "agendServer",
                    Email = "agendaAdministracao@i4us.com.br",
                    NormalizedEmail = "agendaAdministracao@i4us.com.br",
                    UsrCadastro = Guid.NewGuid(),
                    EmailConfirmed = true
                }, "@g3nd4P@s", Roles.Funcionario);

        }
    }

    private void CreateUser(
    UserAutentication user,
    string password,
        string role)
    {
        if (_userManager.FindByNameAsync(user.UserName).Result == null)
        {
            var resultado = _userManager
                .CreateAsync(user, password).Result;

            if (resultado.Succeeded)
                _userManager.AddToRoleAsync(user, role).Wait();
        }
    }
}