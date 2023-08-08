namespace SmartOficina.Seguranca.Infrastructure.Configurations.DependecyInjectionConfig;

public static class DependecyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        #region Autentication
        services.AddScoped<IdentityInitializer>();

        services.AddIdentity<UserModel, IdentityRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequiredLength = 6;
        })
                .AddEntityFrameworkStores<SegurancaContext>()
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
