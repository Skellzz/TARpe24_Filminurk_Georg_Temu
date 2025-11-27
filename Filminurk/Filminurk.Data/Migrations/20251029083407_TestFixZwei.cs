using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Filminurk.Data.Migrations
{
    /// <inheritdoc />
    public partial class TestFixZwei : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FileToApi",
                table: "FileToApi");

            migrationBuilder.RenameTable(
                name: "FileToApi",
                newName: "FilesToApi");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FilesToApi",
                table: "FilesToApi",
                column: "ImageID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FilesToApi",
                table: "FilesToApi");

            migrationBuilder.RenameTable(
                name: "FilesToApi",
                newName: "FileToApi");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileToApi",
                table: "FileToApi",
                column: "ImageID");
        }
    }
}
