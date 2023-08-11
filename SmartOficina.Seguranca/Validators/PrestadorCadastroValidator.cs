namespace SmartOficina.Seguranca.Validators;

public class PrestadorCadastroValidator : AbstractValidator<PrestadorCadastroDto>
{
    public PrestadorCadastroValidator()
    {
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(PrestadorConst.PrestadorSenhaVazio)
            .NotNull().WithMessage(PrestadorConst.PrestadorSenhaVazio)
            .MinimumLength(6).WithMessage(PrestadorConst.PrestadorSenhaVazio)
            .MaximumLength(12).WithMessage(PrestadorConst.PrestadorSenhaVazio);

        RuleFor(x=> x.Email)
            .NotEmpty().WithMessage(PrestadorConst.PrestadorEmailVazio)
            .NotNull().WithMessage(PrestadorConst.PrestadorEmailVazio);

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage(PrestadorConst.PrestadorNomeUsarioVazio)
            .NotNull().WithMessage(PrestadorConst.PrestadorNomeUsarioVazio);

    }
}
