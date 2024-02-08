namespace PrestacaoNuvem.Api.Validators;

public class ServicoValidator : AbstractValidator<ServicoDto>
{
    public ServicoValidator()
    {
        RuleFor(x => x.Descricao)
            .NotEmpty()
            .WithMessage(ServicoConst.NomeValidation)
            .NotNull()
            .WithMessage(ServicoConst.NomeValidation);

        RuleFor(x => x.Valor)
            .NotEmpty()
            .WithMessage(ServicoConst.ValorValidation)
            .NotNull()
            .WithMessage(ServicoConst.ValorValidation);

        RuleFor(x => x.SubServicoId)
        .NotEmpty()
        .WithMessage(ServicoConst.IdSubServicoValidation)
        .NotNull()
        .WithMessage(ServicoConst.IdSubServicoValidation);
    }
}
