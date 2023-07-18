using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartOficina.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVeiculo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                table: "CategoriaServico",
                columns: new[] { "Id", "DataCadastro", "Desc", "Titulo" },
                values: new object[,]
                {
                    { new Guid("27a7b1a7-86d3-4e28-8737-9193d449e1b2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Serviços na parte de suspensão/geometria", "Suspensão" },
                    { new Guid("b03e8c8c-2abe-425b-bf0d-e517f4595cba"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Serviço gerais na parte de motor do veículo", "Motor" }
                });

            migrationBuilder.InsertData(
                table: "Cliente",
                columns: new[] { "Id", "DataCadastro", "Email", "Nome", "Telefone" },
                values: new object[] { new Guid("899f9291-308d-4a94-b19d-299ee534c0ab"), new DateTime(2023, 7, 18, 1, 0, 44, 451, DateTimeKind.Utc).AddTicks(7059), "testecliente@gmail.com", "Teste Cliente", null });

            migrationBuilder.InsertData(
                table: "Prestador",
                columns: new[] { "Id", "DataCadastro", "Nome" },
                values: new object[] { new Guid("957392ea-2a13-4684-a8af-b69e423d87a8"), new DateTime(2023, 7, 18, 1, 0, 44, 451, DateTimeKind.Utc).AddTicks(7215), "Teste Prestador" });

            migrationBuilder.InsertData(
                table: "Veiculo",
                columns: new[] { "Id", "DataCadastro", "Marca", "Modelo", "Placa", "Tipo" },
                values: new object[,]
                {
                    { new Guid("5c4289cf-2578-4bc8-baa1-e1be20943274"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hyundai", "I30", "BBB-1234", 0 },
                    { new Guid("977c9490-2f3d-429d-a102-09f8317843c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chevrolet", "Agile", "AAA-1234", 0 },
                    { new Guid("cec1b671-de00-482c-8a89-14aeefab8092"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chevrolet", "Celta", "CCC-1234", 0 }
                });

            migrationBuilder.InsertData(
                table: "SubServico",
                columns: new[] { "Id", "CategoriaId", "DataCadastro", "Desc", "Titulo" },
                values: new object[,]
                {
                    { new Guid("019e301b-03d5-46fd-9d78-a9c5ac19f213"), new Guid("b03e8c8c-2abe-425b-bf0d-e517f4595cba"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bloco condenado/Sem retífica, troca por um novo", "Troca bloco" },
                    { new Guid("033e7e22-73f8-4573-a35b-ffc222236295"), new Guid("b03e8c8c-2abe-425b-bf0d-e517f4595cba"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Troca de todos os pistões", "Troca pistão" },
                    { new Guid("7b39de98-16c9-4cf1-8be2-ca78b39ccc37"), new Guid("27a7b1a7-86d3-4e28-8737-9193d449e1b2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Troca da peça", "Troca Amortecedor" },
                    { new Guid("f8ffcf58-3ccf-43c0-a2b4-afaf399d809b"), new Guid("27a7b1a7-86d3-4e28-8737-9193d449e1b2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Troca da peça", "Troca bandeja" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cliente",
                keyColumn: "Id",
                keyValue: new Guid("899f9291-308d-4a94-b19d-299ee534c0ab"));

            migrationBuilder.DeleteData(
                table: "Prestador",
                keyColumn: "Id",
                keyValue: new Guid("957392ea-2a13-4684-a8af-b69e423d87a8"));

            migrationBuilder.DeleteData(
                table: "SubServico",
                keyColumn: "Id",
                keyValue: new Guid("019e301b-03d5-46fd-9d78-a9c5ac19f213"));

            migrationBuilder.DeleteData(
                table: "SubServico",
                keyColumn: "Id",
                keyValue: new Guid("033e7e22-73f8-4573-a35b-ffc222236295"));

            migrationBuilder.DeleteData(
                table: "SubServico",
                keyColumn: "Id",
                keyValue: new Guid("7b39de98-16c9-4cf1-8be2-ca78b39ccc37"));

            migrationBuilder.DeleteData(
                table: "SubServico",
                keyColumn: "Id",
                keyValue: new Guid("f8ffcf58-3ccf-43c0-a2b4-afaf399d809b"));

            migrationBuilder.DeleteData(
                table: "Veiculo",
                keyColumn: "Id",
                keyValue: new Guid("5c4289cf-2578-4bc8-baa1-e1be20943274"));

            migrationBuilder.DeleteData(
                table: "Veiculo",
                keyColumn: "Id",
                keyValue: new Guid("977c9490-2f3d-429d-a102-09f8317843c2"));

            migrationBuilder.DeleteData(
                table: "Veiculo",
                keyColumn: "Id",
                keyValue: new Guid("cec1b671-de00-482c-8a89-14aeefab8092"));

            migrationBuilder.DeleteData(
                table: "CategoriaServico",
                keyColumn: "Id",
                keyValue: new Guid("27a7b1a7-86d3-4e28-8737-9193d449e1b2"));

            migrationBuilder.DeleteData(
                table: "CategoriaServico",
                keyColumn: "Id",
                keyValue: new Guid("b03e8c8c-2abe-425b-bf0d-e517f4595cba"));

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
    }
}
