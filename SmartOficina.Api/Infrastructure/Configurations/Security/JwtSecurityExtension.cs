namespace SmartOficina.Api.Infrastructure.Configurations.Security;

public static class JwtSecurityExtension
{
    public static IServiceCollection AddJwtSecurity(this IServiceCollection services, TokenConfigurations tokenConfigurations)
    {

        return services;
    }
}
