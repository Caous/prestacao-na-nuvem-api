using PrestacaoNuvem.Api.Enumerations;

namespace PrestacaoNuvem.Api.Domain.Model;
public class Telefone
{
    public string CodigoArea { get; private set; }
    public string Numero { get; private set; }
    public ETipoTelefone TipoTelefone { get; private set; }
    public Telefone(string codigoArea, string numero, ETipoTelefone tipoTelefone)
    {
        Validation(codigoArea, numero);
        CodigoArea = codigoArea;
        Numero = numero;
        TipoTelefone = tipoTelefone;
    }

    private void Validation(string codigoArea, string numero)
    {
        if (codigoArea.IsMissing())
            throw new ArgumentException("Codigo área não informado");
        if (numero.IsMissing())
            throw new ArgumentException("Número não informado");
    }
}
