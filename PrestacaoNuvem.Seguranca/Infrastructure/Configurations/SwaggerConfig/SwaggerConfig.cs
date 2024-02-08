namespace PrestacaoNuvem.Seguranca.Infrastructure.Configurations.SwaggerConfig;

public static class SwaggerConfig
{
    public static void AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Prestacao Nuvem v1", Version = "v1" });

            var scheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            c.OperationFilter<AuthResponsesOperationFilter>();

            c.AddSecurityDefinition(scheme.Reference.Id, scheme);

        });
    }
}

