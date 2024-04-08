using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Challenges.Migrations
{
    public partial class new2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataUltimaActualizareStreak",
                table: "Utilizator",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataUltimaActualizareStreak",
                table: "Utilizator");
        }
    }
}
