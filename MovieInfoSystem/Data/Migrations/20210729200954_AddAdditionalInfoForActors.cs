using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieInfoSystem.Data.Migrations
{
    public partial class AddAdditionalInfoForActors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Biography",
                table: "Actors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Actors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Actors",
                type: "nvarchar(max)",
                nullable: true);

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
                name: "Biography",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Actors");
        }
    }
}
