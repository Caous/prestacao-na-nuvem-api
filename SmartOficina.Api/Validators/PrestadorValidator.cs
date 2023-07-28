namespace SmartOficina.Api.Validators;

public class PrestadorValidator : AbstractValidator<PrestadorDto>
{
    public PrestadorValidator()
    {
        RuleFor(x => x.TipoCadastro)
          .NotEmpty()
          .WithMessage(PrestadorConst.TipoCadastroValidation)
          .NotNull()
          .WithMessage(PrestadorConst.TipoCadastroValidation);

        RuleFor(x=> x.Nome)
            .NotEmpty()
            .WithMessage(PrestadorConst.NomeValidation)
            .NotNull()
            .WithMessage(PrestadorConst.NomeValidation);

        RuleFor(x => x.CPF)
          .NotEmpty()
          .WithMessage(PrestadorConst.CPFValidation)
          .NotNull()
          .WithMessage(PrestadorConst.CPFValidation);

        RuleFor(x => x.CpfRepresentante)
         .NotEmpty()
         .WithMessage(PrestadorConst.CPFValidation)
         .NotNull()
         .WithMessage(PrestadorConst.CPFValidation);

        RuleFor(x => x.CNPJ)
           .NotEmpty()
           .WithMessage(PrestadorConst.CNPJValidation)
           .NotNull()
           .WithMessage(PrestadorConst.CNPJValidation);

        RuleFor(x => x.RazaoSocial)
           .NotEmpty()
           .WithMessage(PrestadorConst.RazaoSocialValidation)
           .NotNull()
           .WithMessage(PrestadorConst.RazaoSocialValidation);

        RuleFor(x => x.NomeFantasia)
           .NotEmpty()
           .WithMessage(PrestadorConst.NomeFantasiaValidation)
           .NotNull()
           .WithMessage(PrestadorConst.NomeFantasiaValidation);

        RuleFor(x => x.NomeRepresentante)
           .NotEmpty()
           .WithMessage(PrestadorConst.NomeValidation)
           .NotNull()
           .WithMessage(PrestadorConst.NomeValidation);

        RuleFor(x => x.Telefone)
           .NotEmpty()
           .WithMessage(PrestadorConst.TelefoneValidation)
           .NotNull()
           .WithMessage(PrestadorConst.TelefoneValidation);

        RuleFor(x => x.EmailEmpresa)
           .NotEmpty()
           .WithMessage(PrestadorConst.EmailValidation)
           .NotNull()
           .WithMessage(PrestadorConst.EmailValidation);

        RuleFor(x => x.Endereco)
           .NotEmpty()
           .WithMessage(PrestadorConst.EnderecoValidation)
           .NotNull()
           .WithMessage(PrestadorConst.EnderecoValidation);

        RuleFor(x => x.EmailRepresentante)
          .NotEmpty()
          .WithMessage(PrestadorConst.EmailValidation)
          .NotNull()
          .WithMessage(PrestadorConst.EmailValidation);

        RuleFor(x => x.SituacaoCadastral)
          .NotEmpty()
          .WithMessage(PrestadorConst.SituacaoCadastralValidation)
          .NotNull()
          .WithMessage(PrestadorConst.SituacaoCadastralValidation);

        RuleFor(x => x.DataAbertura)
        .NotEmpty()
        .WithMessage(PrestadorConst.DataValidation)
        .NotNull()
        .WithMessage(PrestadorConst.DataValidation);

        RuleFor(x => x.DataSituacaoCadastral)
        .NotEmpty()
        .WithMessage(PrestadorConst.DataValidation)
        .NotNull()
        .WithMessage(PrestadorConst.DataValidation);

    }
}
