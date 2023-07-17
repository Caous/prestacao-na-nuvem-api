namespace SmartOficina.Api.Infrastructure.Configurations.ContextConfiguration;

public class PrestadorConfiguration : IEntityTypeConfiguration<Prestador>
{
    public void Configure(EntityTypeBuilder<Prestador> builder)
    {
        builder.ToTable(nameof(Prestador));

        builder.HasKey(k => k.Id);

        builder.Property(p => p.Id).HasValueGenerator<SequentialGuidValueGenerator>();

        builder.Property(p => p.Nome).HasMaxLength(100).IsRequired();
    }
}
