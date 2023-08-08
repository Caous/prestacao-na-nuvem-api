namespace SmartOficina.Api.Validators;

public class VeiculoValidator : AbstractValidator<VeiculoDto>
{
    public VeiculoValidator()
    {
        RuleFor(x=> x.Placa)
            .NotEmpty()
            .WithMessage(VeiculoConst.PlacaValidation)
            .NotNull()
            .WithMessage(VeiculoConst.PlacaValidation);


        //RuleFor(x => x.Tipo)
        //    .NotEmpty()
        //    .WithMessage(VeiculoConst.TipoValidation)
        //    .NotNull()
        //    .WithMessage(VeiculoConst.TipoValidation);


        RuleFor(x => x.Modelo)
            .NotEmpty()
            .WithMessage(VeiculoConst.ModeloValidation)
            .NotNull()
            .WithMessage(VeiculoConst.ModeloValidation);

        RuleFor(x => x.Marca)
            .NotEmpty()
            .WithMessage(VeiculoConst.MarcaValidation)
            .NotNull()
            .WithMessage(VeiculoConst.MarcaValidation);

        RuleFor(x => x.Ano)
        .NotEmpty()
        .WithMessage(VeiculoConst.AnoValidation)
        .NotNull()
        .WithMessage(VeiculoConst.AnoValidation);

        RuleFor(x => x.TipoCombustivel)
        .NotEmpty()
        .WithMessage(VeiculoConst.TipoCombustivelValidation)
        .NotNull()
        .WithMessage(VeiculoConst.TipoCombustivelValidation);
    }
}
