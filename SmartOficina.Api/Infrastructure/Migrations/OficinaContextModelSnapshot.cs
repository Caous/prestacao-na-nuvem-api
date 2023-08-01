﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartOficina.Api.Infrastructure.Context;

#nullable disable

namespace SmartOficina.Api.Infrastructure.Migrations
{
    [DbContext(typeof(OficinaContext))]
    partial class OficinaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence<int>("PrestacaoOrdem")
                .StartsAt(1000L);

            modelBuilder.Entity("SmartOficina.Api.Domain.Model.CategoriaServico", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataDesativacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Desc")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid>("PrestadorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("UsrCadastro")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UsrDesativacao")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PrestadorId");

                    b.ToTable("CategoriaServico", (string)null);
                });

            modelBuilder.Entity("SmartOficina.Api.Domain.Model.Cliente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<DateTime>("DataCadastro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getDate()");

                    b.Property<DateTime?>("DataDesativacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Endereco")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("PrestadorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Rg")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<Guid>("UsrCadastro")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UsrDesativacao")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PrestadorId");

                    b.ToTable("Cliente", (string)null);
                });

            modelBuilder.Entity("SmartOficina.Api.Domain.Model.FuncionarioPrestador", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Cargo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataCadastro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getDate()");

                    b.Property<DateTime?>("DataDesativacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("PrestadorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RG")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<Guid>("UsrCadastro")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UsrDesativacao")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PrestadorId");

                    b.ToTable("FuncionarioPrestador", (string)null);
                });

            modelBuilder.Entity("SmartOficina.Api.Domain.Model.PrestacaoServico", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ClienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataDesativacao")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PrestadorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Referencia")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValueSql("FORMAT((NEXT VALUE FOR PrestacaoOrdem), 'OS#')");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("UsrCadastro")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UsrDesativacao")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("VeiculoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("PrestadorId");

                    b.HasIndex("VeiculoId");

                    b.ToTable("PrestacaoServico", (string)null);
                });

            modelBuilder.Entity("SmartOficina.Api.Domain.Model.Prestador", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CNPJ")
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("CPF")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("CpfRepresentante")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<DateTime?>("DataAbertura")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCadastro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getDate()");

                    b.Property<DateTime?>("DataDesativacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataSituacaoCadastral")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailEmpresa")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("EmailRepresentante")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Endereco")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Nome")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NomeFantasia")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NomeRepresentante")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("RazaoSocial")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("SituacaoCadastral")
                        .HasColumnType("int");

                    b.Property<string>("Telefone")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<int>("TipoCadastro")
                        .HasColumnType("int");

                    b.Property<Guid>("UsrCadastro")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UsrDesativacao")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Prestador", (string)null);
                });

            modelBuilder.Entity("SmartOficina.Api.Domain.Model.Produto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCadastro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getDate()");

                    b.Property<DateTime?>("DataDesativacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Data_validade")
                        .HasColumnType("datetime2");

                    b.Property<string>("Garantia")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid?>("PrestacaoServicoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PrestadorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsrCadastro")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UsrDesativacao")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Valor_Compra")
                        .HasColumnType("real");

                    b.Property<float>("Valor_Venda")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("PrestacaoServicoId");

                    b.HasIndex("PrestadorId");

                    b.ToTable("Produto", (string)null);
                });

            modelBuilder.Entity("SmartOficina.Api.Domain.Model.Servico", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCadastro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getDate()");

                    b.Property<DateTime?>("DataDesativacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<Guid>("PrestacaoServicoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PrestadorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SubServicoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsrCadastro")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UsrDesativacao")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Valor")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("PrestacaoServicoId");

                    b.HasIndex("PrestadorId");

                    b.HasIndex("SubServicoId");

                    b.ToTable("Servico", (string)null);
                });

            modelBuilder.Entity("SmartOficina.Api.Domain.Model.SubCategoriaServico", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoriaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataDesativacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Desc")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("UsrCadastro")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UsrDesativacao")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("SubCategoriaServico", (string)null);
                });

            modelBuilder.Entity("SmartOficina.Api.Domain.Model.Veiculo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Ano")
                        .HasColumnType("int");

                    b.Property<string>("Chassi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataCadastro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getDate()");

                    b.Property<DateTime?>("DataDesativacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<Guid>("PrestadorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.Property<string>("TipoCombustivel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UsrCadastro")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UsrDesativacao")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PrestadorId");

                    b.ToTable("Veiculo", (string)null);
                });

            modelBuilder.Entity("SmartOficina.Api.Domain.Model.CategoriaServico", b =>
                {
                    b.HasOne("SmartOficina.Api.Domain.Model.Prestador", "Prestador")
                        .WithMany("CategoriaServicos")
                        .HasForeignKey("PrestadorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Prestador");
                });

            modelBuilder.Entity("SmartOficina.Api.Domain.Model.Cliente", b =>
                {
                    b.HasOne("SmartOficina.Api.Domain.Model.Prestador", "Prestador")
                        .WithMany("Clientes")
                        .HasForeignKey("PrestadorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Prestador");
                });

            modelBuilder.Entity("SmartOficina.Api.Domain.Model.FuncionarioPrestador", b =>
                {
                    b.HasOne("SmartOficina.Api.Domain.Model.Prestador", "Prestador")
                        .WithMany("Funcionarios")
                        .HasForeignKey("PrestadorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Prestador");
                });

            modelBuilder.Entity("SmartOficina.Api.Domain.Model.PrestacaoServico", b =>
                {
                    b.HasOne("SmartOficina.Api.Domain.Model.Cliente", "Cliente")
                        .WithMany("Servicos")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SmartOficina.Api.Domain.Model.Prestador", "Prestador")
                        .WithMany("OrdemServicos")
                        .HasForeignKey("PrestadorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SmartOficina.Api.Domain.Model.Veiculo", "Veiculo")
                        .WithMany("Servicos")
                        .HasForeignKey("VeiculoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Cliente");

                    b.Navigation("Prestador");

                    b.Navigation("Veiculo");
                });

            modelBuilder.Entity("SmartOficina.Api.Domain.Model.Produto", b =>
                {
                    b.HasOne("SmartOficina.Api.Domain.Model.PrestacaoServico", "PrestacaoServico")
                        .WithMany("Produtos")
                        .HasForeignKey("PrestacaoServicoId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("SmartOficina.Api.Domain.Model.Prestador", "Prestador")
                        .WithMany("Produtos")
                        .HasForeignKey("PrestadorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("PrestacaoServico");

                    b.Navigation("Prestador");
                });

            modelBuilder.Entity("SmartOficina.Api.Domain.Model.Servico", b =>
                {
                    b.HasOne("SmartOficina.Api.Domain.Model.PrestacaoServico", "PrestacaoServico")
                        .WithMany("Servicos")
                        .HasForeignKey("PrestacaoServicoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SmartOficina.Api.Domain.Model.Prestador", "Prestador")
                        .WithMany("Servicos")
                        .HasForeignKey("PrestadorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SmartOficina.Api.Domain.Model.SubCategoriaServico", "SubCategoriaServico")
                        .WithMany("Servicos")
                        .HasForeignKey("SubServicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PrestacaoServico");

                    b.Navigation("Prestador");

                    b.Navigation("SubCategoriaServico");
                });

            modelBuilder.Entity("SmartOficina.Api.Domain.Model.SubCategoriaServico", b =>
                {
                    b.HasOne("SmartOficina.Api.Domain.Model.CategoriaServico", "Categoria")
                        .WithMany("SubCategoriaServicos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("SmartOficina.Api.Domain.Model.Veiculo", b =>
                {
                    b.HasOne("SmartOficina.Api.Domain.Model.Prestador", "Prestador")
                        .WithMany("Veiculos")
                        .HasForeignKey("PrestadorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Prestador");
                });

            modelBuilder.Entity("SmartOficina.Api.Domain.Model.CategoriaServico", b =>
                {
                    b.Navigation("SubCategoriaServicos");
                });

            modelBuilder.Entity("SmartOficina.Api.Domain.Model.Cliente", b =>
                {
                    b.Navigation("Servicos");
                });

            modelBuilder.Entity("SmartOficina.Api.Domain.Model.PrestacaoServico", b =>
                {
                    b.Navigation("Produtos");

                    b.Navigation("Servicos");
                });

            modelBuilder.Entity("SmartOficina.Api.Domain.Model.Prestador", b =>
                {
                    b.Navigation("CategoriaServicos");

                    b.Navigation("Clientes");

                    b.Navigation("Funcionarios");

                    b.Navigation("OrdemServicos");

                    b.Navigation("Produtos");

                    b.Navigation("Servicos");

                    b.Navigation("Veiculos");
                });

            modelBuilder.Entity("SmartOficina.Api.Domain.Model.SubCategoriaServico", b =>
                {
                    b.Navigation("Servicos");
                });

            modelBuilder.Entity("SmartOficina.Api.Domain.Model.Veiculo", b =>
                {
                    b.Navigation("Servicos");
                });
#pragma warning restore 612, 618
        }
    }
}
