using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace PrestacaoNuvem.Api.Util;

[ExcludeFromCodeCoverage]
public abstract class TelefoneValidations
{
    public static string FormatarNumeroTelefone(string telefone)
    {
        if (string.IsNullOrEmpty(telefone))
        {
            return string.Empty;
        }

        string numerosApenas = RemoverPontuacaoTelefone(telefone);

        // Formatar o número de telefone
        if (numerosApenas.Length == 11)
        {
            return $"({numerosApenas.Substring(0, 2)}) {numerosApenas.Substring(2, 5)}-{numerosApenas.Substring(7)}";
        }
        else if (numerosApenas.Length == 10)
        {
            return $"({numerosApenas.Substring(0, 2)}) {numerosApenas.Substring(2, 4)}-{numerosApenas.Substring(6)}";
        }
        else
        {
            // Se o número de telefone não estiver no formato esperado, retornar o número sem formatação
            return numerosApenas;
        }
    }
    public static string RemoverPontuacaoTelefone(string telefone)
    {
        if (telefone.IsMissing())
        {
            return string.Empty;
        }
        return Regex.Replace(telefone, @"[^\d]", "");

    }
}
