using Microsoft.EntityFrameworkCore.Design;

namespace SmartOficina.Api.Infrastructure.Context;

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
    public DbSet<SubCategoriaServico> SubCategoriaServico { get; set; }
    public DbSet<CategoriaServico> CategoriaServico { get; set; }
    public DbSet<Produto> ProdutoEstoque { get; set; }
    public DbSet<FuncionarioPrestador> FuncionarioPrestador { get; set; }
    public DbSet<Produto> Produto { get; set; }
    public DbSet<UserAutentication> UserAutentications { get; set; }    

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
           .Entries()
           .Where(e => e.Entity is Domain.Model.Base && (
                   e.State == EntityState.Added));

        foreach (var entityEntry in entries)
        {
            ((Domain.Model.Base)entityEntry.Entity).DataCadastro = DateTime.UtcNow;
        }
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OficinaContext).Assembly);
        
        modelBuilder.HasSequence<int>("PrestacaoOrdem").StartsAt(1000).IncrementsBy(1);

        base.OnModelCreating(modelBuilder);
    }

    public class OficinaDbContextFactory : IDesignTimeDbContextFactory<OficinaContext>
    {
        public OficinaContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OficinaContext>();
            optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=Oficina;Integrated Security=False;User ID=sa;Password=yourStrong(!)Password;MultipleActiveResultSets=True;TrustServerCertificate=true");

            return new OficinaContext(optionsBuilder.Options);
        }
    }
}
