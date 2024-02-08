namespace PrestacaoNuvem.Api;

public static class StringExtensions
{

    public static bool IsMissing(this string value) { 
    
        return value == null || value.Length == 0;
    }

}
