using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GuarderPet.API.Migrations
{
    public partial class historiesstarted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "PetServiceHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegisterDate",
                table: "PetServiceHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "PetServiceHistories");

            migrationBuilder.DropColumn(
                name: "RegisterDate",
                table: "PetServiceHistories");
        }
    }
}
