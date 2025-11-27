using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Filminurk.Data.Migrations
{
    /// <inheritdoc />
    public partial class FavoriteLists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FavoriteListID",
                table: "Movies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FavoriteListID",
                table: "Actors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FavoriteLists",
                columns: table => new
                {
                    FavoriteListID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListBelongsToUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMovieOrActor = table.Column<bool>(type: "bit", nullable: false),
                    ListName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPrivate = table.Column<bool>(type: "bit", nullable: false),
                    ListCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ListModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ListDeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsReported = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteLists", x => x.FavoriteListID);
                });

            migrationBuilder.CreateTable(
                name: "FilesToDatabase",
                columns: table => new
                {
                    ImageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesToDatabase", x => x.ImageID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_FavoriteListID",
                table: "Movies",
                column: "FavoriteListID");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_FavoriteListID",
                table: "Actors",
                column: "FavoriteListID");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_FavoriteLists_FavoriteListID",
                table: "Actors",
                column: "FavoriteListID",
                principalTable: "FavoriteLists",
                principalColumn: "FavoriteListID");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_FavoriteLists_FavoriteListID",
                table: "Movies",
                column: "FavoriteListID",
                principalTable: "FavoriteLists",
                principalColumn: "FavoriteListID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_FavoriteLists_FavoriteListID",
                table: "Actors");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_FavoriteLists_FavoriteListID",
                table: "Movies");

            migrationBuilder.DropTable(
                name: "FavoriteLists");

            migrationBuilder.DropTable(
                name: "FilesToDatabase");

            migrationBuilder.DropIndex(
                name: "IX_Movies_FavoriteListID",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Actors_FavoriteListID",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "FavoriteListID",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "FavoriteListID",
                table: "Actors");
        }
    }
}
