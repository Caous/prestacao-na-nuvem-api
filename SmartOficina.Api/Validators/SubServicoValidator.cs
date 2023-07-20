namespace SmartOficina.Api.Validators;

public class SubServicoValidator : AbstractValidator<SubServicoDto>
{
    public SubServicoValidator()
    {
        RuleFor(x => x.Desc)
            .NotEmpty()
            .WithMessage("Por favor adiconar uma descrição para o serviço")
            .NotNull()
            .WithMessage("Por favor adicionar uma descrição para o serviço");

        RuleFor(x => x.Titulo)
            .NotEmpty()
            .WithMessage("Por favor adiconar um titulo para o serviço")
            .NotNull()
            .WithMessage("Por favor adicionar uma descrição para o serviço");
    }
}
