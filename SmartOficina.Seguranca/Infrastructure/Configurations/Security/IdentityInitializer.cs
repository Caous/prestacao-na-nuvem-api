namespace SmartOficina.Seguranca.Infrastructure.Configurations.Security;

public class IdentityInitializer
{
    private readonly SegurancaContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;

    public IdentityInitializer(SegurancaContext context,
            UserManager<UserModel> userManager,
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

            
        }
    }

    
}
