namespace SmartOficina.Api.Infrastructure.Configurations.ContextConfiguration;
//ToDo: Colocar campos de usuario desativação, data desativação e usuario inclusão
public class VeiculoConfiguration : IEntityTypeConfiguration<Veiculo>
{
    public void Configure(EntityTypeBuilder<Veiculo> builder)
    {
        builder.ToTable(nameof(Veiculo));

        builder.HasKey(k => k.Id);

        builder.Property(p => p.Id).HasValueGenerator<SequentialGuidValueGenerator>();

        builder.Property(p => p.Placa).HasMaxLength(15);

        builder.Property(p => p.Marca).HasMaxLength(35).IsRequired();

        builder.Property(p => p.Modelo).HasMaxLength(100).IsRequired();

        builder.Property(p => p.Tipo).IsRequired();
    }
}
