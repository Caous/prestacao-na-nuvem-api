
namespace PrestacaoNuvem.Api.Infrastructure.Configurations.ContextConfiguration;

public class FilialConfiguration : IEntityTypeConfiguration<Filial>
{
    public void Configure(EntityTypeBuilder<Filial> builder)
    {
        builder.ToTable(nameof(Filial));

        builder.HasKey(k => k.Id);

        builder.Property(p => p.Id).HasValueGenerator<SequentialGuidValueGenerator>();

        builder.Property(p => p.Observacao).HasMaxLength(300).IsRequired();
        
        builder.Property(p => p.CEP).HasMaxLength(15).IsRequired();

        builder.Property(p => p.IdGerenteFilial).IsRequired();

        builder.Property(p => p.Logradouro).HasMaxLength(300).IsRequired();
        
        builder.Property(p => p.Nome).HasMaxLength(150).IsRequired();

        builder.Property(p => p.Matriz).IsRequired();

        builder.Property(p => p.Numero).HasMaxLength(10).IsRequired();
        
        builder.Property(p => p.PrestadorId).IsRequired();

        builder.HasOne(p => p.Prestador).WithMany(s => s.Filiais).HasForeignKey(f => f.PrestadorId).OnDelete(DeleteBehavior.NoAction);
    }
}
