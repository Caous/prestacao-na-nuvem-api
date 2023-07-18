using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartOficina.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cliente",
                keyColumn: "Id",
                keyValue: new Guid("1333b7b8-ace7-4873-ad03-649cd8815499"));

            migrationBuilder.DeleteData(
                table: "Prestador",
                keyColumn: "Id",
                keyValue: new Guid("66e26469-b800-43ac-b55b-7cdb7ccc6c83"));

            migrationBuilder.InsertData(
                table: "CategoriaServico",
                columns: new[] { "Id", "DataCadastro", "Desc", "Titulo" },
                values: new object[,]
                {
                    { new Guid("21b1de84-b459-41d8-909c-e347af37257e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Serviço gerais na parte de motor do veículo", "Motor" },
                    { new Guid("9007d333-ff0c-41df-9518-f5f893652f03"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Serviços na parte de suspensão/geometria", "Suspensão" }
                });

            migrationBuilder.InsertData(
                table: "Cliente",
                columns: new[] { "Id", "DataCadastro", "Email", "Nome", "Telefone" },
                values: new object[] { new Guid("cf5adba5-9753-439f-a584-9defdd1381cc"), new DateTime(2023, 7, 18, 0, 33, 48, 944, DateTimeKind.Utc).AddTicks(8013), "testecliente@gmail.com", "Teste Cliente", null });

            migrationBuilder.InsertData(
                table: "Prestador",
                columns: new[] { "Id", "DataCadastro", "Nome" },
                values: new object[] { new Guid("b4a4cc90-6196-4d71-8a53-b9256546b417"), new DateTime(2023, 7, 18, 0, 33, 48, 944, DateTimeKind.Utc).AddTicks(8195), "Teste Prestador" });

            migrationBuilder.InsertData(
                table: "SubServico",
                columns: new[] { "Id", "CategoriaId", "DataCadastro", "Desc", "Titulo" },
                values: new object[,]
                {
                    { new Guid("61b38a3e-c2c8-49f0-9fb8-17c5b8c61abc"), new Guid("21b1de84-b459-41d8-909c-e347af37257e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Troca de todos os pistões", "Troca pistão" },
                    { new Guid("86a1333b-f744-450c-8600-b888dc151cc5"), new Guid("21b1de84-b459-41d8-909c-e347af37257e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bloco condenado/Sem retífica, troca por um novo", "Troca bloco" },
                    { new Guid("e61932bf-d005-4737-8f73-76c3ed972f8a"), new Guid("9007d333-ff0c-41df-9518-f5f893652f03"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Troca da peça", "Troca bandeja" },
                    { new Guid("fe519082-ce88-4478-b59f-89f907065e5f"), new Guid("9007d333-ff0c-41df-9518-f5f893652f03"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Troca da peça", "Troca Amortecedor" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cliente",
                keyColumn: "Id",
                keyValue: new Guid("cf5adba5-9753-439f-a584-9defdd1381cc"));

            migrationBuilder.DeleteData(
                table: "Prestador",
                keyColumn: "Id",
                keyValue: new Guid("b4a4cc90-6196-4d71-8a53-b9256546b417"));

            migrationBuilder.DeleteData(
                table: "SubServico",
                keyColumn: "Id",
                keyValue: new Guid("61b38a3e-c2c8-49f0-9fb8-17c5b8c61abc"));

            migrationBuilder.DeleteData(
                table: "SubServico",
                keyColumn: "Id",
                keyValue: new Guid("86a1333b-f744-450c-8600-b888dc151cc5"));

            migrationBuilder.DeleteData(
                table: "SubServico",
                keyColumn: "Id",
                keyValue: new Guid("e61932bf-d005-4737-8f73-76c3ed972f8a"));

            migrationBuilder.DeleteData(
                table: "SubServico",
                keyColumn: "Id",
                keyValue: new Guid("fe519082-ce88-4478-b59f-89f907065e5f"));

            migrationBuilder.DeleteData(
                table: "CategoriaServico",
                keyColumn: "Id",
                keyValue: new Guid("21b1de84-b459-41d8-909c-e347af37257e"));

            migrationBuilder.DeleteData(
                table: "CategoriaServico",
                keyColumn: "Id",
                keyValue: new Guid("9007d333-ff0c-41df-9518-f5f893652f03"));

            migrationBuilder.InsertData(
                table: "Cliente",
                columns: new[] { "Id", "DataCadastro", "Email", "Nome", "Telefone" },
                values: new object[] { new Guid("1333b7b8-ace7-4873-ad03-649cd8815499"), new DateTime(2023, 7, 17, 1, 13, 15, 826, DateTimeKind.Utc).AddTicks(4817), "testecliente@gmail.com", "Teste Cliente", null });

            migrationBuilder.InsertData(
                table: "Prestador",
                columns: new[] { "Id", "DataCadastro", "Nome" },
                values: new object[] { new Guid("66e26469-b800-43ac-b55b-7cdb7ccc6c83"), new DateTime(2023, 7, 17, 1, 13, 15, 826, DateTimeKind.Utc).AddTicks(4932), "Teste Prestador" });
        }
    }
}
