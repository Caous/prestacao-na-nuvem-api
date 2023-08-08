namespace SmartOficina.Seguranca.Infrastructure.Configurations.ContextConfiguration;

public static class DbContextConfiguration
{
    public static void RegisterContext(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<SegurancaContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
    }
}