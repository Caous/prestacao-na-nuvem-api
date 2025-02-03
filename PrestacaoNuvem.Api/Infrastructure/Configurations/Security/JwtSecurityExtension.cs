namespace PrestacaoNuvem.Api.Infrastructure.Configurations.Security;

public static class JwtSecurityExtension
{
    public static void AddJwtSecurity
    (
        this IServiceCollection services,
        TokenConfigurations tokenConfigurations
    )
    {
        // Configurando a dependência para a classe de validação
        // de credenciais e geração de tokens
        services.AddScoped<IAcessoManager, AcessoManager>();

        var signingConfigurations = new SigningConfigurations(
            tokenConfigurations.SecretJwtKey!);
        services.AddSingleton(signingConfigurations);

        services.AddSingleton(tokenConfigurations);

        services.AddAuthentication(authOptions =>
        {
            authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            authOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(bearerOptions =>
        {
            var paramsValidation = bearerOptions.TokenValidationParameters;
            paramsValidation.IssuerSigningKey = signingConfigurations.Key;
            paramsValidation.ValidAudience = tokenConfigurations.Audience;
            paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

            // Valida a assinatura de um token recebido
            paramsValidation.ValidateIssuerSigningKey = true;

            // Verifica se um token recebido ainda é válido
            paramsValidation.ValidateLifetime = true;

            // Tempo de tolerância para a expiração de um token (utilizado
            // caso haja problemas de sincronismo de horário entre diferentes
            // computadores envolvidos no processo de comunicação)
            paramsValidation.ClockSkew = TimeSpan.Zero;
            
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
