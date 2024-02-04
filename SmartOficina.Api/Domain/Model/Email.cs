using System.Text.RegularExpressions;

namespace SmartOficina.Api.Domain.Model;

public class Email
{
    public Email(EmailConfigHost configHost)
    {
        ConfigHost = configHost;
    }
    public Email()
    {

    }
    public EmailConfigHost ConfigHost { get; set; }
    public string[] ToEmail { get; set; }
    public string FromEmail { get; set; }
    public string Menssage { get; set; }
    public string[] Cco { get; set; }
    public string Subject { get; set; }

    public static bool Validation(string email)
    {
        if (email.IsMissing()) return false;

        var emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        var match = emailRegex.Match(email);

        if (!match.Success)
            return false;

        return true;
    }
}
