using PrestacaoNuvem.Api.Util;

namespace PrestacaoNuvem.Api.Validators;

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
           .Custom((cpf, context) =>
           {
               if (!CpfValidations.FormartValidation(cpf))
                   context.AddFailure(ClienteConst.CpfNaoValidado);
           })
           .NotEmpty()
           .WithMessage(ClienteConst.CPFValidation)
           .NotNull()
           .WithMessage(ClienteConst.CPFValidation);

        RuleFor(x => x.Endereco)
           .NotEmpty()
           .WithMessage(ClienteConst.EnderecoValidation)
           .NotNull()
           .WithMessage(ClienteConst.EnderecoValidation);

    }
}
