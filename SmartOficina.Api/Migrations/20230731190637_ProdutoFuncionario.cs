using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartOficina.Api.Migrations
{
    /// <inheritdoc />
    public partial class ProdutoFuncionario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Ano",
                table: "Veiculo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TipoCombustivel",
                table: "Veiculo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data_validade",
                table: "Produto",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getDate()");

            migrationBuilder.AddColumn<int>(
                name: "Qtd",
                table: "Produto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProdutoPrestacaoServico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data_validade = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Garantia = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Valor_Venda = table.Column<float>(type: "real", nullable: false),
                    QtdVenda = table.Column<int>(type: "int", nullable: false),
                    IdProdutoEstoque = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PrestacaoServicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getDate()"),
                    DataDesativacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsrCadastro = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsrDesativacao = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoPrestacaoServico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdutoPrestacaoServico_PrestacaoServico_IdProdutoEstoque",
                        column: x => x.IdProdutoEstoque,
                        principalTable: "PrestacaoServico",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoPrestacaoServico_IdProdutoEstoque",
                table: "ProdutoPrestacaoServico",
                column: "IdProdutoEstoque");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProdutoPrestacaoServico");

            migrationBuilder.DropColumn(
                name: "Ano",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "TipoCombustivel",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "Qtd",
                table: "Produto");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data_validade",
                table: "Produto",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getDate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
