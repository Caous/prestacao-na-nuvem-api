namespace SmartOficina.Api.Infrastructure.Configuration
{
    public class SubServicoConfiguration : IEntityTypeConfiguration<SubServico>
    {
        public void Configure(EntityTypeBuilder<SubServico> builder)
        {
            builder.ToTable(nameof(SubServico));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasValueGenerator<SequentialGuidValueGenerator>();

            builder.Property(x => x.Desc).HasMaxLength(200).IsRequired();

            builder.Property(x => x.Titulo).HasMaxLength(100).IsRequired();

            builder.HasMany(s => s.Servicos).WithOne(p => p.SubServico).HasForeignKey(f => f.SubServicoId);
        }
    }
}
