using SmartOficina.Api.Enumerations;

namespace SmartOficina.Api.Domain.Model;
public class Graduacao
{
    public EGraduacao GraduacaoEscolar { get; private set; }
    public Graduacao(string graduacao)
    {
        Validation(graduacao);
    }

    private void Validation(string graduacao)
    {
        if (graduacao.IsMissing())
            throw new ArgumentException("Graduação não informada");
        DefinirGraduacao(graduacao);
    }

    private void DefinirGraduacao(string graduacao)
    {
        var superior = nameof(EGraduacao.SUPERIOR).ToUpper();
        var fundamental = nameof(EGraduacao.FUNDAMENTAL).ToUpper();
        

        graduacao = graduacao.ToUpper();

        if (graduacao.Equals(superior))
        {
            GraduacaoEscolar = EGraduacao.SUPERIOR;
        }
        else if (graduacao.Equals(fundamental))
        {
            GraduacaoEscolar = EGraduacao.FUNDAMENTAL;
        }
        else
        {
            GraduacaoEscolar = EGraduacao.SUPERIOR;
        }

    }
}
