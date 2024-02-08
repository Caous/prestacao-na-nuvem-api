using PrestacaoNuvem.Api.Enumerations;

namespace PrestacaoNuvem.Api.Domain.Model;

public class Sexo
{
    public ESexo SexoTipo { get; private set; }
    public Sexo(string sexo)
    {
        Validation(sexo);
    }

    private void Validation(string sexo)
    {
        if (!sexo.IsMissing())
            throw new ArgumentException("Sexo não preenchido");

        DefinicaoSexo(sexo);
    }

    private void DefinicaoSexo(string sexo)
    {
        var masculino = nameof(ESexo.MASCULINO).ToUpper();
        var femino = nameof(ESexo.FEMININO).ToUpper();

        sexo = sexo.ToUpper();

        if (sexo.Equals(masculino))
        {
            SexoTipo = ESexo.MASCULINO;
        }
        else if (sexo.Equals(femino))
        {
            SexoTipo = ESexo.FEMININO;
        }
        else
        {
            SexoTipo = ESexo.OUTROS;
        }

    }

}
