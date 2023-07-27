using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartOficina.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "PrestacaoOrdem",
                startValue: 1000L);

            migrationBuilder.CreateTable(
                name: "CategoriaServico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaServico", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true),
                    RG = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prestador",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CNPJ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Razao_Social = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome_Fantasia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Representante = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestador", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Veiculo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Placa = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubServico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubServico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubServico_CategoriaServico_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "CategoriaServico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrestacaoServico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrestacaoServico_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PrestacaoServico_Prestador_PrestadorId",
                        column: x => x.PrestadorId,
                        principalTable: "Prestador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrestacaoServico_Veiculo_VeiculoId",
                        column: x => x.VeiculoId,
                        principalTable: "Veiculo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Servico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Valor = table.Column<float>(type: "real", nullable: false),
                    SubServicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrestacaoServicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servico_PrestacaoServico_PrestacaoServicoId",
                        column: x => x.PrestacaoServicoId,
                        principalTable: "PrestacaoServico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Servico_SubServico_SubServicoId",
                        column: x => x.SubServicoId,
                        principalTable: "SubServico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CategoriaServico",
                columns: new[] { "Id", "DataCadastro", "Desc", "Titulo" },
                values: new object[,]
                {
                    { new Guid("5dfb6bc2-e519-47b8-aac3-8840358d4509"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Serviço gerais na parte de motor do veículo", "Motor" },
                    { new Guid("f2c53a84-591c-4ff6-84e2-ca15102f4c3c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Serviços na parte de suspensão/geometria", "Suspensão" }
                });

            migrationBuilder.InsertData(
                table: "Cliente",
                columns: new[] { "Id", "CPF", "DataCadastro", "Email", "Endereco", "Nome", "RG", "Telefone" },
                values: new object[] { new Guid("ddf70c56-46b3-4803-9618-0c18b72347f3"), "000987565", new DateTime(2023, 7, 27, 1, 10, 19, 270, DateTimeKind.Utc).AddTicks(8440), "testecliente@gmail.com", "Rua Cel Barroso", "Teste Cliente", "12345677890", null });

            migrationBuilder.InsertData(
                table: "Prestador",
                columns: new[] { "Id", "CNPJ", "CPF", "DataCadastro", "Email", "Endereco", "Nome", "Nome_Fantasia", "Razao_Social", "Representante", "Telefone" },
                values: new object[] { new Guid("612517ba-1511-443a-b7e8-48688ee0d056"), "000987565987", "000987565", new DateTime(2023, 7, 27, 1, 10, 19, 270, DateTimeKind.Utc).AddTicks(8684), null, "Portal Morumbi", "Teste Prestador", "Teste Regis", "Teste Novo", "Regis", null });

            migrationBuilder.InsertData(
                table: "Veiculo",
                columns: new[] { "Id", "DataCadastro", "Marca", "Modelo", "Placa", "Tipo" },
                values: new object[,]
                {
                    { new Guid("19422c16-6edd-47b1-bd67-042735f17aa0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chevrolet", "Agile", "AAA-1234", 0 },
                    { new Guid("8ab6d92b-1a4f-407c-af69-e9160979b89a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chevrolet", "Celta", "CCC-1234", 0 },
                    { new Guid("d3b04b36-ca04-4d93-854a-dcb7178808f6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hyundai", "I30", "BBB-1234", 0 }
                });

            migrationBuilder.InsertData(
                table: "SubServico",
                columns: new[] { "Id", "CategoriaId", "DataCadastro", "Desc", "Titulo" },
                values: new object[,]
                {
                    { new Guid("45f602b4-6984-4844-840e-1e981812411e"), new Guid("f2c53a84-591c-4ff6-84e2-ca15102f4c3c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Troca da peça", "Troca Amortecedor" },
                    { new Guid("4a4df18a-be7b-4ef1-823a-3efead301206"), new Guid("5dfb6bc2-e519-47b8-aac3-8840358d4509"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bloco condenado/Sem retífica, troca por um novo", "Troca bloco" },
                    { new Guid("661d0750-179a-4d51-8f1d-9d45bc010a9e"), new Guid("f2c53a84-591c-4ff6-84e2-ca15102f4c3c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Troca da peça", "Troca bandeja" },
                    { new Guid("6fd9108f-cb00-4d56-963b-98393654d6e4"), new Guid("5dfb6bc2-e519-47b8-aac3-8840358d4509"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Troca de todos os pistões", "Troca pistão" }
                });

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
                name: "IX_Servico_PrestacaoServicoId",
                table: "Servico",
                column: "PrestacaoServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Servico_SubServicoId",
                table: "Servico",
                column: "SubServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_SubServico_CategoriaId",
                table: "SubServico",
                column: "CategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Servico");

            migrationBuilder.DropTable(
                name: "PrestacaoServico");

            migrationBuilder.DropTable(
                name: "SubServico");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Prestador");

            migrationBuilder.DropTable(
                name: "Veiculo");

            migrationBuilder.DropTable(
                name: "CategoriaServico");

            migrationBuilder.DropSequence(
                name: "PrestacaoOrdem");
        }
    }
}
