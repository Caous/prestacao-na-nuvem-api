namespace SmartOficina.Api.Validators;

public class ProdutoValidator : AbstractValidator<ProdutoDto>
{
    public ProdutoValidator()
    {

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

        RuleFor(x => x.TipoMedidaItem)
        .NotEmpty()
        .WithMessage(ProdutoConst.TipoMedidaValidation)
        .NotNull()
        .WithMessage(ProdutoConst.TipoMedidaValidation);

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

        RuleFor(x => x.PrestadorId)
        .NotEmpty()
        .WithMessage(ClienteConst.PrestadorValidation)
        .NotNull()
        .WithMessage(ClienteConst.PrestadorValidation);
    }
}
