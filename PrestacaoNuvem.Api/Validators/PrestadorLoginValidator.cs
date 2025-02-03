namespace PrestacaoNuvem.Api.Validators;

public class PrestadorLoginValidator : AbstractValidator<PrestadorLoginDto> 
{
    public PrestadorLoginValidator()
    {
        RuleFor(x => x.Password)
         .NotEmpty().WithMessage(PrestadorConst.PrestadorSenhaVazio)
         .NotNull().WithMessage(PrestadorConst.PrestadorSenhaVazio)
         .MinimumLength(6).WithMessage(PrestadorConst.PrestadorSenhaVazio)
         .MaximumLength(12).WithMessage(PrestadorConst.PrestadorSenhaVazio);

        //ToDo: Verificar como validar duas variáveis ao mesmo tempo
        //RuleFor(x => x.Email)
        //    .Custom((email, context) =>{ 
            
        //    email
        //    })
        //    .NotEmpty().WithMessage(PrestadorConst.PrestadorEmailVazio)
        //    .NotNull().WithMessage(PrestadorConst.PrestadorEmailVazio);

        //RuleFor(x => x.UserName)
        //    .NotEmpty().WithMessage(PrestadorConst.PrestadorNomeUsarioVazio)
        //    .NotNull().WithMessage(PrestadorConst.PrestadorNomeUsarioVazio);
    }
}
