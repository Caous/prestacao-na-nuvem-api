namespace SmartOficina.Api.Infrastructure.Configurations.SwaggerConfig;

public static class SwaggerConfig
{
    public static void AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(c =>
         {
             c.SwaggerDoc("v1", new OpenApiInfo { Title = "Smart Oficina v1", Version = "v1" });
             c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
             {
                 Description = "X-API-Key must appear in header",
                 Type = SecuritySchemeType.ApiKey,
                 Name = "X-API-Key",
                 In = ParameterLocation.Header,
                 Scheme = "ApiKeyScheme"
             });
             var key = new OpenApiSecurityScheme()
             {
                 Reference = new OpenApiReference
                 {
                     Type = ReferenceType.SecurityScheme,
                     Id = "ApiKey"
                 },
                 In = ParameterLocation.Header
             };
             var requirement = new OpenApiSecurityRequirement
                        {
                             { key, new List<string>() }
                        };
             c.AddSecurityRequirement(requirement);
         });
    }
}
