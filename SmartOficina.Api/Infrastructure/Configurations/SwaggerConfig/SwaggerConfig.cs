using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace SmartOficina.Api.Infrastructure.Configurations.SwaggerConfig;

public static class SwaggerConfig
{
    public static void AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(setup =>
         {
             //c.SwaggerDoc("v1", new OpenApiInfo { Title = "Smart Oficina v1", Version = "v1" });
             //c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
             //{
             //    Description = "X-API-Key must appear in header",
             //    Type = SecuritySchemeType.ApiKey,
             //    Name = "X-API-Key",
             //    In = ParameterLocation.Header,
             //    Scheme = "ApiKeyScheme"
             //});
             //var key = new OpenApiSecurityScheme()
             //{
             //    Reference = new OpenApiReference
             //    {
             //        Type = ReferenceType.SecurityScheme,
             //        Id = "ApiKey"
             //    },
             //    In = ParameterLocation.Header
             //};
             //var requirement = new OpenApiSecurityRequirement
             //           {
             //                { key, new List<string>() }
             //           };
             //c.AddSecurityRequirement(requirement);

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

             setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

             setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
         });
    }
}
