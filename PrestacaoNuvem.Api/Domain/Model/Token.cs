namespace PrestacaoNuvem.Api.Domain.Model;

public class Token
{
    public bool Authenticated { get; set; }
    public string? Created { get; set; }
    public string? Expiration { get; set; }
    public string? AccessToken { get; set; }
    public string? Message { get; set; }
    public string[] Roles { get; set; } = Array.Empty<string>();
    public string UserName { get; set; }
}

