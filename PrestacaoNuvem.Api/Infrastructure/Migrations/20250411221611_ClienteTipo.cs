using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrestacaoNuvem.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ClienteTipo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoCliente",
                table: "Cliente",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoCliente",
                table: "Cliente");
        }
    }
}
