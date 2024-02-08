namespace PrestacaoNuvem.Seguranca.Infrastructure.Configurations.Security;

public class TokenConfigurations
{
    public string? Audience { get; set; }
    public string? Issuer { get; set; }
    public int Days { get; set; }
    public string? SecretJwtKey { get; set; }
}
