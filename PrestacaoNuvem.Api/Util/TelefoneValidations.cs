using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace PrestacaoNuvem.Api.Util;

[ExcludeFromCodeCoverage]
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
