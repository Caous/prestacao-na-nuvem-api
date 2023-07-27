namespace SmartOficina.Api.Infrastructure.Configurations.ContextConfiguration;
//ToDo: Colocar campos de usuario desativação, data desativação e usuario inclusão
public class SubServicoConfiguration : IEntityTypeConfiguration<SubServico>
{
    public void Configure(EntityTypeBuilder<SubServico> builder)
    {
        builder.ToTable(nameof(SubServico));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasValueGenerator<SequentialGuidValueGenerator>();

        builder.Property(x => x.Desc).HasMaxLength(200).IsRequired();

        builder.Property(x => x.Titulo).HasMaxLength(100).IsRequired();

        builder.HasOne(p => p.Categoria).WithMany(s => s.SubServicos).HasForeignKey(f => f.CategoriaId);

    }
}
