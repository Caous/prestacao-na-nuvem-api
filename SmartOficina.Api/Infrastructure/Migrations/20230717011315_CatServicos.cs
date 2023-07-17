using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartOficina.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CatServicos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cliente",
                keyColumn: "Id",
                keyValue: new Guid("2cdea58a-e3ab-4f86-bd9c-8689b671005d"));

            migrationBuilder.DeleteData(
                table: "Prestador",
                keyColumn: "Id",
                keyValue: new Guid("34fe7575-16ce-4bcd-8574-7e5ed368beeb"));

            migrationBuilder.AlterColumn<string>(
                name: "Placa",
                table: "Veiculo",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SubServicoId",
                table: "Servico",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.InsertData(
                table: "Cliente",
                columns: new[] { "Id", "DataCadastro", "Email", "Nome", "Telefone" },
                values: new object[] { new Guid("1333b7b8-ace7-4873-ad03-649cd8815499"), new DateTime(2023, 7, 17, 1, 13, 15, 826, DateTimeKind.Utc).AddTicks(4817), "testecliente@gmail.com", "Teste Cliente", null });

            migrationBuilder.InsertData(
                table: "Prestador",
                columns: new[] { "Id", "DataCadastro", "Nome" },
                values: new object[] { new Guid("66e26469-b800-43ac-b55b-7cdb7ccc6c83"), new DateTime(2023, 7, 17, 1, 13, 15, 826, DateTimeKind.Utc).AddTicks(4932), "Teste Prestador" });

            migrationBuilder.CreateIndex(
                name: "IX_Servico_SubServicoId",
                table: "Servico",
                column: "SubServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_SubServico_CategoriaId",
                table: "SubServico",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Servico_SubServico_SubServicoId",
                table: "Servico",
                column: "SubServicoId",
                principalTable: "SubServico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servico_SubServico_SubServicoId",
                table: "Servico");

            migrationBuilder.DropTable(
                name: "SubServico");

            migrationBuilder.DropTable(
                name: "CategoriaServico");

            migrationBuilder.DropIndex(
                name: "IX_Servico_SubServicoId",
                table: "Servico");

            migrationBuilder.DeleteData(
                table: "Cliente",
                keyColumn: "Id",
                keyValue: new Guid("1333b7b8-ace7-4873-ad03-649cd8815499"));

            migrationBuilder.DeleteData(
                table: "Prestador",
                keyColumn: "Id",
                keyValue: new Guid("66e26469-b800-43ac-b55b-7cdb7ccc6c83"));

            migrationBuilder.DropColumn(
                name: "SubServicoId",
                table: "Servico");

            migrationBuilder.AlterColumn<string>(
                name: "Placa",
                table: "Veiculo",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.InsertData(
                table: "Cliente",
                columns: new[] { "Id", "DataCadastro", "Email", "Nome", "Telefone" },
                values: new object[] { new Guid("2cdea58a-e3ab-4f86-bd9c-8689b671005d"), new DateTime(2023, 7, 15, 2, 7, 11, 882, DateTimeKind.Utc).AddTicks(9655), "testecliente@gmail.com", "Teste Cliente", null });

            migrationBuilder.InsertData(
                table: "Prestador",
                columns: new[] { "Id", "DataCadastro", "Nome" },
                values: new object[] { new Guid("34fe7575-16ce-4bcd-8574-7e5ed368beeb"), new DateTime(2023, 7, 15, 2, 7, 11, 882, DateTimeKind.Utc).AddTicks(9867), "Teste Prestador" });
        }
    }
}
