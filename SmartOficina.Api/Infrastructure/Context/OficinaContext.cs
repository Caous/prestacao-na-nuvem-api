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
        public DbSet<Veiculo> Veiculo { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
               .Entries()
               .Where(e => e.Entity is Base && (
                       e.State == EntityState.Added));

            foreach (var entityEntry in entries)
            {
                ((Base)entityEntry.Entity).DataCadastro = DateTime.UtcNow;
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration(new PrestacaoServicoConfiguration());
            modelBuilder.ApplyConfiguration(new PrestadorConfiguration());
            modelBuilder.ApplyConfiguration(new ServicoConfiguration());
            modelBuilder.ApplyConfiguration(new VeiculoConfiguration());

            modelBuilder.HasSequence<int>("PrestacaoOrdem").StartsAt(1000).IncrementsBy(1);

            modelBuilder.Entity<Cliente>().HasData(new Cliente() { Id = Guid.NewGuid(), DataCadastro = DateTime.UtcNow, Nome = "Teste Cliente", Email = "testecliente@gmail.com" });
            modelBuilder.Entity<Prestador>().HasData(new Prestador() { Id = Guid.NewGuid(), DataCadastro = DateTime.UtcNow, Nome = "Teste Prestador"});

            base.OnModelCreating(modelBuilder);
        }
    }
}
