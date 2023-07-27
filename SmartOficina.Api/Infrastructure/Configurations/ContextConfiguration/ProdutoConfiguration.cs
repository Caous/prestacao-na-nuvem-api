﻿namespace SmartOficina.Api.Infrastructure.Configurations.ContextConfiguration;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable(nameof(Cliente));

        builder.HasKey(k => k.Id);

        builder.Property(p => p.Nome).HasMaxLength(100).IsRequired();

        builder.Property(p => p.Marca).HasMaxLength(25).IsRequired();

        builder.Property(p => p.Modelo).HasMaxLength(50).IsRequired();

        //builder.Property(p => p.Data_validade)

        builder.Property(p => p.Garantia).HasMaxLength(10).IsRequired();

        //builder.Property(p => p.Valor_Compra).HasMaxLength(100);
        //builder.Property(p => p.Valor_Venda).HasMaxLength(100);
    }
}
