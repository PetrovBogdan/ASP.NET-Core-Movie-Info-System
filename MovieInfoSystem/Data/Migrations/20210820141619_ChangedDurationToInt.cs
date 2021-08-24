namespace MovieInfoSystem.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangedDurationToInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectorMOvie_Directors_DirectorId",
                table: "DirectorMOvie");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectorMOvie_Movies_MovieId",
                table: "DirectorMOvie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DirectorMOvie",
                table: "DirectorMOvie");

            migrationBuilder.RenameTable(
                name: "DirectorMOvie",
                newName: "DirectorMovie");

            migrationBuilder.RenameIndex(
                name: "IX_DirectorMOvie_MovieId",
                table: "DirectorMovie",
                newName: "IX_DirectorMovie_MovieId");

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "Movies",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DirectorMovie",
                table: "DirectorMovie",
                columns: new[] { "DirectorId", "MovieId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DirectorMovie_Directors_DirectorId",
                table: "DirectorMovie",
                column: "DirectorId",
                principalTable: "Directors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectorMovie_Movies_MovieId",
                table: "DirectorMovie",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectorMovie_Directors_DirectorId",
                table: "DirectorMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectorMovie_Movies_MovieId",
                table: "DirectorMovie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DirectorMovie",
                table: "DirectorMovie");

            migrationBuilder.RenameTable(
                name: "DirectorMovie",
                newName: "DirectorMOvie");

            migrationBuilder.RenameIndex(
                name: "IX_DirectorMovie_MovieId",
                table: "DirectorMOvie",
                newName: "IX_DirectorMOvie_MovieId");

            migrationBuilder.AlterColumn<string>(
                name: "Duration",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DirectorMOvie",
                table: "DirectorMOvie",
                columns: new[] { "DirectorId", "MovieId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DirectorMOvie_Directors_DirectorId",
                table: "DirectorMOvie",
                column: "DirectorId",
                principalTable: "Directors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectorMOvie_Movies_MovieId",
                table: "DirectorMOvie",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
