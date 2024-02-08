namespace PrestacaoNuvem.Seguranca.Infrastructure.Configurations.SwaggerConfig;

public class AuthResponsesOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var authAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
            .Union(context.MethodInfo.GetCustomAttributes(true))
            .OfType<AuthorizeAttribute>();

        if (authAttributes.Any())
        {
            var securityRequirement = new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                         Reference = new OpenApiReference
                         {
                             Id = JwtBearerDefaults.AuthenticationScheme,
                             Type = ReferenceType.SecurityScheme
                         }
                    },
                    new List<string>()
                }
            };
            operation.Security = new List<OpenApiSecurityRequirement> { securityRequirement };
            operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
        }
    }
}
