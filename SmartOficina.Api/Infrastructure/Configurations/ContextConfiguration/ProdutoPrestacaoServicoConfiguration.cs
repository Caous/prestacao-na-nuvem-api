namespace SmartOficina.Api.Infrastructure.Configurations.ContextConfiguration;

public class ProdutoPrestacaoServicoConfiguration : IEntityTypeConfiguration<ProdutoPrestacaoServico>
{
    public void Configure(EntityTypeBuilder<ProdutoPrestacaoServico> builder)
    {
        builder.ToTable(nameof(ProdutoPrestacaoServico));

        builder.HasKey(k => k.Id);

        builder.Property(p => p.Id).HasValueGenerator<SequentialGuidValueGenerator>();

        builder.Property(p => p.Marca).HasMaxLength(250).IsRequired();

        builder.Property(p => p.Modelo).IsRequired();

        builder.Property(p => p.Garantia).HasMaxLength(200);

        builder.Property(p => p.Nome).HasMaxLength(200).IsRequired();

        builder.Property(p => p.QtdVenda).IsRequired();

        builder.Property(p => p.Valor_Venda).IsRequired();

        builder.Property(p => p.UsrCadastro).IsRequired();

        builder.Property(p => p.DataCadastro).HasDefaultValueSql("getDate()").IsRequired();

        builder.HasOne(p => p.PrestacaoServico).WithMany(s => s.Produtos).HasForeignKey(f => f.IdProdutoEstoque);
    }
}
