using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartOficina.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "PrestacaoOrdem",
                startValue: 1000L);

            migrationBuilder.CreateTable(
                name: "Prestador",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoCadastro = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CPF = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    CpfRepresentante = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    CNPJ = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    RazaoSocial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NomeFantasia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NomeRepresentante = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    EmailEmpresa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Endereco = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EmailRepresentante = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SituacaoCadastral = table.Column<int>(type: "int", nullable: false),
                    DataAbertura = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataSituacaoCadastral = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getDate()"),
                    DataDesativacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsrCadastro = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsrDesativacao = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestador", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAutentications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDesativacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsrCadastro = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsrDescricaoCadastro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsrDesativacao = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UsrDescricaoDesativacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAutentications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoriaServico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PrestadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDesativacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsrCadastro = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsrDesativacao = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaServico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoriaServico_Prestador_PrestadorId",
                        column: x => x.PrestadorId,
                        principalTable: "Prestador",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Rg = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PrestadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getDate()"),
                    DataDesativacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsrCadastro = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsrDesativacao = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cliente_Prestador_PrestadorId",
                        column: x => x.PrestadorId,
                        principalTable: "Prestador",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FuncionarioPrestador",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrestadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RG = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cargo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getDate()"),
                    DataDesativacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsrCadastro = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsrDesativacao = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncionarioPrestador", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FuncionarioPrestador_Prestador_PrestadorId",
                        column: x => x.PrestadorId,
                        principalTable: "Prestador",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Veiculo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Placa = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Chassi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    PrestadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    TipoCombustivel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getDate()"),
                    DataDesativacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsrCadastro = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsrDesativacao = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Veiculo_Prestador_PrestadorId",
                        column: x => x.PrestadorId,
                        principalTable: "Prestador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubCategoriaServico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDesativacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsrCadastro = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsrDesativacao = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategoriaServico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategoriaServico_CategoriaServico_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "CategoriaServico",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PrestacaoServico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Referencia = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "FORMAT((NEXT VALUE FOR PrestacaoOrdem), 'OS#')"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PrestadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VeiculoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDesativacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsrCadastro = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsrDesativacao = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrestacaoServico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrestacaoServico_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrestacaoServico_Prestador_PrestadorId",
                        column: x => x.PrestadorId,
                        principalTable: "Prestador",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PrestacaoServico_Veiculo_VeiculoId",
                        column: x => x.VeiculoId,
                        principalTable: "Veiculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Data_validade = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Garantia = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Valor_Compra = table.Column<float>(type: "real", nullable: false),
                    Valor_Venda = table.Column<float>(type: "real", nullable: false),
                    PrestadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrestacaoServicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getDate()"),
                    DataDesativacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsrCadastro = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsrDesativacao = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produto_PrestacaoServico_PrestacaoServicoId",
                        column: x => x.PrestacaoServicoId,
                        principalTable: "PrestacaoServico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Produto_Prestador_PrestadorId",
                        column: x => x.PrestadorId,
                        principalTable: "Prestador",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Servico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrestadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Valor = table.Column<float>(type: "real", nullable: false),
                    SubServicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrestacaoServicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getDate()"),
                    DataDesativacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsrCadastro = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsrDesativacao = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servico_PrestacaoServico_PrestacaoServicoId",
                        column: x => x.PrestacaoServicoId,
                        principalTable: "PrestacaoServico",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Servico_Prestador_PrestadorId",
                        column: x => x.PrestadorId,
                        principalTable: "Prestador",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Servico_SubCategoriaServico_SubServicoId",
                        column: x => x.SubServicoId,
                        principalTable: "SubCategoriaServico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoriaServico_PrestadorId",
                table: "CategoriaServico",
                column: "PrestadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_PrestadorId",
                table: "Cliente",
                column: "PrestadorId");

            migrationBuilder.CreateIndex(
                name: "IX_FuncionarioPrestador_PrestadorId",
                table: "FuncionarioPrestador",
                column: "PrestadorId");

            migrationBuilder.CreateIndex(
                name: "IX_PrestacaoServico_ClienteId",
                table: "PrestacaoServico",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_PrestacaoServico_PrestadorId",
                table: "PrestacaoServico",
                column: "PrestadorId");

            migrationBuilder.CreateIndex(
                name: "IX_PrestacaoServico_VeiculoId",
                table: "PrestacaoServico",
                column: "VeiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_PrestacaoServicoId",
                table: "Produto",
                column: "PrestacaoServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_PrestadorId",
                table: "Produto",
                column: "PrestadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Servico_PrestacaoServicoId",
                table: "Servico",
                column: "PrestacaoServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Servico_PrestadorId",
                table: "Servico",
                column: "PrestadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Servico_SubServicoId",
                table: "Servico",
                column: "SubServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoriaServico_CategoriaId",
                table: "SubCategoriaServico",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_PrestadorId",
                table: "Veiculo",
                column: "PrestadorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FuncionarioPrestador");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Servico");

            migrationBuilder.DropTable(
                name: "UserAutentications");

            migrationBuilder.DropTable(
                name: "PrestacaoServico");

            migrationBuilder.DropTable(
                name: "SubCategoriaServico");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Veiculo");

            migrationBuilder.DropTable(
                name: "CategoriaServico");

            migrationBuilder.DropTable(
                name: "Prestador");

            migrationBuilder.DropSequence(
                name: "PrestacaoOrdem");
        }
    }
}
