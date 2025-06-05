namespace PrestacaoNuvem.Api.Domain.Model;

public class TwilioOptions
{
    public const string Twillio = "Twilio";
    public string Username { get; init; }
    public string Password { get; init; }
    public string Number { get; init; }
    public string NumberCode { get; init; } = null!;
}
