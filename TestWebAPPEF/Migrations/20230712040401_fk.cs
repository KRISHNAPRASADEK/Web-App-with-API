using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestWebAPPEF.Migrations
{
    public partial class fk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Producer_ProducerIDId",
                table: "Movie");

            migrationBuilder.RenameColumn(
                name: "ProducerIDId",
                table: "Movie",
                newName: "ProducerId");

            migrationBuilder.RenameIndex(
                name: "IX_Movie_ProducerIDId",
                table: "Movie",
                newName: "IX_Movie_ProducerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Producer_ProducerId",
                table: "Movie",
                column: "ProducerId",
                principalTable: "Producer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Producer_ProducerId",
                table: "Movie");

            migrationBuilder.RenameColumn(
                name: "ProducerId",
                table: "Movie",
                newName: "ProducerIDId");

            migrationBuilder.RenameIndex(
                name: "IX_Movie_ProducerId",
                table: "Movie",
                newName: "IX_Movie_ProducerIDId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Producer_ProducerIDId",
                table: "Movie",
                column: "ProducerIDId",
                principalTable: "Producer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
