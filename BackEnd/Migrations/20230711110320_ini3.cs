using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestWebAPPEF.Migrations
{
    public partial class ini3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiredctorId",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "ProducerID",
                table: "Movie");

            migrationBuilder.AddColumn<int>(
                name: "DiredctorIdId",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProducerIDId",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Director",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Director", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movie_DiredctorIdId",
                table: "Movie",
                column: "DiredctorIdId");

            migrationBuilder.CreateIndex(
                name: "IX_Movie_ProducerIDId",
                table: "Movie",
                column: "ProducerIDId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Director_DiredctorIdId",
                table: "Movie",
                column: "DiredctorIdId",
                principalTable: "Director",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Producer_ProducerIDId",
                table: "Movie",
                column: "ProducerIDId",
                principalTable: "Producer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Director_DiredctorIdId",
                table: "Movie");

            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Producer_ProducerIDId",
                table: "Movie");

            migrationBuilder.DropTable(
                name: "Director");

            migrationBuilder.DropIndex(
                name: "IX_Movie_DiredctorIdId",
                table: "Movie");

            migrationBuilder.DropIndex(
                name: "IX_Movie_ProducerIDId",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "DiredctorIdId",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "ProducerIDId",
                table: "Movie");

            migrationBuilder.AddColumn<string>(
                name: "DiredctorId",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProducerID",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
