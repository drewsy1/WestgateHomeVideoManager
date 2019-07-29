using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WHVM.Database.Migrations
{
    public partial class AddedFileMetadata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileCategories",
                columns: table => new
                {
                    FileCategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileCategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileCategories", x => x.FileCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "FileFormats",
                columns: table => new
                {
                    FileFormatId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileFormatName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileFormats", x => x.FileFormatId);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    FileId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(nullable: true),
                    SourceId = table.Column<int>(nullable: true),
                    ClipId = table.Column<int>(nullable: true),
                    FileFormatId = table.Column<int>(nullable: false),
                    FilePath = table.Column<string>(nullable: true),
                    FileNotes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_Files_Clips_ClipId",
                        column: x => x.ClipId,
                        principalTable: "Clips",
                        principalColumn: "ClipId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Files_FileFormats_FileFormatId",
                        column: x => x.FileFormatId,
                        principalTable: "FileFormats",
                        principalColumn: "FileFormatId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Files_Sources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Sources",
                        principalColumn: "SourceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FileFileCategories",
                columns: table => new
                {
                    FileId = table.Column<int>(nullable: false),
                    FileCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileFileCategories", x => new { x.FileId, x.FileCategoryId });
                    table.ForeignKey(
                        name: "FK_FileFileCategories_FileCategories_FileCategoryId",
                        column: x => x.FileCategoryId,
                        principalTable: "FileCategories",
                        principalColumn: "FileCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileFileCategories_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "FileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FileCategories",
                columns: new[] { "FileCategoryId", "FileCategoryName" },
                values: new object[,]
                {
                    { 1, "Original" },
                    { 2, "Split" },
                    { 3, "Converted" },
                    { 4, "Public" }
                });

            migrationBuilder.InsertData(
                table: "FileFormats",
                columns: new[] { "FileFormatId", "FileFormatName" },
                values: new object[,]
                {
                    { 1, "mp4" },
                    { 2, "avi" },
                    { 3, "mov" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileFileCategories_FileCategoryId",
                table: "FileFileCategories",
                column: "FileCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_ClipId",
                table: "Files",
                column: "ClipId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_FileFormatId",
                table: "Files",
                column: "FileFormatId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_SourceId",
                table: "Files",
                column: "SourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileFileCategories");

            migrationBuilder.DropTable(
                name: "FileCategories");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "FileFormats");
        }
    }
}
