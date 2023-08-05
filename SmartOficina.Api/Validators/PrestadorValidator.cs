namespace SmartOficina.Api.Validators;

public class PrestadorValidator : AbstractValidator<PrestadorDto>
{
    public PrestadorValidator()
    {
        RuleFor(x => x.TipoCadastro)
          .NotEmpty()
          .WithMessage(PrestadorConst.TipoCadastroValidation)
          .NotNull()
          .WithMessage(PrestadorConst.TipoCadastroValidation);

        //RuleFor(x => x.SituacaoCadastral)
        //  .NotEmpty()
        //  .WithMessage(PrestadorConst.SituacaoCadastralValidation)
        //  .NotNull()
        //  .WithMessage(PrestadorConst.SituacaoCadastralValidation);


    }
}
