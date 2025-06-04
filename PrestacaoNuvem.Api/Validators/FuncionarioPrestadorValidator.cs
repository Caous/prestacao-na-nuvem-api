namespace PrestacaoNuvem.Api.Validators;

public class FuncionarioPrestadorValidator : AbstractValidator<FuncionarioPrestadorDto>
{
    public FuncionarioPrestadorValidator()
    {
        RuleFor(x => x.Nome)
           .NotEmpty()
           .WithMessage(FuncionarioPrestadorConst.NomeValidation)
           .NotNull()
           .WithMessage(FuncionarioPrestadorConst.NomeValidation);

        RuleFor(x => x.Telefone)
            .NotEmpty()
            .WithMessage(FuncionarioPrestadorConst.TelefoneValidation)
            .NotNull()
            .WithMessage(FuncionarioPrestadorConst.TelefoneValidation);

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(FuncionarioPrestadorConst.EmailValidation)
            .NotNull()
            .WithMessage(FuncionarioPrestadorConst.EmailValidation);

        RuleFor(x => x.CPF)
            .Custom((cpf, context) =>
            {
                if (!CpfValidations.FormartValidation(cpf))
                    context.AddFailure(ClienteConst.CpfNaoValidado);
            })
           .NotEmpty()
           .WithMessage(FuncionarioPrestadorConst.CPFValidation)
           .NotNull()
           .WithMessage(FuncionarioPrestadorConst.CPFValidation);
    }
}
