using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Challenges.WebApp.Migrations
{
    public partial class nwww12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "Puncte",
                table: "Utilizator",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "Puncte",
                table: "Utilizator");
        }
    }
}
