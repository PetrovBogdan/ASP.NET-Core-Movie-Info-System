using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieInfoSystem.Migrations
{
    public partial class AddedAdditionalPropertiesInCountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FlagUrl",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlagUrl",
                table: "Countries");
        }
    }
}
