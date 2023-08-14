using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestWebAPPEF.Migrations
{
    public partial class fk2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Director_DiredctorIdId",
                table: "Movie");

            migrationBuilder.DropIndex(
                name: "IX_Movie_DiredctorIdId",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "DiredctorIdId",
                table: "Movie");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiredctorIdId",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Movie_DiredctorIdId",
                table: "Movie",
                column: "DiredctorIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Director_DiredctorIdId",
                table: "Movie",
                column: "DiredctorIdId",
                principalTable: "Director",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
