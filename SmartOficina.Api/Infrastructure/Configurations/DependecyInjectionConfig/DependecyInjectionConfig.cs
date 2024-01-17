using SmartOficina.Api.Domain.Interfaces;
using SmartOficina.Api.Domain.Services;

namespace SmartOficina.Api.Infrastructure.Configurations.DependecyInjectionConfig;

public static class DependecyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
       
        #region Injection Repository
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IPrestadorRepository, PrestadorRepository>();
        services.AddScoped<IVeiculoRepository, VeiculoRepository>();
        services.AddScoped<IPrestacaoServicoRepository, PrestacaoServicoRepository>();
        services.AddScoped<ICategoriaServicoRepository, CategoriaServicoRepository>();
        services.AddScoped<ISubServicoRepository, SubServicoRepository>();
        services.AddScoped<IServicoRepository, ServicoRepository>();
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IFuncionarioPrestadorRepository, FuncionarioPrestadorServiceRepository>();
        #endregion

        #region Injection Services
        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<ICategoriaService, CategoriaService>();
        services.AddScoped<IFuncionarioService, FuncionarioService>();
        services.AddScoped<IPrestacaoServicoService, PrestacaoServicoService>();
        services.AddScoped<IPrestadorService, PrestadorService>();
        services.AddScoped<IProdutoService, ProdutoService>();
        services.AddScoped<ISubCategoriaServicoService, SubCategoriaServicoService>();
        services.AddScoped<IVeiculoService, VeiculoService>();
        #endregion

        #region Cors
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("http://localhost:5173", "https://smart-oficina-ui.vercel.app");
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            });
        });
        #endregion

        #region Json
        services.AddControllers().AddJsonOptions(cfg =>
        {
            cfg.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            cfg.JsonSerializerOptions.MaxDepth = 0;
        });
        #endregion

        #region Fluent
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        #endregion
    }
}
