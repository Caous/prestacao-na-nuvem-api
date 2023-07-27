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
    public DbSet<SubServico> SubServico { get; set; }
    public DbSet<CategoriaServico> CategoriaServico { get; set; }

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
        modelBuilder.ApplyConfiguration(new CategoriaServicoConfiguration());
        modelBuilder.ApplyConfiguration(new SubServicoConfiguration());


        modelBuilder.HasSequence<int>("PrestacaoOrdem").StartsAt(1000).IncrementsBy(1);

        modelBuilder.Entity<Cliente>().HasData(new Cliente() { Id = Guid.NewGuid(), DataCadastro = DateTime.UtcNow, Nome = "Teste Cliente", Email = "testecliente@gmail.com", Telefone = "56874877", Rg = "12345677890", CPF ="000987565", Endereco = "Rua Cel Barroso" });
        modelBuilder.Entity<Prestador>().HasData(new Prestador() { Id = Guid.NewGuid(), DataCadastro = DateTime.UtcNow, Nome = "Teste Prestador", CPF = "000987565", CNPJ = "000987565987", Razao_Social= "Teste Novo", Nome_Fantasia= "Teste Regis", Representante = "Regis", Endereco = "Portal Morumbi" });
        var guidSusp = Guid.NewGuid();
        var guidMotor = Guid.NewGuid();
        modelBuilder.Entity<CategoriaServico>().HasData(new CategoriaServico[]
        {
            new CategoriaServico()
            {
                Id = guidSusp,
                Titulo = "Suspensão",
                Desc = "Serviços na parte de suspensão/geometria"
            },
            new CategoriaServico
            {
                Id = guidMotor,
                Titulo = "Motor",
                Desc = "Serviço gerais na parte de motor do veículo"
            },
        });

        modelBuilder.Entity<SubServico>().HasData(new SubServico()
        {
            Id = Guid.NewGuid(),
            Titulo = "Troca bandeja",
            Desc = "Troca da peça",
            CategoriaId = guidSusp
        });

        modelBuilder.Entity<SubServico>().HasData(new SubServico()
        {
            Id = Guid.NewGuid(),
            Titulo = "Troca Amortecedor",
            Desc = "Troca da peça",
            CategoriaId = guidSusp
        });

        modelBuilder.Entity<SubServico>().HasData(new SubServico()
        {
            Id = Guid.NewGuid(),
            Titulo = "Troca pistão",
            Desc = "Troca de todos os pistões",
            CategoriaId = guidMotor
        });

        modelBuilder.Entity<SubServico>().HasData(new SubServico()
        {
            Id = Guid.NewGuid(),
            Titulo = "Troca bloco",
            Desc = "Bloco condenado/Sem retífica, troca por um novo",
            CategoriaId = guidMotor
        });

        modelBuilder.Entity<Veiculo>().HasData(new Veiculo[] {
            new Veiculo() { Id = Guid.NewGuid(), Marca = "Chevrolet", Modelo = "Agile", Placa = "AAA-1234" },
            new Veiculo() { Id = Guid.NewGuid(), Marca = "Hyundai", Modelo = "I30", Placa = "BBB-1234" },
            new Veiculo() { Id = Guid.NewGuid(), Marca = "Chevrolet", Modelo = "Celta", Placa = "CCC-1234" },
        });

        base.OnModelCreating(modelBuilder);
    }
}
