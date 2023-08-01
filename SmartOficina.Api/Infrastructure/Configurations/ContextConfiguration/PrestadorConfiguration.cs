namespace SmartOficina.Api.Infrastructure.Configurations.ContextConfiguration;

public class PrestadorConfiguration : IEntityTypeConfiguration<Prestador>
{
    public void Configure(EntityTypeBuilder<Prestador> builder)
    {
        builder.ToTable(nameof(Prestador));

        builder.HasKey(k => k.Id);

        builder.Property(p => p.Id).HasValueGenerator<SequentialGuidValueGenerator>();
        builder.Property(p => p.TipoCadastro);
        builder.Property(p => p.Nome).HasMaxLength(100);
        builder.Property(p => p.CPF).HasMaxLength(11);
        builder.Property(p => p.CpfRepresentante).HasMaxLength(11);
        builder.Property(p => p.CNPJ).HasMaxLength(14);
        builder.Property(p => p.RazaoSocial).HasMaxLength(100);
        builder.Property(p => p.NomeFantasia).HasMaxLength(100);
        builder.Property(p => p.NomeRepresentante).HasMaxLength(100);
        builder.Property(p => p.Telefone).HasMaxLength(12);
        builder.Property(p => p.EmailEmpresa).HasMaxLength(50);
        builder.Property(p => p.Endereco).HasMaxLength(100);
        builder.Property(p => p.EmailRepresentante).HasMaxLength(50);
        builder.Property(p => p.SituacaoCadastral);
        builder.Property(p => p.UsrCadastro);
        builder.Property(p => p.DataCadastro).HasDefaultValueSql("getDate()");
    }
}
