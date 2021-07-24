using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieInfoSystem.Data.Migrations
{
    public partial class AddedCountryDescriptioImageAndDateOfBirthToActors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Actors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DateOfBirth",
                table: "Actors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Actors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Actors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_CountryId",
                table: "Actors",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Countries_CountryId",
                table: "Actors",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Countries_CountryId",
                table: "Actors");

            migrationBuilder.DropIndex(
                name: "IX_Actors_CountryId",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Actors");
        }
    }
}
