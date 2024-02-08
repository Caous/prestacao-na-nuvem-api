using PrestacaoNuvem.Api.Enumerations;

namespace PrestacaoNuvem.Api.Domain.Model;
public class EstadoCivil
{
    public EEstadoCivil Estado { get; private set; }
    public EstadoCivil(string estadoCivil)
    {
        Validation(estadoCivil);
    }

    private void Validation(string estadoCivil)
    {
        if (estadoCivil.IsMissing())
            throw new ArgumentException("Estado civíl não informada");
        DefinirEstadoCivil(estadoCivil);
    }

    private void DefinirEstadoCivil(string estadoCivil)
    {
        var viuvo = nameof(EEstadoCivil.Viuvo).ToUpper();
        var divorciado = nameof(EEstadoCivil.Divorciado).ToUpper();
        var sepJudicial = nameof(EEstadoCivil.SepJudicialmente).ToUpper();
        var uniaoEstavel = nameof(EEstadoCivil.UniaoEstavel).ToUpper();
        var solteiro = nameof(EEstadoCivil.Solteiro).ToUpper();


        estadoCivil = estadoCivil.ToUpper();

        if (estadoCivil.Equals(viuvo))
        {
            Estado = EEstadoCivil.Viuvo;
        }
        else if (estadoCivil.Equals(divorciado))
        {
            Estado = EEstadoCivil.Divorciado;
        }
        else if (estadoCivil.Equals(sepJudicial))
        {
            Estado = EEstadoCivil.SepJudicialmente;
        }
        else if (estadoCivil.Equals(uniaoEstavel))
        {
            Estado = EEstadoCivil.UniaoEstavel;
        }
        else if (estadoCivil.Equals(solteiro))
        {
            Estado = EEstadoCivil.Solteiro;
        }
        else
        {
            Estado = EEstadoCivil.Casado;
        }

    }
}
