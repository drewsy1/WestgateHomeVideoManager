using Microsoft.EntityFrameworkCore.Migrations;

namespace WHVM.Database.Migrations
{
    public partial class RemovedClipFilePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClipFilePath",
                table: "Clips");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
