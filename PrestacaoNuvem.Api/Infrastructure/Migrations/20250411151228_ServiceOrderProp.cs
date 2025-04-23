using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrestacaoNuvem.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ServiceOrderProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataPagamento",
                table: "PrestacaoServico",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DescontoPercentual",
                table: "PrestacaoServico",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FormaPagamento",
                table: "PrestacaoServico",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "PrecoDescontado",
                table: "PrestacaoServico",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "PrecoOrdem",
                table: "PrestacaoServico",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataPagamento",
                table: "PrestacaoServico");

            migrationBuilder.DropColumn(
                name: "DescontoPercentual",
                table: "PrestacaoServico");

            migrationBuilder.DropColumn(
                name: "FormaPagamento",
                table: "PrestacaoServico");

            migrationBuilder.DropColumn(
                name: "PrecoDescontado",
                table: "PrestacaoServico");

            migrationBuilder.DropColumn(
                name: "PrecoOrdem",
                table: "PrestacaoServico");
        }
    }
}
