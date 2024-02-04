using SmartOficina.Api.Domain.Interfacesk;

namespace SmartOficina.Api.Domain.Services
{
    public class EmailManager : IEmailManager
    {
        public EmailManager()
        {

        }

        public async Task<bool> SendEmailAsync(Email emailConfig)
        {
            if (!ValidationEmails(emailConfig.FromEmail, emailConfig.ToEmail))
                return false;

            if (emailConfig.Menssage.IsMissing())
                return false;

            if (emailConfig.Subject.IsMissing())
                return false;
            
            return true;
        }

        private bool ValidationEmails(string fromEmail, string[] toEmail)
        {
            if (!Email.Validation(fromEmail))
                return false;
            for (int i = 0; i < toEmail.Length; i++)
                if (!Email.Validation(toEmail[i])) return false;

            return true;
        }
    }
}
