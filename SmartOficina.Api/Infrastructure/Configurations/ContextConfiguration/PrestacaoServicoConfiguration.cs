namespace SmartOficina.Api.Infrastructure.Configurations.ContextConfiguration;

public class PrestacaoServicoConfiguration : IEntityTypeConfiguration<PrestacaoServico>
{
    public void Configure(EntityTypeBuilder<PrestacaoServico> builder)
    {
        builder.ToTable(nameof(PrestacaoServico));

        builder.HasKey(k => k.Id);

        builder.Property(p => p.Id).HasValueGenerator<SequentialGuidValueGenerator>();

        builder.Property(p => p.Status).IsRequired().HasConversion<int>();

        builder.Property(p => p.Referencia).IsRequired()
            .HasDefaultValueSql("FORMAT((NEXT VALUE FOR PrestacaoOrdem), 'OS#')");

        builder.HasOne(p => p.Prestador).WithMany(s => s.Servicos).HasForeignKey(f => f.PrestadorId);

        builder.HasOne(c => c.Cliente).WithMany(s => s.Servicos).HasForeignKey(f => f.ClienteId);

        builder.HasOne(v => v.Veiculo).WithMany(s => s.Servicos).HasForeignKey(f => f.VeiculoId);

        builder.HasMany(s => s.Servicos).WithOne(p => p.PrestacaoServico).HasForeignKey(f => f.PrestacaoServicoId);
    }
}
