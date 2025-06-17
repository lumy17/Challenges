using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Challenges.WebApp.Migrations
{
    public partial class deketeAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoTask_Challenge_ChallengeId",
                table: "TodoTask");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Challenge");

            migrationBuilder.EnsureSchema(
                name: "challenges");

            migrationBuilder.RenameTable(
                name: "UserPreference",
                newName: "UserPreference",
                newSchema: "challenges");

            migrationBuilder.RenameTable(
                name: "UserChallenge",
                newName: "UserChallenge",
                newSchema: "challenges");

            migrationBuilder.RenameTable(
                name: "UserBadge",
                newName: "UserBadge",
                newSchema: "challenges");

            migrationBuilder.RenameTable(
                name: "TodoTask",
                newName: "TodoTask",
                newSchema: "challenges");

            migrationBuilder.RenameTable(
                name: "FinishedTask",
                newName: "FinishedTask",
                newSchema: "challenges");

            migrationBuilder.RenameTable(
                name: "ChallengeCategory",
                newName: "ChallengeCategory",
                newSchema: "challenges");

            migrationBuilder.RenameTable(
                name: "Challenge",
                newName: "Challenge",
                newSchema: "challenges");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Category",
                newSchema: "challenges");

            migrationBuilder.RenameTable(
                name: "Badge",
                newName: "Badge",
                newSchema: "challenges");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "AspNetUserTokens",
                newSchema: "challenges");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "AspNetUsers",
                newSchema: "challenges");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "AspNetUserRoles",
                newSchema: "challenges");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "AspNetUserLogins",
                newSchema: "challenges");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "AspNetUserClaims",
                newSchema: "challenges");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "AspNetRoles",
                newSchema: "challenges");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "AspNetRoleClaims",
                newSchema: "challenges");

            migrationBuilder.RenameTable(
                name: "AppUser",
                newName: "AppUser",
                newSchema: "challenges");

            migrationBuilder.AlterColumn<int>(
                name: "ChallengeId",
                schema: "challenges",
                table: "TodoTask",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoTask_Challenge_ChallengeId",
                schema: "challenges",
                table: "TodoTask",
                column: "ChallengeId",
                principalSchema: "challenges",
                principalTable: "Challenge",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoTask_Challenge_ChallengeId",
                schema: "challenges",
                table: "TodoTask");

            migrationBuilder.RenameTable(
                name: "UserPreference",
                schema: "challenges",
                newName: "UserPreference");

            migrationBuilder.RenameTable(
                name: "UserChallenge",
                schema: "challenges",
                newName: "UserChallenge");

            migrationBuilder.RenameTable(
                name: "UserBadge",
                schema: "challenges",
                newName: "UserBadge");

            migrationBuilder.RenameTable(
                name: "TodoTask",
                schema: "challenges",
                newName: "TodoTask");

            migrationBuilder.RenameTable(
                name: "FinishedTask",
                schema: "challenges",
                newName: "FinishedTask");

            migrationBuilder.RenameTable(
                name: "ChallengeCategory",
                schema: "challenges",
                newName: "ChallengeCategory");

            migrationBuilder.RenameTable(
                name: "Challenge",
                schema: "challenges",
                newName: "Challenge");

            migrationBuilder.RenameTable(
                name: "Category",
                schema: "challenges",
                newName: "Category");

            migrationBuilder.RenameTable(
                name: "Badge",
                schema: "challenges",
                newName: "Badge");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                schema: "challenges",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                schema: "challenges",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                schema: "challenges",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                schema: "challenges",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                schema: "challenges",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                schema: "challenges",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                schema: "challenges",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "AppUser",
                schema: "challenges",
                newName: "AppUser");

            migrationBuilder.AlterColumn<int>(
                name: "ChallengeId",
                table: "TodoTask",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Challenge",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoTask_Challenge_ChallengeId",
                table: "TodoTask",
                column: "ChallengeId",
                principalTable: "Challenge",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
