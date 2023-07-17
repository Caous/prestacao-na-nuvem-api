namespace SmartOficina.Api.Infrastructure.Configurations.DependecyInjectionConfig;

public static class DependecyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IPrestadorRepository, PrestadorRepository>();
        services.AddScoped<IVeiculoRepository, VeiculoRepository>();
        services.AddScoped<IPrestacaoServicoRepository, PrestacaoServicoRepository>();
        services.AddScoped<ICategoriaServicoRepository, CategoriaServicoRepository>();
        services.AddScoped<ISubServicoRepository, SubServicoRepository>();
    }
}
