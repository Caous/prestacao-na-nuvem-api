using Microsoft.EntityFrameworkCore;

namespace SmartOficina.Api.Infrastructure.Configurations.ContextConfiguration
{
    public class FuncionarioPrestadorConfiguration : IEntityTypeConfiguration<FuncionarioPrestador>
    {
        public void Configure(EntityTypeBuilder<FuncionarioPrestador> builder)
        {
            builder.ToTable(nameof(FuncionarioPrestador));

            builder.HasKey(k => k.Id);

            //builder.Property(p => p.Id_Prestador).HasValueGenerator<SequentialGuidValueGenerator>();

            builder.Property(p => p.Nome).HasMaxLength(100).IsRequired();

            builder.Property(p => p.Telefone).HasMaxLength(25);

            builder.Property(p => p.Email).HasMaxLength(50).IsRequired();

            builder.Property(p => p.RG).HasMaxLength(10).IsRequired();

            builder.Property(p => p.CPF).HasMaxLength(11).IsRequired();

            builder.Property(p => p.Endereco).HasMaxLength(100);

            //Todo: Validar se tem empresa associada
            //builder.Property(p => p.Empresa_Associada);
        }
    }
}
