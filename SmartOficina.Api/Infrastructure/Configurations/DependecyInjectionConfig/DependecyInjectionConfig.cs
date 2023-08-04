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

        #region Cors
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("http://localhost:5173");
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            });
        });
        #endregion

        #region Json
        services.AddControllers(opt =>
        {
            opt.Filters.Add<ApiKeyAttribute>();
        }).AddJsonOptions(cfg =>
        {
            cfg.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            cfg.JsonSerializerOptions.MaxDepth = 0;
        });
        #endregion

        #region Fluent
        services.AddControllers().AddFluentValidation(options =>
        {
            // Validate child properties and root collection elements
            options.ImplicitlyValidateChildProperties = true;
            options.ImplicitlyValidateRootCollectionElements = true;

            // Automatic registration of validators in assembly
            options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        });
        #endregion

        #region Autentication
        services.AddIdentity<UserAutentication, IdentityRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequiredLength = 6;
        })
                .AddEntityFrameworkStores<DbContext>()
                .AddDefaultTokenProviders();
        #endregion

        #region JWT
        var tokenConfigurations = new TokenConfigurations();
        new ConfigureFromConfigurationOptions<TokenConfigurations>(
            configuration.GetSection("TokenConfigurations"))
                .Configure(tokenConfigurations);

        services.AddJwtSecurity(tokenConfigurations);

        #endregion
    }
}
