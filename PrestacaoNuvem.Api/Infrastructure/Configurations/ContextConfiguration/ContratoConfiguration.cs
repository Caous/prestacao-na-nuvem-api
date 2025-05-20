namespace PrestacaoNuvem.Api.Infrastructure.Configurations.ContextConfiguration;

public class ContratoConfiguration : IEntityTypeConfiguration<Contrato>
{
    public void Configure(EntityTypeBuilder<Contrato> builder)
    {
        builder.ToTable(nameof(Contrato));

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).HasValueGenerator<SequentialGuidValueGenerator>();

        builder.Property(c => c.ClienteId).IsRequired();

        builder.Property(c => c.DataCadastro)
               .IsRequired()
               .HasColumnType("datetime")
               .HasDefaultValueSql("GETDATE()");


        builder.Property(c => c.Valor)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        builder.Property(c => c.Status)
               .IsRequired()
               .HasMaxLength(50);

        builder.HasOne(c => c.Cliente)
               .WithMany(c => c.Contratos)
               .HasForeignKey(c => c.ClienteId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
