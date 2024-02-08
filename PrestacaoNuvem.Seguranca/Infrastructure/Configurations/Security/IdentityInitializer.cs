namespace PrestacaoNuvem.Seguranca.Infrastructure.Configurations.Security;

public class IdentityInitializer
{
    private readonly SegurancaContext _context;
    private readonly UserManager<UserModel> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public IdentityInitializer(SegurancaContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<UserModel> userManager,
            IConfiguration configuration)
    {
        _context = context;
        _roleManager = roleManager;
        _userManager = userManager;
        _configuration = configuration;
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

            string userName = _configuration["Admin:UserName"] ?? string.Empty;

            if (_userManager.FindByNameAsync(userName).Result == null)
            {
                string email = _configuration["Admin:Email"] ?? string.Empty;
                string password = _configuration["Admin:Password"] ?? string.Empty;
                var model = new UserModel
                {
                    UserName = userName,
                    Email = email,
                    NormalizedEmail = email,
                    UsrCadastro = Guid.NewGuid(),
                    UsrDescricaoCadastro = "Usuário adm",
                    EmailConfirmed = true
                };
                var resultado = _userManager
                    .CreateAsync(model, password).Result;

                if (resultado.Succeeded)
                    _userManager.AddToRoleAsync(model, Roles.Administrador).Wait();
            }

        }
    }


}
