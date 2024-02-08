using PrestacaoNuvem.Api.Domain.Interfacesk;
using System.Net.Mail;
using System.Net;

namespace PrestacaoNuvem.Api.Domain.Services
{
    public class EmailManager : IEmailManager
    {
        public EmailManager()
        {

        }

        public async Task<bool> SendEmailSmtpAsync(Email emailConfig)
        {
            if (!ValidationEmails(emailConfig.FromEmail, emailConfig.ToEmail))
                return false;

            if (emailConfig.Menssage.IsMissing())
                return false;

            if (emailConfig.Subject.IsMissing())
                return false;
            try
            {

                using (MailMessage mensagemEmail = new MailMessage(emailConfig.FromEmail, emailConfig.ToEmail[0], emailConfig.Subject, emailConfig.Menssage))
                {
                    SmtpClient smtpClient = new SmtpClient(emailConfig.ConfigHost.Host, emailConfig.ConfigHost.Port);
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(emailConfig.ConfigHost.UserName, emailConfig.ConfigHost.Password);                    
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Timeout = 30000;
                    smtpClient.Send(mensagemEmail);
                }

            }
            catch (Exception)
            {
                return false;
            }
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
