namespace SmartOficina.Api.Validators;

public class ServicoValidator : AbstractValidator<ServicoDto>
{
    public ServicoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage(SubServicoConst.NomeValidation)
            .NotNull()
            .WithMessage(SubServicoConst.NomeValidation);

        RuleFor(x => x.Valor)
            .NotEmpty()
            .WithMessage(SubServicoConst.ValorValidation)
            .NotNull()
            .WithMessage(SubServicoConst.ValorValidation);
    }
}
