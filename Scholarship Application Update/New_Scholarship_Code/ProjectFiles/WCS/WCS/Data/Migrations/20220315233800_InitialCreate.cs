using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCS.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HighSchoolStem",
                table: "StudentProfiles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
