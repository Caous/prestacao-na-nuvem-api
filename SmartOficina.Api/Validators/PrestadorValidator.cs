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

        RuleFor(x => x.CNPJ)
           .NotEmpty()
           .WithMessage(PrestadorConst.RGValidation)
           .NotNull()
           .WithMessage(PrestadorConst.RGValidation);

        RuleFor(x => x.CPF)
           .NotEmpty()
           .WithMessage(PrestadorConst.CPFValidation)
           .NotNull()
           .WithMessage(PrestadorConst.CPFValidation);

        RuleFor(x => x.Razao_Social)
           .NotEmpty()
           .WithMessage(PrestadorConst.NomeValidation)
           .NotNull()
           .WithMessage(PrestadorConst.NomeValidation);

        RuleFor(x => x.Nome_Fantasia)
           .NotEmpty()
           .WithMessage(PrestadorConst.NomeValidation)
           .NotNull()
           .WithMessage(PrestadorConst.NomeValidation);

        RuleFor(x => x.Representante)
           .NotEmpty()
           .WithMessage(PrestadorConst.NomeValidation)
           .NotNull()
           .WithMessage(PrestadorConst.NomeValidation);

        RuleFor(x => x.Telefone)
           .NotEmpty()
           .WithMessage(PrestadorConst.NomeValidation)
           .NotNull()
           .WithMessage(PrestadorConst.NomeValidation);

        RuleFor(x => x.Email)
           .NotEmpty()
           .WithMessage(PrestadorConst.NomeValidation)
           .NotNull()
           .WithMessage(PrestadorConst.NomeValidation);

        RuleFor(x => x.Endereco)
           .NotEmpty()
           .WithMessage(PrestadorConst.NomeValidation)
           .NotNull()
           .WithMessage(PrestadorConst.NomeValidation);

    }
}
