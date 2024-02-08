namespace PrestacaoNuvem.Api.Domain.Model;
public class Cpf
{
    public string Numero { get; private set; }
    public Cpf(string cpf)
    {

        ValidationCPF(cpf);

        Numero = cpf;
    }

    private void ValidationCPF(string cpf)
    {
        if(cpf.IsMissing())
            throw new ArgumentException("Cpf não informado");

        if (FormartValidation(cpf))
            throw new ArgumentException("Cpf não é válido");

    }

    public bool FormartValidation(string cpfValue)
    {


        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf;
        string digito;

        int soma;
        int resto;

        cpfValue = cpfValue.Trim();
        cpfValue = cpfValue.Replace(".", "").Replace("-", "");

        if (cpfValue.Length != 11)
            return false;

        tempCpf = cpfValue[..9];

        soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        resto = soma % 11;

        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = resto.ToString();
        tempCpf = tempCpf + digito;
        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        digito = digito + resto.ToString();
        return cpfValue.EndsWith(digito);
    }


}
