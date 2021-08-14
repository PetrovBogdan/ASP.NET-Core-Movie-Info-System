﻿namespace MovieInfoSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedCreatorPropertyForMovies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Creator",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Creator",
                table: "Movies");
        }
    }
}
