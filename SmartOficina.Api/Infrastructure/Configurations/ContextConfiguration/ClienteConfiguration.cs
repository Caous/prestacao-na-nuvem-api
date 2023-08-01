namespace SmartOficina.Api.Infrastructure.Configurations.ContextConfiguration;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable(nameof(Cliente));

        builder.HasKey(k => k.Id);

        builder.Property(p => p.Id).HasValueGenerator<SequentialGuidValueGenerator>();

        builder.Property(p => p.Nome).HasMaxLength(100).IsRequired();

        builder.Property(p => p.Telefone).HasMaxLength(25);

        builder.Property(p => p.Email).HasMaxLength(50).IsRequired();

        builder.Property(p => p.Rg).HasMaxLength(10).IsRequired();

        builder.Property(p => p.CPF).HasMaxLength(11).IsRequired();

        builder.Property(p => p.Endereco).HasMaxLength(100);

        builder.Property(p => p.PrestadorId).IsRequired();

        builder.Property(p => p.UsrCadastro).IsRequired();

        builder.Property(p => p.DataCadastro).HasDefaultValueSql("getDate()").IsRequired();

        builder.HasOne(p => p.Prestador).WithMany(s => s.Clientes).HasForeignKey(f => f.PrestadorId).OnDelete(DeleteBehavior.NoAction);
    }
}
