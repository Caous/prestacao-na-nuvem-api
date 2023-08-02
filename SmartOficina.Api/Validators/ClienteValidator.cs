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

        RuleFor(x => x.CPF)
           .NotEmpty()
           .WithMessage(ClienteConst.CPFValidation)
           .NotNull()
           .WithMessage(ClienteConst.CPFValidation);

        RuleFor(x => x.Endereco)
           .NotEmpty()
           .WithMessage(ClienteConst.EnderecoValidation)
           .NotNull()
           .WithMessage(ClienteConst.EnderecoValidation);

        RuleFor(x => x.PrestadorId)
          .NotEmpty()
          .WithMessage(ClienteConst.PrestadorValidation)
          .NotNull()
          .WithMessage(ClienteConst.PrestadorValidation);
    }
}
