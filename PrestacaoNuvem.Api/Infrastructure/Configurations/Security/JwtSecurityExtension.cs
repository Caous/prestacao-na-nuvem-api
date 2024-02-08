namespace PrestacaoNuvem.Api.Infrastructure.Configurations.Security;

public static class JwtSecurityExtension
{
    public static void AddJwtSecurity
    (
        this IServiceCollection services,
        TokenConfigurations tokenConfigurations
    )
    {
        services.AddAuthentication(authOptions =>
        {
            authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            authOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(bearerOptions =>
        {
            bearerOptions.TokenValidationParameters = new TokenValidationParameters
            {
                // Valida a assinatura de um token recebido
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.SecretJwtKey)),
                ValidAudience = tokenConfigurations.Audience,
                ValidIssuer = tokenConfigurations.Issuer,
            
                // Verifica se um token recebido ainda é válido
                ValidateLifetime = true,
                
            };
            
            // Tempo de tolerância para a expiração de um token (utilizado
            // caso haja problemas de sincronismo de horário entre diferentes
            // computadores envolvidos no processo de comunicação)
            //paramsValidation.ClockSkew = TimeSpan.Zero;




        });

        services.AddAuthorization(auth =>
        {
            auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                .RequireAuthenticatedUser().Build());
        });

    }
}
