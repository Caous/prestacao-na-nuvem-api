namespace SmartOficina.Api.Validators;

public class SubServicoValidator : AbstractValidator<SubCategoriaServicoDto>
{
    public SubServicoValidator()
    {
        RuleFor(x => x.Desc)
            .NotEmpty()
            .WithMessage(SubServicoConst.DescricaoValidation)
            .NotNull()
            .WithMessage(SubServicoConst.DescricaoValidation);

        RuleFor(x => x.Titulo)
            .NotEmpty()
            .WithMessage(SubServicoConst.TituloValidation)
            .NotNull()
            .WithMessage(SubServicoConst.TituloValidation);

        RuleFor(x => x.CategoriaId)
            .NotEmpty()
            .WithMessage(SubServicoConst.IdCategoriaValidation)
            .NotNull()
            .WithMessage(SubServicoConst.IdCategoriaValidation);
    }
}
