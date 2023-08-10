namespace SmartOficina.Api.Infrastructure.Configurations.SwaggerConfig;

public static class SwaggerConfig
{
    public static void AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(setup =>
         {
             setup.SwaggerDoc("v1", new OpenApiInfo { Title = "Smart Oficina v1", Version = "v1" });
             
             var jwtSecurityScheme = new OpenApiSecurityScheme
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

             setup.OperationFilter<SecurityRequirementsOperationFilter>();

             setup.AddSecurityDefinition("Bearer", jwtSecurityScheme);

             //setup.AddSecurityRequirement(new OpenApiSecurityRequirement
             //{
             //    { jwtSecurityScheme, Array.Empty<string>() }
             //});
         });
    }
}
