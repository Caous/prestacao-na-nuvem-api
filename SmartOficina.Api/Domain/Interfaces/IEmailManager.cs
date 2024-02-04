namespace SmartOficina.Api.Domain.Interfacesk
{
    public interface IEmailManager
    {
        Task<bool> SendEmailAsync(Email emailConfig);
    }
}
