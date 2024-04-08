using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Challenges.Migrations
{
    public partial class new3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SarcinaRealizata_ProvocareUtilizator_provocareUtilizatorId",
                table: "SarcinaRealizata");

            migrationBuilder.DropColumn(
                name: "IdProvocareUtilizator",
                table: "SarcinaRealizata");

            migrationBuilder.DropColumn(
                name: "IdSarcini",
                table: "SarcinaRealizata");

            migrationBuilder.RenameColumn(
                name: "provocareUtilizatorId",
                table: "SarcinaRealizata",
                newName: "ProvocareUtilizatorId");

            migrationBuilder.RenameIndex(
                name: "IX_SarcinaRealizata_provocareUtilizatorId",
                table: "SarcinaRealizata",
                newName: "IX_SarcinaRealizata_ProvocareUtilizatorId");

            migrationBuilder.AddColumn<bool>(
                name: "EsteDeblocat",
                table: "RealizareUtilizator",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_SarcinaRealizata_ProvocareUtilizator_ProvocareUtilizatorId",
                table: "SarcinaRealizata",
                column: "ProvocareUtilizatorId",
                principalTable: "ProvocareUtilizator",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SarcinaRealizata_ProvocareUtilizator_ProvocareUtilizatorId",
                table: "SarcinaRealizata");

            migrationBuilder.DropColumn(
                name: "EsteDeblocat",
                table: "RealizareUtilizator");

            migrationBuilder.RenameColumn(
                name: "ProvocareUtilizatorId",
                table: "SarcinaRealizata",
                newName: "provocareUtilizatorId");

            migrationBuilder.RenameIndex(
                name: "IX_SarcinaRealizata_ProvocareUtilizatorId",
                table: "SarcinaRealizata",
                newName: "IX_SarcinaRealizata_provocareUtilizatorId");

            migrationBuilder.AddColumn<int>(
                name: "IdProvocareUtilizator",
                table: "SarcinaRealizata",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdSarcini",
                table: "SarcinaRealizata",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_SarcinaRealizata_ProvocareUtilizator_provocareUtilizatorId",
                table: "SarcinaRealizata",
                column: "provocareUtilizatorId",
                principalTable: "ProvocareUtilizator",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
