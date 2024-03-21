namespace PrestacaoNuvem.Api.Infrastructure.Configurations.ContextConfiguration;

public class OrdemVendaConfiguration : IEntityTypeConfiguration<OrdemVenda>
{
    public void Configure(EntityTypeBuilder<OrdemVenda> builder)
    {
        builder.ToTable(nameof(OrdemVenda));

        builder.HasKey(k => k.Id);

        builder.Property(p => p.Id).HasValueGenerator<SequentialGuidValueGenerator>();

        builder.Property(p => p.Status).IsRequired().HasConversion<int>();

        builder.Property(p => p.Referencia).IsRequired()
            .HasDefaultValueSql("FORMAT((NEXT VALUE FOR VendaOrdem), 'OV#')");

        builder.Property(p => p.CPF).HasMaxLength(15);

        builder.Property(p => p.FuncionarioPrestadorId).IsRequired();

        builder.Property(p => p.PrestadorId).IsRequired();

        builder.HasOne(p => p.Prestador).WithMany(s => s.OrdemVendas).HasForeignKey(f => f.PrestadorId).OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(p => p.FuncionarioPrestador).WithMany(s => s.OrdemVendas).HasForeignKey(f => f.FuncionarioPrestadorId).OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(c => c.Cliente).WithMany(s => s.OrdemVendas).HasForeignKey(f => f.ClienteId).OnDelete(DeleteBehavior.Restrict);
     
    }
}
