using System.Text.RegularExpressions;

namespace SmartOficina.Api.Util;

public abstract class TelefoneValidations
{
    public static string RemoverPontuacaoTelefone(string telefone)
    {
        if (telefone.IsMissing())
        {
            return string.Empty;
        }
        return Regex.Replace(telefone, @"[^\d]", "");

    }
}
