using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Challenges.WebApp.Migrations
{
    public partial class nww2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategorieProvocare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategorieId = table.Column<int>(type: "int", nullable: false),
                    ProvocareId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorieProvocare", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategorieProvocare_Categorie_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "Categorie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategorieProvocare_Provocare_ProvocareId",
                        column: x => x.ProvocareId,
                        principalTable: "Provocare",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategorieUtilizator",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategorieId = table.Column<int>(type: "int", nullable: false),
                    UtilizatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorieUtilizator", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategorieUtilizator_Categorie_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "Categorie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategorieUtilizator_Utilizator_UtilizatorId",
                        column: x => x.UtilizatorId,
                        principalTable: "Utilizator",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategorieProvocare_CategorieId",
                table: "CategorieProvocare",
                column: "CategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_CategorieProvocare_ProvocareId",
                table: "CategorieProvocare",
                column: "ProvocareId");

            migrationBuilder.CreateIndex(
                name: "IX_CategorieUtilizator_CategorieId",
                table: "CategorieUtilizator",
                column: "CategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_CategorieUtilizator_UtilizatorId",
                table: "CategorieUtilizator",
                column: "UtilizatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategorieProvocare");

            migrationBuilder.DropTable(
                name: "CategorieUtilizator");

            migrationBuilder.DropTable(
                name: "Categorie");
        }
    }
}
