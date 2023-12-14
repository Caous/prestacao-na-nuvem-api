using System.Text.RegularExpressions;

namespace SmartOficina.Api.Util;

public abstract class RgValidations
{
    public static string RemoverPontuacaoRg(string rg)
    {
        if (rg.IsMissing())
        {
            return string.Empty;
        }
        return Regex.Replace(rg, @"[^\d]", ""); ;
    }
}
