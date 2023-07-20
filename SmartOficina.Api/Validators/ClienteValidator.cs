namespace SmartOficina.Api.Validators;

public class ClienteValidator : AbstractValidator<ClienteDto>
{
    public ClienteValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(ClienteConst.EmailValidation)
            .NotNull()
            .WithMessage(ClienteConst.EmailValidation);

        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage(ClienteConst.NomeValidation)
            .NotNull()
            .WithMessage(ClienteConst.NomeValidation);

        RuleFor(x => x.Telefone)
            .NotEmpty()
            .WithMessage(ClienteConst.TelefoneValidation)
            .NotNull()
            .WithMessage(ClienteConst.TelefoneValidation);
    }
}
