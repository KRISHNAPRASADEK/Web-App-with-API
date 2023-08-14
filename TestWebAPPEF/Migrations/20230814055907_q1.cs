using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestWebAPPEF.Migrations
{
    public partial class q1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "language",
                table: "Movie",
                newName: "Language");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Language",
                table: "Movie",
                newName: "language");
        }
    }
}
