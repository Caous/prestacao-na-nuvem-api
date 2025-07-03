using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrestacaoNuvem.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAgendaEventoModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgendaEventos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataHoraInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataHoraFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Localizacao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendaEventos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgendaEventoFuncionarios",
                columns: table => new
                {
                    AgendaEventoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FuncionarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CorIndicativa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendaEventoFuncionarios", x => new { x.AgendaEventoId, x.FuncionarioId });
                    table.ForeignKey(
                        name: "FK_AgendaEventoFuncionarios_AgendaEventos_AgendaEventoId",
                        column: x => x.AgendaEventoId,
                        principalTable: "AgendaEventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgendaEventoFuncionarios");

            migrationBuilder.DropTable(
                name: "AgendaEventos");
        }
    }
}
