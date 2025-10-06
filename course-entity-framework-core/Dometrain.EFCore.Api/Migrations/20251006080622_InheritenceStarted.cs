using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dometrain.EFCore.Api.Migrations
{
    /// <inheritdoc />
    public partial class InheritenceStarted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movie_Actors");

            migrationBuilder.DropTable(
                name: "Movie_Directors");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ReleaseDate",
                table: "Pictures",
                type: "date",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(8)");

            migrationBuilder.AddColumn<string>(
                name: "ChannelFirstAiredOn",
                table: "Pictures",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Pictures",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "GrossRevenue",
                table: "Pictures",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Genres",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChannelFirstAiredOn",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "GrossRevenue",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Genres");

            migrationBuilder.AlterColumn<string>(
                name: "ReleaseDate",
                table: "Pictures",
                type: "char(8)",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.CreateTable(
                name: "Movie_Actors",
                columns: table => new
                {
                    MovieIdentifier = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie_Actors", x => new { x.MovieIdentifier, x.Id });
                    table.ForeignKey(
                        name: "FK_Movie_Actors_Pictures_MovieIdentifier",
                        column: x => x.MovieIdentifier,
                        principalTable: "Pictures",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movie_Directors",
                columns: table => new
                {
                    MovieIdentifier = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie_Directors", x => x.MovieIdentifier);
                    table.ForeignKey(
                        name: "FK_Movie_Directors_Pictures_MovieIdentifier",
                        column: x => x.MovieIdentifier,
                        principalTable: "Pictures",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
