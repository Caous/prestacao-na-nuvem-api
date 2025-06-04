namespace PrestacaoNuvem.Api.Util;
public static class FormatHelper
{
    public static string RemoveMask(string input)
    {
        return string.IsNullOrWhiteSpace(input)
            ? string.Empty
            : new string(input.Where(char.IsDigit).ToArray());
    }
}
