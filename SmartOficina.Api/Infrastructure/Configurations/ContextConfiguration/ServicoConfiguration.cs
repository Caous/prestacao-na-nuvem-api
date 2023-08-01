namespace SmartOficina.Api.Infrastructure.Configurations.ContextConfiguration;
public class ServicoConfiguration : IEntityTypeConfiguration<Servico>
{
    public void Configure(EntityTypeBuilder<Servico> builder)
    {
        builder.ToTable(nameof(Servico));

        builder.HasKey(k => k.Id);

        builder.Property(p => p.Id).HasValueGenerator<SequentialGuidValueGenerator>();

        builder.Property(p => p.Descricao).IsRequired().HasMaxLength(250);

        builder.Property(p => p.Valor).IsRequired();

        builder.HasOne(p => p.SubCategoriaServico).WithMany(s => s.Servicos).HasForeignKey(f => f.SubServicoId);

        builder.Property(p => p.PrestadorId).IsRequired();

        builder.Property(p => p.UsrCadastro).IsRequired();

        builder.Property(p => p.DataCadastro).HasDefaultValueSql("getDate()").IsRequired();

        builder.HasOne(p => p.Prestador).WithMany(s => s.Servicos).HasForeignKey(f => f.PrestadorId).OnDelete(DeleteBehavior.NoAction);

    }
}
