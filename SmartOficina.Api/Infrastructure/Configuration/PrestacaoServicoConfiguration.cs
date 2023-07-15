using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using SmartOficina.Api.Domain;

namespace SmartOficina.Api.Infrastructure.Configuration
{
    public class PrestacaoServicoConfiguration : IEntityTypeConfiguration<PrestacaoServico>
    {
        public void Configure(EntityTypeBuilder<PrestacaoServico> builder)
        {
            builder.ToTable(nameof(PrestacaoServico));

            builder.HasKey(k => k.Id);

            builder.Property(p => p.Id).HasValueGenerator<SequentialGuidValueGenerator>();

            builder.Property(p => p.Status).IsRequired().HasConversion<int>();

            builder.HasOne(p => p.Prestador).WithMany(s => s.Servicos).HasForeignKey(f => f.PrestadorId);

            builder.HasOne(c => c.Cliente).WithMany(s => s.Servicos).HasForeignKey(f => f.ClienteId);

            builder.HasMany(s => s.Servicos).WithOne(p => p.PrestacaoServico).HasForeignKey(f => f.PrestacaoServicoId);
        }
    }
}
