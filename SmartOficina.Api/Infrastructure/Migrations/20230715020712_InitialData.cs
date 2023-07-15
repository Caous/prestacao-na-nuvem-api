using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartOficina.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cliente",
                columns: new[] { "Id", "DataCadastro", "Email", "Nome", "Telefone" },
                values: new object[] { new Guid("2cdea58a-e3ab-4f86-bd9c-8689b671005d"), new DateTime(2023, 7, 15, 2, 7, 11, 882, DateTimeKind.Utc).AddTicks(9655), "testecliente@gmail.com", "Teste Cliente", null });

            migrationBuilder.InsertData(
                table: "Prestador",
                columns: new[] { "Id", "DataCadastro", "Nome" },
                values: new object[] { new Guid("34fe7575-16ce-4bcd-8574-7e5ed368beeb"), new DateTime(2023, 7, 15, 2, 7, 11, 882, DateTimeKind.Utc).AddTicks(9867), "Teste Prestador" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cliente",
                keyColumn: "Id",
                keyValue: new Guid("2cdea58a-e3ab-4f86-bd9c-8689b671005d"));

            migrationBuilder.DeleteData(
                table: "Prestador",
                keyColumn: "Id",
                keyValue: new Guid("34fe7575-16ce-4bcd-8574-7e5ed368beeb"));
        }
    }
}
