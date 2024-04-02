using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace PrestacaoNuvem.Api.Util
{
    [ExcludeFromCodeCoverage]
    public abstract class CnpjValidations
    {
        public static string NumeroCnpj { get; private set; }

        public static string CnpjSemPontuacao(string numeroCnpj)
        {
            if (string.IsNullOrWhiteSpace(numeroCnpj))
            {
                return string.Empty;
            }

            NumeroCnpj = Regex.Replace(numeroCnpj, @"[^\d]", "");
            return NumeroCnpj;
        }

        public static bool FormartValidation(string cnpjValue)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj;
            string digito;

            int soma;
            int resto;

            cnpjValue = cnpjValue.Trim();
            cnpjValue = cnpjValue.Replace(".", "").Replace("/", "").Replace("-", "");

            if (cnpjValue.Length != 14)
                return false;

            tempCnpj = cnpjValue[..12];

            soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);
            resto = resto < 2 ? 0 : 11 - resto;

            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            resto = resto < 2 ? 0 : 11 - resto;
            digito = digito + resto.ToString();

            return cnpjValue.EndsWith(digito);
        }
    }
}
