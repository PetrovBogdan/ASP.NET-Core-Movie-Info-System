namespace MovieInfoSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedAdditionalPropertiesToDirectors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Biography",
                table: "Directors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Directors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Directors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Directors_CountryId",
                table: "Directors",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Directors_Countries_CountryId",
                table: "Directors",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Directors_Countries_CountryId",
                table: "Directors");

            migrationBuilder.DropIndex(
                name: "IX_Directors_CountryId",
                table: "Directors");

            migrationBuilder.DropColumn(
                name: "Biography",
                table: "Directors");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Directors");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Directors");
        }
    }
}
