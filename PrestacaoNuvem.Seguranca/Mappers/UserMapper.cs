namespace PrestacaoNuvem.Seguranca.Mappers;

public class UserMapper : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.ToTable("User");

        builder.Property(p => p.Email)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(p => p.UserName)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(p => p.DataCadastro)
            .IsRequired()
            .HasColumnType("DateTime")
            .HasDefaultValueSql("GETDATE()");
    }
}
