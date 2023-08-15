namespace SmartOficina.Api.Infrastructure.Configurations.ContextConfiguration;

public static class DbContextConfiguration
{
    public static void RegisterContext(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<OficinaContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
    }
}
