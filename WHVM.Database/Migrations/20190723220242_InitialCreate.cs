using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WHVM.Database.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    CollectionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CollectionName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.CollectionId);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "SourceFormats",
                columns: table => new
                {
                    SourceFormatId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SourceFormatText = table.Column<string>(nullable: true),
                    SourceFormatLogoPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceFormats", x => x.SourceFormatId);
                });

            migrationBuilder.CreateTable(
                name: "Sources",
                columns: table => new
                {
                    SourceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SourceLabel = table.Column<string>(nullable: true),
                    SourceDateCreated = table.Column<DateTime>(nullable: true),
                    SourceDateImported = table.Column<DateTime>(nullable: true),
                    SourceFormatId = table.Column<int>(nullable: false),
                    SourceFilePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sources", x => x.SourceId);
                    table.ForeignKey(
                        name: "FK_Sources_SourceFormats_SourceFormatId",
                        column: x => x.SourceFormatId,
                        principalTable: "SourceFormats",
                        principalColumn: "SourceFormatId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Clips",
                columns: table => new
                {
                    ClipId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SourceSegment = table.Column<int>(nullable: true),
                    SourceId = table.Column<int>(nullable: true),
                    ClipNumber = table.Column<int>(nullable: true),
                    ClipTimeStart = table.Column<DateTime>(nullable: true),
                    ClipTimeEnd = table.Column<DateTime>(nullable: true),
                    ClipVidTimeStart = table.Column<TimeSpan>(nullable: true),
                    ClipVidTimeEnd = table.Column<TimeSpan>(nullable: true),
                    ClipReviewerId = table.Column<int>(nullable: true),
                    ClipCameraOperatorId = table.Column<int>(nullable: true),
                    ClipDescription = table.Column<string>(nullable: true),
                    ClipFilePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clips", x => x.ClipId);
                    table.ForeignKey(
                        name: "FK_Clips_Persons_ClipCameraOperatorId",
                        column: x => x.ClipCameraOperatorId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clips_Persons_ClipReviewerId",
                        column: x => x.ClipReviewerId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clips_Sources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Sources",
                        principalColumn: "SourceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClipCollections",
                columns: table => new
                {
                    ClipId = table.Column<int>(nullable: false),
                    CollectionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClipCollections", x => new { x.ClipId, x.CollectionId });
                    table.ForeignKey(
                        name: "FK_ClipCollections_Clips_ClipId",
                        column: x => x.ClipId,
                        principalTable: "Clips",
                        principalColumn: "ClipId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClipCollections_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "CollectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClipPersons",
                columns: table => new
                {
                    ClipId = table.Column<int>(nullable: false),
                    PersonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClipPersons", x => new { x.ClipId, x.PersonId });
                    table.ForeignKey(
                        name: "FK_ClipPersons_Clips_ClipId",
                        column: x => x.ClipId,
                        principalTable: "Clips",
                        principalColumn: "ClipId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClipPersons_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClipCollections_CollectionId",
                table: "ClipCollections",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ClipPersons_PersonId",
                table: "ClipPersons",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Clips_ClipCameraOperatorId",
                table: "Clips",
                column: "ClipCameraOperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Clips_ClipReviewerId",
                table: "Clips",
                column: "ClipReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_Clips_SourceId",
                table: "Clips",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Sources_SourceFormatId",
                table: "Sources",
                column: "SourceFormatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClipCollections");

            migrationBuilder.DropTable(
                name: "ClipPersons");

            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.DropTable(
                name: "Clips");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Sources");

            migrationBuilder.DropTable(
                name: "SourceFormats");
        }
    }
}
