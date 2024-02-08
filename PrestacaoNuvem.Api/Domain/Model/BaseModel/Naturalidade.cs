namespace PrestacaoNuvem.Api.Domain.Model;
public class Naturalidade
{
    public Naturalidade(string uf, string cidade)
    {
        Validation(uf, cidade);
        Uf = uf;
        Cidade = cidade;
    }

    private void Validation(string uf, string cidade)
    {
        if (cidade.IsMissing())
            throw new ArgumentException("Cidade não informada");
        if (uf.IsMissing())
            throw new ArgumentException("Uf não infomada");
    }

    public string Uf { get; private set; }
    public string Cidade { get; private set; }


}
