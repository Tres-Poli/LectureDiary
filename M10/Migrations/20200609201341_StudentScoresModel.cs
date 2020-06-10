using Microsoft.EntityFrameworkCore.Migrations;

namespace M10.Migrations
{
    public partial class StudentScoresModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentScores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(nullable: false),
                    Lec1Scores = table.Column<int>(nullable: false),
                    Lec2Scores = table.Column<int>(nullable: false),
                    Lec3Scores = table.Column<int>(nullable: false),
                    Lec4Scores = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentScores", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentScores");
        }
    }
}
