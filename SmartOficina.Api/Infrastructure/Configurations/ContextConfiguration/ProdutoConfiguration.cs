namespace SmartOficina.Api.Infrastructure.Configurations.ContextConfiguration;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable(nameof(Produto));

        builder.HasKey(k => k.Id);

        builder.Property(p => p.Id).HasValueGenerator<SequentialGuidValueGenerator>();

        builder.Property(p => p.Nome).HasMaxLength(100).IsRequired();

        builder.Property(p => p.Marca).HasMaxLength(25).IsRequired();

        builder.Property(p => p.Modelo).HasMaxLength(50).IsRequired();

        builder.Property(p => p.Valor_Compra).IsRequired();

        builder.Property(p => p.Valor_Venda).IsRequired();

        builder.Property(p => p.PrestadorId).IsRequired();

        builder.Property(p => p.UsrCadastro).IsRequired();

        builder.Property(p => p.DataCadastro).HasDefaultValueSql("getDate()").IsRequired();

        builder.HasOne(p => p.PrestacaoServico).WithMany(s => s.Produtos).HasForeignKey(f => f.PrestacaoServicoId).OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(p => p.Prestador).WithMany(s => s.Produtos).HasForeignKey(f => f.PrestadorId).OnDelete(DeleteBehavior.NoAction);

    }
}
