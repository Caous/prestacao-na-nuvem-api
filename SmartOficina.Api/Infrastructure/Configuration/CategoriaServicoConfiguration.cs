namespace SmartOficina.Api.Infrastructure.Configuration;

public class CategoriaServicoConfiguration : IEntityTypeConfiguration<CategoriaServico>
{
    public void Configure(EntityTypeBuilder<CategoriaServico> builder)
    {
        builder.ToTable(nameof(Servico));

        builder.HasKey(k => k.Id);

        builder.Property(p => p.Id).HasValueGenerator<SequentialGuidValueGenerator>();

        builder.Property(p => p.Titulo).HasMaxLength(100).IsRequired();

        builder.Property(p => p.Desc).HasMaxLength(200).IsRequired();

        builder.HasMany(s => s.SubServicos).WithOne(p => p.Categoria).HasForeignKey(f => f.CategoriaId);
    }
}
