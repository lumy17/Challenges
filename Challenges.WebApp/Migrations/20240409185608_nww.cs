using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Challenges.WebApp.Migrations
{
    public partial class nww : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EsteDeblocat",
                table: "RealizareUtilizator");

            migrationBuilder.AlterColumn<string>(
                name: "Stare",
                table: "ProvocareUtilizator",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Vizualizari",
                table: "Provocare",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "VizualizareProvocare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvocareId = table.Column<int>(type: "int", nullable: false),
                    UtilizatorId = table.Column<int>(type: "int", nullable: false),
                    DataVizualizare = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VizualizareProvocare", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VizualizareProvocare_Provocare_ProvocareId",
                        column: x => x.ProvocareId,
                        principalTable: "Provocare",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VizualizareProvocare_Utilizator_UtilizatorId",
                        column: x => x.UtilizatorId,
                        principalTable: "Utilizator",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VizualizareProvocare_ProvocareId",
                table: "VizualizareProvocare",
                column: "ProvocareId");

            migrationBuilder.CreateIndex(
                name: "IX_VizualizareProvocare_UtilizatorId",
                table: "VizualizareProvocare",
                column: "UtilizatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VizualizareProvocare");

            migrationBuilder.DropColumn(
                name: "Vizualizari",
                table: "Provocare");

            migrationBuilder.AddColumn<bool>(
                name: "EsteDeblocat",
                table: "RealizareUtilizator",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Stare",
                table: "ProvocareUtilizator",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
