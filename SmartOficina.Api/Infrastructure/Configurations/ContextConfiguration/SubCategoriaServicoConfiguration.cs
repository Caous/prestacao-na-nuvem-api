namespace SmartOficina.Api.Infrastructure.Configurations.ContextConfiguration;
public class SubCategoriaServicoConfiguration : IEntityTypeConfiguration<SubCategoriaServico>
{
    public void Configure(EntityTypeBuilder<SubCategoriaServico> builder)
    {
        builder.ToTable(nameof(SubCategoriaServico));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasValueGenerator<SequentialGuidValueGenerator>();

        builder.Property(x => x.Desc).HasMaxLength(200).IsRequired();

        builder.Property(x => x.Titulo).HasMaxLength(100).IsRequired();

        builder.HasOne(p => p.Categoria).WithMany(s => s.SubCategoriaServicos).HasForeignKey(f => f.CategoriaId).OnDelete(DeleteBehavior.NoAction);


    }
}
