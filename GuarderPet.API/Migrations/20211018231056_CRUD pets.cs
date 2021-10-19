using Microsoft.EntityFrameworkCore.Migrations;

namespace GuarderPet.API.Migrations
{
    public partial class CRUDpets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetPhoto_Pets_PetId",
                table: "PetPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PetPhoto",
                table: "PetPhoto");

            migrationBuilder.RenameTable(
                name: "PetPhoto",
                newName: "PetPhotos");

            migrationBuilder.RenameIndex(
                name: "IX_PetPhoto_PetId",
                table: "PetPhotos",
                newName: "IX_PetPhotos_PetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PetPhotos",
                table: "PetPhotos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PetPhotos_Pets_PetId",
                table: "PetPhotos",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetPhotos_Pets_PetId",
                table: "PetPhotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PetPhotos",
                table: "PetPhotos");

            migrationBuilder.RenameTable(
                name: "PetPhotos",
                newName: "PetPhoto");

            migrationBuilder.RenameIndex(
                name: "IX_PetPhotos_PetId",
                table: "PetPhoto",
                newName: "IX_PetPhoto_PetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PetPhoto",
                table: "PetPhoto",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PetPhoto_Pets_PetId",
                table: "PetPhoto",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
