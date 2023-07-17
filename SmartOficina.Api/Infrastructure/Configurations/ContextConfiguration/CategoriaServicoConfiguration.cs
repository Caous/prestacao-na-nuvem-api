namespace SmartOficina.Api.Infrastructure.Configurations.ContextConfiguration;

public class CategoriaServicoConfiguration : IEntityTypeConfiguration<CategoriaServico>
{
    public void Configure(EntityTypeBuilder<CategoriaServico> builder)
    {
        builder.ToTable(nameof(CategoriaServico));

        builder.HasKey(k => k.Id);

        builder.Property(p => p.Id).HasValueGenerator<SequentialGuidValueGenerator>();

        builder.Property(p => p.Titulo).HasMaxLength(100).IsRequired();

        builder.Property(p => p.Desc).HasMaxLength(200).IsRequired();

    }
}
