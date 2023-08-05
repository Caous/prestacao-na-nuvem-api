namespace SmartOficina.Api.Infrastructure.Configurations.Security;

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

            if (!_roleManager.RoleExistsAsync(Roles.Fornecedor).Result)
            {
                var resultado = _roleManager.CreateAsync(
                    new IdentityRole(Roles.Fornecedor)).Result;
                if (!resultado.Succeeded)
                {
                    throw new Exception(
                        $"Erro durante a criação da role {Roles.Funcionario}.");
                }
            }

            if (!_roleManager.RoleExistsAsync(Roles.Administrador).Result)
            {
                var resultado = _roleManager.CreateAsync(
                    new IdentityRole(Roles.Administrador)).Result;
                if (!resultado.Succeeded)
                {
                    throw new Exception(
                        $"Erro durante a criação da role {Roles.Funcionario}.");
                }
            }

            CreateUser(
                new UserAutentication()
                {
                    UserName = "OficinaNaNuvemAdm",
                    Email = "OficinaNaNuvemAdm@i4us.com.br",
                    NormalizedEmail = "OficinaNaNuvemAdm@i4us.com.br",
                    UsrCadastro = Guid.NewGuid(),
                    UsrDescricaoCadastro = "Usuário adm",
                    EmailConfirmed = true
                }, "@fic4N4N@v3m", Roles.Administrador);

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
