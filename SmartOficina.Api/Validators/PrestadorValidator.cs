namespace SmartOficina.Api.Validators;

public class PrestadorValidator : AbstractValidator<PrestadorDto>
{
    public PrestadorValidator()
    {
        RuleFor(x=> x.Nome)
            .NotEmpty()
            .WithMessage(PrestadorConst.NomeValidation)
            .NotNull()
            .WithMessage(PrestadorConst.NomeValidation);

    }
}
