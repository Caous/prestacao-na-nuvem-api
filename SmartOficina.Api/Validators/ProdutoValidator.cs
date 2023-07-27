namespace SmartOficina.Api.Validators
{
    public class ProdutoValidator : AbstractValidator<ProdutoDto>
    {
        public ProdutoValidator() {

            RuleFor(x => x.Nome)
           .NotEmpty()
           .WithMessage(ProdutoConst.NomeValidation)
           .NotNull()
           .WithMessage(ProdutoConst.NomeValidation);

            RuleFor(x => x.Marca)
           .NotEmpty()
           .WithMessage(ProdutoConst.MarcaValidation)
           .NotNull()
           .WithMessage(ProdutoConst.MarcaValidation);

            RuleFor(x => x.Modelo)
           .NotEmpty()
           .WithMessage(ProdutoConst.ModeloValidation)
           .NotNull()
           .WithMessage(ProdutoConst.ModeloValidation);

            RuleFor(x => x.Data_validade)
           .NotEmpty()
           .WithMessage(ProdutoConst.DataValidadeValidation)
           .NotNull()
           .WithMessage(ProdutoConst.DataValidadeValidation);

            RuleFor(x => x.Garantia)
           .NotEmpty()
           .WithMessage(ProdutoConst.GarantiaValidation)
           .NotNull()
           .WithMessage(ProdutoConst.GarantiaValidation);

            RuleFor(x => x.Valor_Compra)
           .NotEmpty()
           .WithMessage(ProdutoConst.ValorCompraValidation)
           .NotNull()
           .WithMessage(ProdutoConst.ValorCompraValidation);

            RuleFor(x => x.Valor_Venda)
           .NotEmpty()
           .WithMessage(ProdutoConst.ValorVendaValidation)
           .NotNull()
           .WithMessage(ProdutoConst.ValorVendaValidation);
        }
    }
}
