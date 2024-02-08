namespace PrestacaoNuvem.Seguranca.Infrastructure.Context;

public class SegurancaContext : IdentityDbContext<UserModel>
{
    public SegurancaContext(DbContextOptions<SegurancaContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMapper());

        base.OnModelCreating(modelBuilder);
    }

}