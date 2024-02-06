namespace SmartOficina.Api.Domain.Interfacesk
{
    public interface IEmailManager
    {
        Task<bool> SendEmailSmtpAsync(Email emailConfig);
    }
}
