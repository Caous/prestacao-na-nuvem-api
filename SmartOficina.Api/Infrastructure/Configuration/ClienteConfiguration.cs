namespace SmartOficina.Api.Infrastructure.Mapping;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable(nameof(Cliente));

        builder.HasKey(k => k.Id);

        builder.Property(p => p.Id).HasValueGenerator<SequentialGuidValueGenerator>();

        builder.Property(p => p.Nome).HasMaxLength(100).IsRequired();

        builder.Property(p => p.Telefone).HasMaxLength(25);

        builder.Property(p => p.Email).HasMaxLength(125);

    }
}
