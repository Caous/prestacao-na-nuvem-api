using System.Text.RegularExpressions;

namespace SmartOficina.Api.Util;

public abstract class CpfValidations
{
    Cpf _cpf;

    public static string NumeroCpf { get; private set; }

    public static string CpfSemPontuacao(string numeroCpf) {

        if (numeroCpf.IsMissing())
        {
            return string.Empty;
        }
        NumeroCpf = Regex.Replace(numeroCpf, @"[^\d]", "");

        return NumeroCpf;
    }
}
