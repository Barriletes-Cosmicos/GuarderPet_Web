using Microsoft.EntityFrameworkCore.Migrations;

namespace GuarderPet.API.Migrations
{
    public partial class Pethistories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CareDescriptions_PetServiceHistories_PetServiceHistoryId",
                table: "CareDescriptions");

            migrationBuilder.DropIndex(
                name: "IX_CareDescriptions_PetServiceHistoryId",
                table: "CareDescriptions");

            migrationBuilder.DropColumn(
                name: "PetServiceHistoryId",
                table: "CareDescriptions");

            migrationBuilder.AddColumn<int>(
                name: "HistoryId",
                table: "CareDescriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "CareDescriptions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_CareDescriptions_HistoryId",
                table: "CareDescriptions",
                column: "HistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CareDescriptions_PetServiceHistories_HistoryId",
                table: "CareDescriptions",
                column: "HistoryId",
                principalTable: "PetServiceHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CareDescriptions_PetServiceHistories_HistoryId",
                table: "CareDescriptions");

            migrationBuilder.DropIndex(
                name: "IX_CareDescriptions_HistoryId",
                table: "CareDescriptions");

            migrationBuilder.DropColumn(
                name: "HistoryId",
                table: "CareDescriptions");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "CareDescriptions");

            migrationBuilder.AddColumn<int>(
                name: "PetServiceHistoryId",
                table: "CareDescriptions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CareDescriptions_PetServiceHistoryId",
                table: "CareDescriptions",
                column: "PetServiceHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CareDescriptions_PetServiceHistories_PetServiceHistoryId",
                table: "CareDescriptions",
                column: "PetServiceHistoryId",
                principalTable: "PetServiceHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
