using SmartOficina.Api.Util;

namespace SmartOficina.Api.Validators;

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

        //RuleFor(x => x.RG)
        //    .Custom((rg, context) =>
        //    {
        //        if (!RgValidations.ValidarRG(rg))
        //            context.AddFailure(FuncionarioPrestadorConst.RgValitation);
        //    })
        //   .NotEmpty()
        //   .WithMessage(FuncionarioPrestadorConst.RGValidation)
        //   .NotNull()
        //   .WithMessage(FuncionarioPrestadorConst.RGValidation);

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

        RuleFor(x => x.Endereco)
           .NotEmpty()
           .WithMessage(FuncionarioPrestadorConst.EnderecoValidation)
           .NotNull()
           .WithMessage(FuncionarioPrestadorConst.EnderecoValidation);

        RuleFor(x => x.Cargo)
          .NotEmpty()
          .WithMessage(FuncionarioPrestadorConst.CargoValidation)
          .NotNull()
          .WithMessage(FuncionarioPrestadorConst.CargoValidation);
    }
}
