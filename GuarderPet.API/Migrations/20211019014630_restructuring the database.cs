using Microsoft.EntityFrameworkCore.Migrations;

namespace GuarderPet.API.Migrations
{
    public partial class restructuringthedatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetServiceHistories_CareDescriptions_CareDescriptionId",
                table: "PetServiceHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_PetServices_CareDescriptions_CareDescriptionId",
                table: "PetServices");

            migrationBuilder.DropIndex(
                name: "IX_PetServices_CareDescriptionId",
                table: "PetServices");

            migrationBuilder.DropIndex(
                name: "IX_PetServiceHistories_CareDescriptionId",
                table: "PetServiceHistories");

            migrationBuilder.DropColumn(
                name: "CareDescriptionId",
                table: "PetServices");

            migrationBuilder.DropColumn(
                name: "CareDescriptionId",
                table: "PetServiceHistories");

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "CareDescriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PetServiceHistoryId",
                table: "CareDescriptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PetServicesId",
                table: "CareDescriptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ServicePrice",
                table: "CareDescriptions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_CareDescriptions_PetServiceHistoryId",
                table: "CareDescriptions",
                column: "PetServiceHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CareDescriptions_PetServicesId",
                table: "CareDescriptions",
                column: "PetServicesId");

            migrationBuilder.AddForeignKey(
                name: "FK_CareDescriptions_PetServiceHistories_PetServiceHistoryId",
                table: "CareDescriptions",
                column: "PetServiceHistoryId",
                principalTable: "PetServiceHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CareDescriptions_PetServices_PetServicesId",
                table: "CareDescriptions",
                column: "PetServicesId",
                principalTable: "PetServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CareDescriptions_PetServiceHistories_PetServiceHistoryId",
                table: "CareDescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_CareDescriptions_PetServices_PetServicesId",
                table: "CareDescriptions");

            migrationBuilder.DropIndex(
                name: "IX_CareDescriptions_PetServiceHistoryId",
                table: "CareDescriptions");

            migrationBuilder.DropIndex(
                name: "IX_CareDescriptions_PetServicesId",
                table: "CareDescriptions");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "CareDescriptions");

            migrationBuilder.DropColumn(
                name: "PetServiceHistoryId",
                table: "CareDescriptions");

            migrationBuilder.DropColumn(
                name: "PetServicesId",
                table: "CareDescriptions");

            migrationBuilder.DropColumn(
                name: "ServicePrice",
                table: "CareDescriptions");

            migrationBuilder.AddColumn<int>(
                name: "CareDescriptionId",
                table: "PetServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CareDescriptionId",
                table: "PetServiceHistories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PetServices_CareDescriptionId",
                table: "PetServices",
                column: "CareDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_PetServiceHistories_CareDescriptionId",
                table: "PetServiceHistories",
                column: "CareDescriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PetServiceHistories_CareDescriptions_CareDescriptionId",
                table: "PetServiceHistories",
                column: "CareDescriptionId",
                principalTable: "CareDescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PetServices_CareDescriptions_CareDescriptionId",
                table: "PetServices",
                column: "CareDescriptionId",
                principalTable: "CareDescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
