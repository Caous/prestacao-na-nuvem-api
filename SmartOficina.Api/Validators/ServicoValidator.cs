namespace SmartOficina.Api.Validators;

public class ServicoValidator : AbstractValidator<ServicoDto>
{
    public ServicoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage(ServicoConst.NomeValidation)
            .NotNull()
            .WithMessage(ServicoConst.NomeValidation);

        RuleFor(x => x.Valor)
            .NotEmpty()
            .WithMessage(ServicoConst.ValorValidation)
            .NotNull()
            .WithMessage(ServicoConst.ValorValidation);
    }
}
