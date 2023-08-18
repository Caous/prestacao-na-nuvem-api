using System.Text.RegularExpressions;

namespace SmartOficina.Api.Domain.Model;
public class Email
{
    public Email(string endereco)
    {
        Validation(endereco);
        Endereco = endereco;
    }
    public string Endereco { get; private set; }

    private static void Validation(string email)
    {
        if (email.IsMissing()) return;

        var emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        var match = emailRegex.Match(email);

        if (!match.Success)
            throw new ArgumentException("E-mail não tem formato válido");

    }

}
