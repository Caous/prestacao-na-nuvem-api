namespace SmartOficina.Api.Infrastructure.Configurations.ContextConfiguration;

public class ServicoConfiguration : IEntityTypeConfiguration<Servico>
{
    public void Configure(EntityTypeBuilder<Servico> builder)
    {
        builder.ToTable(nameof(Servico));

        builder.HasKey(k => k.Id);

        builder.Property(p => p.Id).HasValueGenerator<SequentialGuidValueGenerator>();

        builder.Property(p => p.Nome).IsRequired().HasMaxLength(250);

        builder.Property(p => p.Valor).IsRequired();

        builder.HasOne(p => p.SubServico).WithMany(s => s.Servicos).HasForeignKey(f => f.SubServicoId);

    }
}
