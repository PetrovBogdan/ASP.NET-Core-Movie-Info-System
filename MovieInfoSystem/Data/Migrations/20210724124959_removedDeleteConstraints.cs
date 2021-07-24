using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieInfoSystem.Data.Migrations
{
    public partial class removedDeleteConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActorMovie_Actors_ActorId",
                table: "ActorMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_ActorMovie_Movies_MovieId",
                table: "ActorMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Countries_CountryId",
                table: "Actors");

            migrationBuilder.DropForeignKey(
                name: "FK_CountryMovie_Countries_CountryId",
                table: "CountryMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_CountryMovie_Movies_MovieId",
                table: "CountryMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectorMOvie_Directors_DirectorId",
                table: "DirectorMOvie");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectorMOvie_Movies_MovieId",
                table: "DirectorMOvie");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreMovie_Genres_GenreId",
                table: "GenreMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreMovie_Movies_MovieId",
                table: "GenreMovie");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ActorMovie_Actors_ActorId",
                table: "ActorMovie",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActorMovie_Movies_MovieId",
                table: "ActorMovie",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CountryMovie_Countries_CountryId",
                table: "CountryMovie",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CountryMovie_Movies_MovieId",
                table: "CountryMovie",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMovie_Genres_GenreId",
                table: "GenreMovie",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMovie_Movies_MovieId",
                table: "GenreMovie",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActorMovie_Actors_ActorId",
                table: "ActorMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_ActorMovie_Movies_MovieId",
                table: "ActorMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_CountryMovie_Countries_CountryId",
                table: "CountryMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_CountryMovie_Movies_MovieId",
                table: "CountryMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectorMOvie_Directors_DirectorId",
                table: "DirectorMOvie");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectorMOvie_Movies_MovieId",
                table: "DirectorMOvie");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreMovie_Genres_GenreId",
                table: "GenreMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreMovie_Movies_MovieId",
                table: "GenreMovie");

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
                name: "FK_ActorMovie_Actors_ActorId",
                table: "ActorMovie",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ActorMovie_Movies_MovieId",
                table: "ActorMovie",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Countries_CountryId",
                table: "Actors",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CountryMovie_Countries_CountryId",
                table: "CountryMovie",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CountryMovie_Movies_MovieId",
                table: "CountryMovie",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectorMOvie_Directors_DirectorId",
                table: "DirectorMOvie",
                column: "DirectorId",
                principalTable: "Directors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectorMOvie_Movies_MovieId",
                table: "DirectorMOvie",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMovie_Genres_GenreId",
                table: "GenreMovie",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMovie_Movies_MovieId",
                table: "GenreMovie",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
