namespace PrestacaoNuvem.Api.Domain.Interfacesk
{
    public interface IEmailManager
    {
        Task<bool> SendEmailSmtpAsync(Email emailConfig);
        Task<bool> PostPropostaEmailAsync(EmailRequestPropostaDto request);
        Task<bool> PostPropostaEmailComAnexoAsync(EmailRequestContratoFileDto request, byte[] file);
    }
}
