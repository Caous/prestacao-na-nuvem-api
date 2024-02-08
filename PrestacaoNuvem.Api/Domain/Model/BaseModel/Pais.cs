namespace PrestacaoNuvem.Api.Domain.Model;
public class Pais
{
    public string Pai { get; private set; }
    public string Mae { get; private set; }
    public Pais(string pai, string mae)
    {
        Validation(pai, mae);
        Pai = pai;
        Mae = mae;
    }

    private void Validation(string pai, string mae)
    {
        if (pai.IsMissing())
            throw new ArgumentException("Nome do pai não informado");
        if (mae.IsMissing())
            throw new ArgumentException("Nome da mãe não informado");
    }
}
