using Microsoft.EntityFrameworkCore;
using SmartOficina.Api.Domain;
using SmartOficina.Api.Infrastructure.Configuration;
using SmartOficina.Api.Infrastructure.Mapping;

namespace SmartOficina.Api.Infrastructure.Context
{
    public class OficinaContext : DbContext
    {
        public OficinaContext(DbContextOptions<OficinaContext> context) : base(context)
        {
            
        }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<PrestacaoServico> PrestacaoServico { get; set; }
        public DbSet<Servico> Servico { get; set; }
        public DbSet<Prestador> Prestador { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration(new PrestacaoServicoConfiguration());
            modelBuilder.ApplyConfiguration(new PrestadorConfiguration());
            modelBuilder.ApplyConfiguration(new ServicoConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
