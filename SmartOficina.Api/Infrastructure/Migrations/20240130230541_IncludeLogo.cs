using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartOficina.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IncludeLogo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Prestador",
                type: "nvarchar(600)",
                maxLength: 600,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Prestador");
        }
    }
}
