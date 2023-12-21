using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SmartOficina.Api.Util;

[ExcludeFromCodeCoverage]
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

    public static bool ValidarRG(string rgValue)
    {
        // Remover caracteres não numéricos
        rgValue = new string(rgValue.Where(char.IsDigit).ToArray());

        // Verificar se o RG possui um comprimento válido
        if (rgValue.Length != 9)
            return false;

        // Verificar se o primeiro caractere é diferente de zero
        if (rgValue[0] == '0')
            return false;

        // Extrair os 8 primeiros dígitos do RG
        string baseRg = rgValue.Substring(0, 8);

        // Verificar se a data de nascimento é válida (assumindo formato DDMMAAAA)
        if (!DateTime.TryParseExact(baseRg, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            return false;

        // Calcular o dígito verificador
        int digitoVerificador = int.Parse(rgValue[8].ToString());

        // Calcular o dígito verificador esperado
        int soma = 0;
        for (int i = 0; i < 8; i++)
            soma += int.Parse(baseRg[i].ToString()) * (8 - i);

        int resto = soma % 11;
        int digitoEsperado = 11 - resto;
        if (digitoEsperado >= 10)
            digitoEsperado = 0;

        // Comparar o dígito verificador calculado com o informado
        return digitoVerificador == digitoEsperado;
    }
}
