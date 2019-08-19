using System;
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
                        .Annotation("Sqlite:Autoincrement", true),
                    CollectionName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.CollectionId);
                });

            migrationBuilder.CreateTable(
                name: "FileCategories",
                columns: table => new
                {
                    FileCategoryId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
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
                        .Annotation("Sqlite:Autoincrement", true),
                    FileFormatName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileFormats", x => x.FileFormatId);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
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
                        .Annotation("Sqlite:Autoincrement", true),
                    SourceFormatName = table.Column<string>(nullable: true),
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
                        .Annotation("Sqlite:Autoincrement", true),
                    SourceName = table.Column<string>(nullable: true),
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
                        .Annotation("Sqlite:Autoincrement", true),
                    SourceSegment = table.Column<int>(nullable: true),
                    SourceId = table.Column<int>(nullable: true),
                    ClipNumber = table.Column<int>(nullable: true),
                    ClipTimeStart = table.Column<DateTime>(nullable: true),
                    ClipTimeEnd = table.Column<DateTime>(nullable: true),
                    ClipVidTimeStart = table.Column<TimeSpan>(nullable: true),
                    ClipVidTimeEnd = table.Column<TimeSpan>(nullable: true),
                    ClipReviewerId = table.Column<int>(nullable: true),
                    ClipCameraOperatorId = table.Column<int>(nullable: true),
                    ClipName = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    FileId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
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
                table: "Collections",
                columns: new[] { "CollectionId", "CollectionName" },
                values: new object[] { 1, "Baby Moments" });

            migrationBuilder.InsertData(
                table: "Collections",
                columns: new[] { "CollectionId", "CollectionName" },
                values: new object[] { 23, "Westgate Cousins" });

            migrationBuilder.InsertData(
                table: "Collections",
                columns: new[] { "CollectionId", "CollectionName" },
                values: new object[] { 22, "Trips/Vacations" });

            migrationBuilder.InsertData(
                table: "Collections",
                columns: new[] { "CollectionId", "CollectionName" },
                values: new object[] { 21, "Thatchers" });

            migrationBuilder.InsertData(
                table: "Collections",
                columns: new[] { "CollectionId", "CollectionName" },
                values: new object[] { 20, "Swinging" });

            migrationBuilder.InsertData(
                table: "Collections",
                columns: new[] { "CollectionId", "CollectionName" },
                values: new object[] { 19, "Swimming" });

            migrationBuilder.InsertData(
                table: "Collections",
                columns: new[] { "CollectionId", "CollectionName" },
                values: new object[] { 18, "School" });

            migrationBuilder.InsertData(
                table: "Collections",
                columns: new[] { "CollectionId", "CollectionName" },
                values: new object[] { 17, "Pre-School" });

            migrationBuilder.InsertData(
                table: "Collections",
                columns: new[] { "CollectionId", "CollectionName" },
                values: new object[] { 16, "Playtime" });

            migrationBuilder.InsertData(
                table: "Collections",
                columns: new[] { "CollectionId", "CollectionName" },
                values: new object[] { 15, "Play/Performance" });

            migrationBuilder.InsertData(
                table: "Collections",
                columns: new[] { "CollectionId", "CollectionName" },
                values: new object[] { 14, "Outside Playtime" });

            migrationBuilder.InsertData(
                table: "Collections",
                columns: new[] { "CollectionId", "CollectionName" },
                values: new object[] { 13, "Misc. Holidays" });

            migrationBuilder.InsertData(
                table: "Collections",
                columns: new[] { "CollectionId", "CollectionName" },
                values: new object[] { 24, "Westgate Family Gatherings" });

            migrationBuilder.InsertData(
                table: "Collections",
                columns: new[] { "CollectionId", "CollectionName" },
                values: new object[] { 11, "Halloween" });

            migrationBuilder.InsertData(
                table: "Collections",
                columns: new[] { "CollectionId", "CollectionName" },
                values: new object[] { 10, "Grandma's House" });

            migrationBuilder.InsertData(
                table: "Collections",
                columns: new[] { "CollectionId", "CollectionName" },
                values: new object[] { 9, "Frost-McKay Family Gatherings" });

            migrationBuilder.InsertData(
                table: "Collections",
                columns: new[] { "CollectionId", "CollectionName" },
                values: new object[] { 8, "Frost-McKay Cousins" });

            migrationBuilder.InsertData(
                table: "Collections",
                columns: new[] { "CollectionId", "CollectionName" },
                values: new object[] { 7, "Easter" });

            migrationBuilder.InsertData(
                table: "Collections",
                columns: new[] { "CollectionId", "CollectionName" },
                values: new object[] { 6, "Driving" });

            migrationBuilder.InsertData(
                table: "Collections",
                columns: new[] { "CollectionId", "CollectionName" },
                values: new object[] { 5, "Church" });

            migrationBuilder.InsertData(
                table: "Collections",
                columns: new[] { "CollectionId", "CollectionName" },
                values: new object[] { 4, "Christmas" });

            migrationBuilder.InsertData(
                table: "Collections",
                columns: new[] { "CollectionId", "CollectionName" },
                values: new object[] { 3, "Blackmail" });

            migrationBuilder.InsertData(
                table: "Collections",
                columns: new[] { "CollectionId", "CollectionName" },
                values: new object[] { 2, "Birthday" });

            migrationBuilder.InsertData(
                table: "Collections",
                columns: new[] { "CollectionId", "CollectionName" },
                values: new object[] { 12, "Hot Springs" });

            migrationBuilder.InsertData(
                table: "FileCategories",
                columns: new[] { "FileCategoryId", "FileCategoryName" },
                values: new object[] { 1, "Original" });

            migrationBuilder.InsertData(
                table: "FileCategories",
                columns: new[] { "FileCategoryId", "FileCategoryName" },
                values: new object[] { 2, "Split" });

            migrationBuilder.InsertData(
                table: "FileCategories",
                columns: new[] { "FileCategoryId", "FileCategoryName" },
                values: new object[] { 3, "Converted" });

            migrationBuilder.InsertData(
                table: "FileCategories",
                columns: new[] { "FileCategoryId", "FileCategoryName" },
                values: new object[] { 4, "Public" });

            migrationBuilder.InsertData(
                table: "FileFormats",
                columns: new[] { "FileFormatId", "FileFormatName" },
                values: new object[] { 3, "mov" });

            migrationBuilder.InsertData(
                table: "FileFormats",
                columns: new[] { "FileFormatId", "FileFormatName" },
                values: new object[] { 2, "avi" });

            migrationBuilder.InsertData(
                table: "FileFormats",
                columns: new[] { "FileFormatId", "FileFormatName" },
                values: new object[] { 1, "mp4" });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonId", "PersonName" },
                values: new object[] { 8, "Irene McKay" });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonId", "PersonName" },
                values: new object[] { 1, "Bernard Westgate" });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonId", "PersonName" },
                values: new object[] { 2, "Beth Westgate" });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonId", "PersonName" },
                values: new object[] { 3, "Dennis Frost Sr." });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonId", "PersonName" },
                values: new object[] { 4, "Dennis Westgate" });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonId", "PersonName" },
                values: new object[] { 5, "Drew Westgate" });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonId", "PersonName" },
                values: new object[] { 6, "Edward Frost" });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonId", "PersonName" },
                values: new object[] { 7, "Hilda Frost" });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonId", "PersonName" },
                values: new object[] { 9, "Mary Westgate" });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonId", "PersonName" },
                values: new object[] { 10, "Merideth Westgate" });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonId", "PersonName" },
                values: new object[] { 11, "Zachary Westgate" });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonId", "PersonName" },
                values: new object[] { 12, "Gary Frost" });

            migrationBuilder.InsertData(
                table: "SourceFormats",
                columns: new[] { "SourceFormatId", "SourceFormatLogoPath", "SourceFormatName" },
                values: new object[] { 2, "images/Digital8_logo.svg", "Digital8" });

            migrationBuilder.InsertData(
                table: "SourceFormats",
                columns: new[] { "SourceFormatId", "SourceFormatLogoPath", "SourceFormatName" },
                values: new object[] { 3, "images/Hi8_logo.svg", "Hi8" });

            migrationBuilder.InsertData(
                table: "SourceFormats",
                columns: new[] { "SourceFormatId", "SourceFormatLogoPath", "SourceFormatName" },
                values: new object[] { 1, "images/DVD_logo.svg", "DVD" });

            migrationBuilder.InsertData(
                table: "SourceFormats",
                columns: new[] { "SourceFormatId", "SourceFormatLogoPath", "SourceFormatName" },
                values: new object[] { 4, "images/VHS_logo.svg", "VHS" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "SourceId", "SourceDateCreated", "SourceDateImported", "SourceFilePath", "SourceFormatId", "SourceName" },
                values: new object[] { 1, new DateTime(2009, 6, 21, 7, 25, 0, 0, DateTimeKind.Unspecified), new DateTime(2018, 7, 25, 19, 28, 52, 0, DateTimeKind.Unspecified), null, 1, "1994-02_1996-12" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "SourceId", "SourceDateCreated", "SourceDateImported", "SourceFilePath", "SourceFormatId", "SourceName" },
                values: new object[] { 19, new DateTime(2009, 6, 22, 6, 45, 52, 0, DateTimeKind.Unspecified), new DateTime(2018, 7, 30, 0, 36, 45, 0, DateTimeKind.Unspecified), null, 1, "2005-05_2005-11" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "SourceId", "SourceDateCreated", "SourceDateImported", "SourceFilePath", "SourceFormatId", "SourceName" },
                values: new object[] { 18, new DateTime(2009, 6, 22, 6, 27, 41, 0, DateTimeKind.Unspecified), new DateTime(2018, 7, 30, 0, 36, 26, 0, DateTimeKind.Unspecified), null, 1, "2005-01_2005_05" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "SourceId", "SourceDateCreated", "SourceDateImported", "SourceFilePath", "SourceFormatId", "SourceName" },
                values: new object[] { 17, new DateTime(2009, 6, 24, 8, 46, 14, 0, DateTimeKind.Unspecified), new DateTime(2018, 7, 29, 21, 27, 50, 0, DateTimeKind.Unspecified), null, 1, "2003-12_2004-03" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "SourceId", "SourceDateCreated", "SourceDateImported", "SourceFilePath", "SourceFormatId", "SourceName" },
                values: new object[] { 16, new DateTime(2009, 6, 22, 5, 52, 20, 0, DateTimeKind.Unspecified), new DateTime(2018, 8, 1, 18, 10, 5, 0, DateTimeKind.Unspecified), null, 1, "2003-10_2003-11" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "SourceId", "SourceDateCreated", "SourceDateImported", "SourceFilePath", "SourceFormatId", "SourceName" },
                values: new object[] { 15, new DateTime(2009, 6, 22, 3, 16, 10, 0, DateTimeKind.Unspecified), new DateTime(2018, 7, 29, 21, 22, 21, 0, DateTimeKind.Unspecified), null, 1, "2003-07_2003-10" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "SourceId", "SourceDateCreated", "SourceDateImported", "SourceFilePath", "SourceFormatId", "SourceName" },
                values: new object[] { 14, new DateTime(2009, 6, 24, 8, 48, 39, 0, DateTimeKind.Unspecified), new DateTime(2018, 7, 25, 22, 39, 46, 0, DateTimeKind.Unspecified), null, 1, "2003-04_2003-06" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "SourceId", "SourceDateCreated", "SourceDateImported", "SourceFilePath", "SourceFormatId", "SourceName" },
                values: new object[] { 13, new DateTime(2009, 6, 22, 3, 10, 13, 0, DateTimeKind.Unspecified), new DateTime(2018, 7, 25, 20, 13, 59, 0, DateTimeKind.Unspecified), null, 1, "2002-11_2002-12" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "SourceId", "SourceDateCreated", "SourceDateImported", "SourceFilePath", "SourceFormatId", "SourceName" },
                values: new object[] { 12, new DateTime(2009, 6, 22, 3, 8, 8, 0, DateTimeKind.Unspecified), new DateTime(2018, 7, 16, 10, 7, 34, 0, DateTimeKind.Unspecified), null, 1, "2002-09_2002-11" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "SourceId", "SourceDateCreated", "SourceDateImported", "SourceFilePath", "SourceFormatId", "SourceName" },
                values: new object[] { 20, new DateTime(2009, 6, 22, 6, 20, 39, 0, DateTimeKind.Unspecified), new DateTime(2018, 7, 30, 20, 3, 8, 0, DateTimeKind.Unspecified), null, 1, "2005-11_2005-12" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "SourceId", "SourceDateCreated", "SourceDateImported", "SourceFilePath", "SourceFormatId", "SourceName" },
                values: new object[] { 11, new DateTime(2009, 6, 22, 2, 51, 46, 0, DateTimeKind.Unspecified), new DateTime(2018, 8, 1, 9, 41, 45, 0, DateTimeKind.Unspecified), null, 1, "2001-12_2002-02" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "SourceId", "SourceDateCreated", "SourceDateImported", "SourceFilePath", "SourceFormatId", "SourceName" },
                values: new object[] { 9, new DateTime(2009, 6, 22, 2, 48, 17, 0, DateTimeKind.Unspecified), new DateTime(2018, 7, 16, 22, 25, 18, 0, DateTimeKind.Unspecified), null, 1, "2001-02_2001-05" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "SourceId", "SourceDateCreated", "SourceDateImported", "SourceFilePath", "SourceFormatId", "SourceName" },
                values: new object[] { 8, new DateTime(2009, 6, 21, 8, 6, 6, 0, DateTimeKind.Unspecified), new DateTime(2018, 7, 16, 10, 0, 11, 0, DateTimeKind.Unspecified), null, 1, "2000-11_2001-01" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "SourceId", "SourceDateCreated", "SourceDateImported", "SourceFilePath", "SourceFormatId", "SourceName" },
                values: new object[] { 7, new DateTime(2009, 6, 21, 7, 59, 32, 0, DateTimeKind.Unspecified), new DateTime(2018, 7, 16, 9, 45, 52, 0, DateTimeKind.Unspecified), null, 1, "1999-11_2000-04" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "SourceId", "SourceDateCreated", "SourceDateImported", "SourceFilePath", "SourceFormatId", "SourceName" },
                values: new object[] { 6, new DateTime(2009, 6, 21, 7, 51, 8, 0, DateTimeKind.Unspecified), new DateTime(2018, 7, 16, 9, 44, 23, 0, DateTimeKind.Unspecified), null, 1, "1999-02_1999-06" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "SourceId", "SourceDateCreated", "SourceDateImported", "SourceFilePath", "SourceFormatId", "SourceName" },
                values: new object[] { 5, new DateTime(2009, 6, 21, 7, 48, 5, 0, DateTimeKind.Unspecified), new DateTime(2018, 7, 16, 8, 58, 6, 0, DateTimeKind.Unspecified), null, 1, "1998-11_1999-02" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "SourceId", "SourceDateCreated", "SourceDateImported", "SourceFilePath", "SourceFormatId", "SourceName" },
                values: new object[] { 4, new DateTime(2009, 6, 21, 7, 44, 41, 0, DateTimeKind.Unspecified), new DateTime(2018, 7, 16, 1, 18, 24, 0, DateTimeKind.Unspecified), null, 1, "1997-10_1998-03" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "SourceId", "SourceDateCreated", "SourceDateImported", "SourceFilePath", "SourceFormatId", "SourceName" },
                values: new object[] { 3, new DateTime(2009, 6, 21, 7, 38, 13, 0, DateTimeKind.Unspecified), new DateTime(2018, 7, 16, 9, 34, 49, 0, DateTimeKind.Unspecified), null, 1, "1997-05_1997-08" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "SourceId", "SourceDateCreated", "SourceDateImported", "SourceFilePath", "SourceFormatId", "SourceName" },
                values: new object[] { 2, new DateTime(2009, 6, 21, 7, 34, 49, 0, DateTimeKind.Unspecified), new DateTime(2018, 7, 16, 1, 7, 19, 0, DateTimeKind.Unspecified), null, 1, "1996-12_1997-05" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "SourceId", "SourceDateCreated", "SourceDateImported", "SourceFilePath", "SourceFormatId", "SourceName" },
                values: new object[] { 10, new DateTime(2009, 6, 22, 2, 42, 47, 0, DateTimeKind.Unspecified), new DateTime(2018, 8, 1, 9, 41, 45, 0, DateTimeKind.Unspecified), null, 1, "2001-05_2001-08" });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "SourceId", "SourceDateCreated", "SourceDateImported", "SourceFilePath", "SourceFormatId", "SourceName" },
                values: new object[] { 22, new DateTime(2006, 5, 23, 8, 58, 39, 0, DateTimeKind.Unspecified), new DateTime(2019, 3, 23, 12, 11, 0, 0, DateTimeKind.Unspecified), null, 2, "20060523_20060526_Digital8" });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 1, 2, "Christmas Eve, 1995 - Christmas Eve at the Frosts' House", 1, 5, new DateTime(1995, 12, 24, 23, 32, 0, 0, DateTimeKind.Unspecified), new DateTime(1995, 12, 24, 23, 14, 24, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 4, 3, 710), new TimeSpan(0, 0, 0, 0, 968), 1, 1 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 104, 2, "1997-05-17 - Drew Plays Outside In the Pool While Dennis Works On the Playground", 42, 5, new DateTime(1997, 5, 17, 16, 14, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 5, 17, 16, 11, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 57, 32, 348), new TimeSpan(0, 0, 54, 29, 332), 2, 8 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 103, 2, "1997-05-17 - Drew Plays With Elizabeth", 41, 5, new DateTime(1997, 5, 17, 8, 3, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 5, 17, 7, 3, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 54, 29, 299), new TimeSpan(0, 0, 54, 7, 177), 2, 8 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 102, 2, "1997-05-16 - Beth Wakes Up Drew & Zach", 40, 5, new DateTime(1997, 5, 16, 8, 18, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 5, 16, 8, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 54, 7, 143), new TimeSpan(0, 0, 51, 17, 541), 2, 8 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 101, 2, "1997-05-11 - Drew Explores the Playground With Beth While Dennis Continues Working, Zach Plays Inside", 39, 5, new DateTime(1997, 5, 11, 18, 19, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 5, 11, 18, 43, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 51, 12, 769), new TimeSpan(0, 0, 47, 2, 152), 2, 8 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 100, 2, "1997-05-10 - Zach Swings", 38, 5, new DateTime(1997, 5, 10, 8, 19, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 5, 10, 8, 17, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 47, 2, 119), new TimeSpan(0, 0, 45, 12, 176), 2, 8 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 99, 2, "1997-05-08 - Beth Plays With Zach While Drew & Dennis Play Outside", 37, 5, new DateTime(1997, 5, 9, 18, 7, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 5, 9, 12, 42, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 45, 12, 142), new TimeSpan(0, 0, 35, 28, 259), 2, 8 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 98, 2, "1997-05-08 - Dennis & Beth Play With Drew & Zach", 36, 5, new DateTime(1997, 5, 8, 22, 11, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 5, 8, 22, 8, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 35, 28, 226), new TimeSpan(0, 0, 34, 36, 607), 2, 8 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 97, 2, "1997-05-05 - Beth Plays With Drew & Zach While Dennis Works On The Playground", 35, 5, new DateTime(1997, 5, 5, 17, 22, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 5, 5, 17, 16, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 34, 36, 574), new TimeSpan(0, 0, 28, 46, 224), 2, 8 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 96, 2, "1997-05-01 - Beth Plays With Zach", 34, 5, new DateTime(1997, 5, 1, 13, 46, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 5, 1, 13, 43, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 28, 46, 191), new TimeSpan(0, 0, 26, 36, 428), 2, 8 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 95, 2, "1997-04-24 - Gary, Sarah & Beth Play With Drew & Zach, Zach Swings", 33, 5, new DateTime(1997, 4, 24, 20, 46, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 4, 24, 16, 2, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 26, 26, 394), new TimeSpan(0, 0, 21, 44, 403), 2, 8 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 94, 2, "1997-04-23 - Beth, Drew & Zach Play", 32, 5, new DateTime(1997, 4, 23, 10, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 4, 23, 10, 11, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 21, 44, 369), new TimeSpan(0, 0, 16, 52, 211), 2, 8 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 93, 2, "1997-04-14 - Zach Plays With His Mobile", 31, 5, new DateTime(1997, 4, 14, 11, 11, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 4, 14, 11, 10, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 16, 46, 872), new TimeSpan(0, 0, 15, 35, 567), 2, 8 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 92, 2, "1997-04-11 - Beth, Drew & Zach Play", 30, 5, new DateTime(1997, 4, 11, 21, 35, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 4, 11, 21, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 15, 35, 534), new TimeSpan(0, 0, 10, 52, 952), 2, 8 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 91, 2, "1997-04-11 - Dennis Frost Sr.'s Birthday", 29, 5, new DateTime(1997, 4, 11, 11, 44, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 4, 11, 11, 41, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 10, 47, 513), new TimeSpan(0, 0, 10, 15, 658), 2, 8 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 90, 2, "1997-04-10 - Dennis, Drew & Zach Play", 28, 5, new DateTime(1997, 4, 10, 17, 18, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 4, 10, 17, 17, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 10, 15, 615), new TimeSpan(0, 0, 9, 33, 472), 2, 8 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 89, 2, "Easter, 1997 - Westgates Play Outside at Grandma's House (2)", 27, 5, new DateTime(1997, 3, 30, 17, 8, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 3, 30, 12, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 9, 28, 968), new TimeSpan(0, 0, 0, 7, 974), 2, 8 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 88, 2, "Easter, 1997 - Westgates Play Outside at Grandma's House (1)", 26, 5, new DateTime(1997, 3, 30, 12, 3, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 3, 30, 9, 50, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 3, 34, 306), new TimeSpan(0, 1, 44, 50, 484), 2, 7 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 74, 2, "1997-01-21 - Beth Plays With Zach", 12, 5, new DateTime(1997, 1, 21, 19, 39, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 1, 21, 19, 38, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 25, 25, 323), new TimeSpan(0, 0, 24, 45, 350), 2, 7 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 75, 2, "1997-01-28 - Beth Talks To Zach While He Tries To Move", 13, 5, new DateTime(1997, 1, 28, 15, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 1, 28, 15, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 34, 31, 670), new TimeSpan(0, 0, 25, 30, 762), 2, 7 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 76, 2, "1997-02-09 - Beth Films Drew Pottying", 14, 5, new DateTime(1997, 2, 9, 14, 22, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 2, 9, 14, 20, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 37, 16, 701), new TimeSpan(0, 0, 35, 6, 737), 2, 7 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 77, 2, "1997-01-28 - Ed Plays With Zachary", 15, 5, new DateTime(1997, 1, 28, 18, 33, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 1, 28, 18, 32, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 35, 6, 705), new TimeSpan(0, 0, 34, 37, 442), 2, 7 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 78, 2, "1997-02-09 - Dennis & Beth Play With Drew & Zach", 16, 5, new DateTime(1997, 2, 9, 21, 14, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 2, 9, 20, 46, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 59, 43, 646), new TimeSpan(0, 0, 37, 16, 734), 2, 7 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 79, 2, "1997-02-27 - Zach Swings", 17, 5, new DateTime(1997, 2, 27, 16, 20, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 2, 27, 16, 17, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 2, 48, 30), new TimeSpan(0, 0, 59, 49, 52), 2, 7 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 105, 2, "1997-05-23 - Maw-Maw's Birthday Party (1)", 43, 5, new DateTime(1997, 5, 23, 18, 41, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 5, 23, 17, 25, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 59, 2, 105), new TimeSpan(0, 0, 57, 32, 382), 2, 8 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 80, 4, "1997-03-02 - Dennis & Zach Entertain Each Other", 18, 5, new DateTime(1997, 3, 2, 15, 36, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 3, 2, 15, 35, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 4, 27, 96), new TimeSpan(0, 1, 2, 53, 603), 2, 7 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 82, 2, "1997-03-05 - Drew & Jordan Play", 20, 5, new DateTime(1997, 3, 5, 11, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 3, 5, 9, 49, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 20, 10, 572), new TimeSpan(0, 1, 10, 41, 103), 2, 7 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 83, 2, "1997-03-06 - Drew & Jordan Play", 21, 5, new DateTime(1997, 3, 6, 14, 47, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 3, 6, 14, 29, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 29, 33, 134), new TimeSpan(0, 1, 20, 16, 77), 2, 7 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 84, 2, "1997-03-10 - Drew & Jordan Play, Beth Plays With Elizabeth & Zach", 22, 5, new DateTime(1997, 3, 10, 15, 40, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 3, 10, 9, 18, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 33, 54, 996), new TimeSpan(0, 1, 29, 37, 638), 2, 7 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 85, 2, "1997-03-12 - Dennis & Drew Play With Zach", 23, 5, new DateTime(1997, 3, 12, 22, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 3, 12, 22, 12, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 37, 29, 143), new TimeSpan(0, 1, 33, 55, 29), 2, 7 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 86, 2, "1997-03-16 - Dennis Plays With Zach", 24, 5, new DateTime(1997, 3, 16, 19, 10, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 3, 16, 19, 8, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 39, 7, 241), new TimeSpan(0, 1, 37, 29, 176), 2, 7 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 87, 4, "1997-03-27 - Westgates Play Outside at Brenda's House", 25, 5, new DateTime(1997, 3, 27, 17, 37, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 3, 27, 16, 49, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 44, 50, 450), new TimeSpan(0, 1, 39, 7, 274), 2, 7 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 81, 2, "1997-03-04 - Drew & Jordan Play, Beth Plays With Zach", 19, 5, new DateTime(1997, 3, 4, 10, 59, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 3, 4, 9, 7, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 10, 35, 731), new TimeSpan(0, 1, 4, 27, 129), 2, 7 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 106, 2, "1997-05-23 - Maw-Maw's Birthday Party (2)", 44, 5, new DateTime(1997, 5, 23, 19, 32, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 5, 23, 18, 44, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 11, 51, 774), new TimeSpan(0, 1, 1, 53, 476), 2, 8 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 140, 2, "1997-10-18 - Zach Cries While Playing With Dennis", 1, 5, new DateTime(1997, 10, 18, 22, 22, 10, 0, DateTimeKind.Unspecified), new DateTime(1997, 10, 18, 22, 22, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 17, 851), new TimeSpan(0, 0, 0, 7, 941), 3, 1 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 141, 2, "1997-10-08 - Drew & Zach Play While Beth Films Their Room", 2, 5, new DateTime(1997, 10, 8, 16, 19, 3, 0, DateTimeKind.Unspecified), new DateTime(1997, 10, 8, 16, 17, 9, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 2, 11, 931), new TimeSpan(0, 0, 0, 17, 917), 3, 1 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 125, 2, "1997-12-23 - Dennis, Drew, Zachary & Austin Play at Grandma's House", 19, 5, new DateTime(1997, 12, 23, 11, 21, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 12, 23, 11, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 26, 8, 867), new TimeSpan(0, 0, 22, 55, 875), 4, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 126, 2, "Christmas Eve, 1997 - Westgate Family Christmas Eve at Grandpa's House", 20, 5, new DateTime(1997, 12, 24, 19, 57, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 12, 24, 18, 51, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 36, 54, 779), new TimeSpan(0, 0, 26, 8, 901), 4, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 127, 2, "Christmas Day, 1997 - Westgate Family Christmas at Grandma's House", 21, 5, new DateTime(1997, 12, 25, 11, 8, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 12, 25, 8, 58, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 52, 45, 95), new TimeSpan(0, 0, 37, 15, 300), 4, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 128, 2, "1997-12-28 - Zachary's Birthday Party at Grandma's House", 22, 5, new DateTime(1997, 12, 28, 17, 3, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 12, 28, 15, 42, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 4, 32, 168), new TimeSpan(0, 0, 52, 45, 128), 4, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 129, 2, "1997-12-30 - Dennis & Beth Play With Drew & Cousins Outside Grandma's House", 23, 5, new DateTime(1997, 12, 30, 12, 14, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 12, 30, 11, 51, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 20, 34, 630), new TimeSpan(0, 1, 4, 58, 695), 4, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 130, 2, "1998-01-09 - Zachary's Birthday Party with Frost Family", 24, 5, new DateTime(1998, 1, 9, 20, 23, 0, 0, DateTimeKind.Unspecified), new DateTime(1998, 1, 9, 19, 5, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 42, 48, 796), new TimeSpan(0, 1, 20, 40, 2), 4, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 124, 2, "1997-12-19 - Frost Family Christmas Party", 18, 5, new DateTime(1997, 12, 19, 20, 9, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 12, 19, 17, 55, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 22, 48, 634), new TimeSpan(0, 0, 2, 4, 591), 4, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 131, 2, "1998-02-04 - Dennis & Beth Play With Drew & Zachary", 25, 5, new DateTime(1998, 2, 4, 20, 19, 0, 0, DateTimeKind.Unspecified), new DateTime(1998, 2, 4, 20, 18, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 44, 21, 355), new TimeSpan(0, 1, 42, 54, 168), 4, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 133, 2, "1998-02-25 - Beth Says Hi to Zachary", 27, 5, new DateTime(1998, 2, 25, 19, 34, 0, 0, DateTimeKind.Unspecified), new DateTime(1998, 2, 25, 19, 34, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 45, 8, 669), new TimeSpan(0, 1, 44, 55, 623), 4, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 134, 2, "1998-03-07 - Beth Plays With Zachary", 28, 5, new DateTime(1998, 3, 7, 14, 19, 0, 0, DateTimeKind.Unspecified), new DateTime(1998, 3, 7, 14, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 47, 49, 896), new TimeSpan(0, 1, 45, 13, 441), 4, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 135, 2, "Dennis Plays With Drew, Zachary & Carly", 29, 5, new DateTime(1998, 3, 14, 13, 53, 0, 0, DateTimeKind.Unspecified), new DateTime(1998, 3, 14, 13, 52, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 48, 43, 350), new TimeSpan(0, 1, 47, 54, 301), 4, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 136, 2, "1998-03-22 - Dennis & Beth Play With Drew & Zachary On the Playground", 30, 5, new DateTime(1998, 3, 22, 15, 25, 0, 0, DateTimeKind.Unspecified), new DateTime(1998, 3, 22, 15, 16, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 51, 45, 65), new TimeSpan(0, 1, 48, 48, 422), 4, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 137, 2, "1998-03-30 - Zachary & Matt Swing Outside", 31, 5, new DateTime(1998, 3, 30, 10, 1, 0, 0, DateTimeKind.Unspecified), new DateTime(1998, 3, 30, 9, 49, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 52, 29, 976), new TimeSpan(0, 1, 51, 50, 437), 4, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 138, 2, "1998-03-30 - Drew, Zachary & Matt Eat Lunch", 32, 5, new DateTime(1998, 3, 30, 11, 20, 0, 0, DateTimeKind.Unspecified), new DateTime(1998, 3, 30, 11, 19, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 53, 15, 88), new TimeSpan(0, 1, 52, 30, 10), 4, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 132, 2, "1998-02-10 - Drew Tells the Rain to Stop", 26, 5, new DateTime(1998, 2, 10, 16, 56, 0, 0, DateTimeKind.Unspecified), new DateTime(1998, 2, 10, 16, 55, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 44, 52, 986), new TimeSpan(0, 1, 44, 21, 389), 4, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 73, 2, "1997-01-20 - Zach Naps", 11, 5, new DateTime(1997, 1, 20, 15, 26, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 1, 20, 15, 26, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 24, 39, 511), new TimeSpan(0, 0, 24, 15, 720), 2, 7 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 123, 2, "1997-12-15 - Beth Plays With Zach", 17, 5, new DateTime(1997, 12, 15, 15, 4, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 12, 15, 11, 58, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 1, 58, 885), new TimeSpan(0, 0, 0, 11, 445), 4, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 121, 2, "1997-12-05 - Beth Plays With Drew & Zach Around the Christmas Tree", 15, 5, new DateTime(1997, 12, 5, 20, 13, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 12, 5, 20, 9, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 55, 27, 788), new TimeSpan(0, 1, 51, 40, 60), 4, 1 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 107, 2, "1997-10-19 - Drew Plays With Tonka Trucks", 1, 5, new DateTime(1997, 10, 19, 17, 36, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 10, 19, 17, 34, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 2, 3, 56), new TimeSpan(0, 0, 0, 8, 809), 4, 1 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 108, 2, "1997-10-24 - Drew & Zach Play With Toy Bucket", 2, 5, new DateTime(1997, 10, 24, 19, 36, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 10, 24, 19, 34, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 3, 8, 255), new TimeSpan(0, 0, 2, 3, 90), 4, 1 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 109, 2, "1997-10-25 - Drew Eats While Dennis Plays with Zach", 3, 5, new DateTime(1997, 10, 25, 21, 39, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 10, 25, 21, 37, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 5, 17, 717), new TimeSpan(0, 0, 3, 8, 288), 4, 1 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 110, 2, "1997-10-29 - Dennis Plays With Zach", 4, 5, new DateTime(1997, 10, 29, 14, 19, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 10, 29, 14, 18, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 5, 57, 290), new TimeSpan(0, 0, 5, 17, 751), 4, 1 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 111, 2, "1997-11-05 - Dennis & Beth Play With Drew & Zach, Zach Walks", 5, 5, new DateTime(1997, 11, 5, 21, 51, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 11, 5, 20, 55, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 30, 5, 871), new TimeSpan(0, 0, 5, 57, 324), 4, 1 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 112, 2, "1997-11-07 - Jamie, Drew & Zach Play in the Kitchen", 6, 5, new DateTime(1997, 11, 7, 16, 33, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 11, 7, 16, 32, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 31, 25, 283), new TimeSpan(0, 0, 30, 12, 244), 4, 1 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 122, 2, "1997-12-11 - Beth, Drew & Zach Open Gifts with Julie & Carly", 16, 5, new DateTime(1997, 12, 11, 16, 2, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 12, 11, 14, 56, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 4, 25, 358), new TimeSpan(0, 1, 55, 33, 26), 4, 1 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 113, 2, "1997-11-13 - Zach Explores House, Drew & Zach Play", 7, 5, new DateTime(1997, 11, 13, 19, 46, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 11, 13, 14, 21, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 44, 42, 480), new TimeSpan(0, 0, 31, 25, 317), 4, 1 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 115, 2, "1997-11-17 - Beth & Dennis Play With Drew & Zach", 9, 5, new DateTime(1997, 11, 17, 17, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 11, 17, 17, 2, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 7, 25, 608), new TimeSpan(0, 0, 55, 51, 748), 4, 1 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 116, 2, "1997-11-21 - Beth & Dennis Play With Drew & Zach", 10, 5, new DateTime(1997, 11, 21, 22, 50, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 11, 21, 22, 35, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 19, 36, 539), new TimeSpan(0, 1, 7, 31, 147), 4, 1 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 117, 2, "1997-11-24 - Beth, Drew & Zach's Video Birthday Card for Austin", 11, 5, new DateTime(1997, 11, 24, 15, 50, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 11, 24, 15, 21, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 29, 16, 852), new TimeSpan(0, 1, 19, 36, 572), 4, 1 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 118, 2, "Thanksgiving, 1997 - Lunch at the Frosts' House With Jeff & Sue", 12, 5, new DateTime(1997, 11, 27, 13, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 11, 27, 12, 58, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 30, 29, 458), new TimeSpan(0, 1, 29, 22, 391), 4, 1 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 119, 12, "Thanksgiving, 1997 - Night at the Frosts' House", 13, 5, new DateTime(1997, 11, 27, 21, 23, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 11, 27, 20, 48, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 47, 52, 466), new TimeSpan(0, 1, 30, 29, 491), 4, 1 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 120, 2, "1997-12-03 - Beth, Dennis, Drew & Zach Decorate the Christmas Tree", 14, 5, new DateTime(1997, 12, 3, 20, 2, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 12, 3, 21, 39, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 51, 40, 26), new TimeSpan(0, 1, 47, 57, 571), 4, 1 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 114, 2, "1997-11-15 - Beth & Dennis Play With Drew & Zach, Ed & Hilda Visit", 8, 5, new DateTime(1997, 11, 15, 20, 6, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 11, 15, 19, 3, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 55, 51, 715), new TimeSpan(0, 0, 44, 42, 513), 4, 1 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 72, 2, "1997-01-13 - Zach Sucks His Thumb While Napping", 10, 5, new DateTime(1997, 1, 13, 21, 59, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 1, 13, 21, 59, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 24, 15, 687), new TimeSpan(0, 0, 23, 46, 758), 2, 7 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 71, 2, "1997-01-07 - Dennis & Beth Play With Drew", 9, 5, new DateTime(1997, 1, 7, 22, 38, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 1, 7, 22, 35, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 23, 46, 725), new TimeSpan(0, 0, 20, 27, 226), 2, 7 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 70, 2, "1997-01-06 - Zach Swings", 8, 5, new DateTime(1997, 1, 6, 16, 22, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 1, 6, 16, 22, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 20, 27, 192), new TimeSpan(0, 0, 20, 14, 146), 2, 7 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 20, 2, "1996-05-08 - Drew & Jordan Play", 20, 5, new DateTime(1996, 5, 8, 15, 25, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 5, 8, 12, 29, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 58, 36, 446), new TimeSpan(0, 0, 53, 18, 529), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 21, 2, "1996-05-09 - Dennis Swings Drew", 21, 5, new DateTime(1996, 5, 9, 18, 25, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 5, 9, 18, 13, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 2, 43, 493), new TimeSpan(0, 0, 58, 43, 253), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 22, 2, "1996-05-09 - Beth Gives Drew a Bath", 22, 5, new DateTime(1996, 5, 9, 21, 22, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 5, 9, 21, 19, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 5, 43, 139), new TimeSpan(0, 1, 2, 43, 526), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 23, 2, "1996-05-11 - Beth Wakes Drew Up", 23, 5, new DateTime(1996, 5, 11, 8, 11, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 5, 11, 8, 11, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 6, 7, 563), new TimeSpan(0, 1, 5, 43, 173), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 24, 2, "1996-05-12 - Dennis Plays With Drew In His Crib", 24, 5, new DateTime(1996, 5, 12, 17, 41, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 5, 12, 17, 40, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 6, 57, 80), new TimeSpan(0, 1, 6, 13, 202), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 25, 2, "1996-05-14 - Dennis Plays With Drew", 25, 5, new DateTime(1996, 5, 14, 21, 3, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 5, 14, 20, 59, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 12, 5, 188), new TimeSpan(0, 1, 7, 2, 752), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 19, 4, "1996-04-21 - Dennis Films Drew", 19, 5, new DateTime(1996, 4, 21, 21, 24, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 4, 21, 21, 18, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 53, 5, 449), new TimeSpan(0, 0, 48, 9, 120), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 26, 4, "1996-05-15 - Drew Learns to Crawl", 26, 5, new DateTime(1996, 5, 15, 21, 14, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 5, 15, 21, 3, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 17, 53, 168), new TimeSpan(0, 1, 12, 5, 221), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 28, 2, "1996-05-28 - Drew Plays While Dennis Eats", 28, 5, new DateTime(1996, 5, 28, 20, 34, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 5, 28, 20, 32, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 19, 57, 92), new TimeSpan(0, 1, 18, 55, 63), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 29, 2, "1996-06-14 - Drew Explores the Bathroom", 29, 5, new DateTime(1996, 6, 14, 10, 6, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 6, 14, 10, 5, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 21, 40, 663), new TimeSpan(0, 1, 20, 3, 399), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 30, 2, "1996-06-15 - Ed Plays With Drew in the Frosts' Backyard", 30, 5, new DateTime(1996, 6, 15, 13, 32, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 6, 15, 13, 32, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 22, 26, 775), new TimeSpan(0, 1, 21, 46, 668), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 31, 2, "1996-06-26 - Drew Plays With a Coaster", 31, 5, new DateTime(1996, 6, 26, 19, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 6, 26, 19, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 22, 53, 135), new TimeSpan(0, 1, 22, 32, 581), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 32, 2, "1996-06-30 - Dennis & Beth Play With Drew in the Kiddie Pool", 32, 5, new DateTime(1996, 6, 30, 13, 54, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 6, 30, 13, 52, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 24, 46, 314), new TimeSpan(0, 1, 22, 58, 774), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 33, 2, "1996-06-30 - Drew Eats His First Watermelon", 33, 5, new DateTime(1996, 6, 30, 21, 36, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 6, 30, 21, 33, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 28, 7, 48), new TimeSpan(0, 1, 24, 51, 720), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 27, 2, "1996-05-26 - Gary Plays With Drew", 27, 5, new DateTime(1996, 5, 26, 15, 50, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 5, 26, 15, 49, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 18, 49, 358), new TimeSpan(0, 1, 17, 59, 441), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 34, 2, "1996-07-01 - Dennis & Beth Play With Drew", 34, 5, new DateTime(1996, 7, 1, 21, 17, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 7, 1, 21, 9, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 29, 9, 444), new TimeSpan(0, 1, 28, 7, 82), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 18, 2, "1996-04-19 - Beth Tours the New House", 18, 5, new DateTime(1996, 4, 19, 18, 16, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 4, 19, 18, 14, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 48, 2, 479), new TimeSpan(0, 0, 46, 6, 931), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 16, 2, "1996-04-17 - Dennis & Ronnie Play Outside", 16, 5, new DateTime(1996, 4, 17, 18, 12, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 4, 17, 18, 8, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 44, 5, 343), new TimeSpan(0, 0, 40, 39, 137), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 2, 2, "1996-02-16 - Dennis Plays With Drew", 2, 5, new DateTime(1996, 2, 16, 21, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 2, 16, 21, 3, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 10, 20, 253), new TimeSpan(0, 0, 0, 11, 678), 1, 1 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 3, 2, "1996-02-18 - Dennis & Beth Watch Drew Play With a Balloon", 3, 5, new DateTime(1996, 2, 18, 14, 10, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 2, 18, 14, 9, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 11, 34, 661), new TimeSpan(0, 0, 10, 20, 286), 1, 1 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 4, 2, "1996-02-18 - Dennis & Beth Feed Drew on Bed", 4, 5, new DateTime(1996, 2, 18, 21, 1, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 2, 18, 20, 58, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 14, 1, 874), new TimeSpan(0, 0, 11, 34, 694), 1, 1 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 5, 2, "1996-02-19 - Dennis & Beth Play With Drew", 5, 5, new DateTime(1996, 2, 19, 19, 35, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 2, 19, 19, 28, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 16, 54, 80), new TimeSpan(0, 0, 14, 1, 908), 1, 1 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 6, 2, "[Redacted at Beth's Request]", 6, 5, new DateTime(1994, 2, 12, 20, 23, 0, 0, DateTimeKind.Unspecified), new DateTime(1994, 2, 12, 20, 8, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 27, 57, 910), new TimeSpan(0, 0, 16, 59, 118), 1, 1 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 7, 2, "[Redacted at Beth's Request]", 7, 5, new DateTime(1994, 2, 12, 20, 19, 0, 0, DateTimeKind.Unspecified), new DateTime(1994, 2, 12, 20, 18, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 28, 53, 498), new TimeSpan(0, 0, 27, 57, 943), 1, 1 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 17, 2, "1996-04-18 - Dennis & Beth Play With Drew", 17, 5, new DateTime(1996, 4, 18, 20, 51, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 4, 18, 20, 49, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 45, 46, 544), new TimeSpan(0, 0, 44, 11, 215), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 8, 12, "1996-04-12 - Dennis' & Beth's Wedding Ceremony", 8, 5, new DateTime(1996, 4, 12, 14, 10, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 4, 12, 14, 2, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 8, 6, 252), new TimeSpan(0, 0, 0, 11, 645), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 10, 2, "1996-04-17 - Ronnie Climbs a Tree", 10, 5, new DateTime(1996, 4, 17, 18, 5, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 4, 17, 18, 4, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 21, 26, 185), new TimeSpan(0, 0, 20, 35, 734), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 11, 2, "1996-04-17 - Beth Films Around Grandma's House", 11, 5, new DateTime(1996, 4, 16, 11, 42, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 4, 16, 11, 40, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 23, 33, 779), new TimeSpan(0, 0, 21, 26, 218), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 12, 2, "1996-04-16 - Dennis & Beth Play With Drew", 12, 5, new DateTime(1996, 4, 16, 15, 57, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 4, 16, 15, 40, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 25, 20, 85), new TimeSpan(0, 0, 23, 33, 813), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 13, 2, "1996-04-16 - Car Ride With Brenda", 13, 5, new DateTime(1996, 4, 16, 17, 38, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 4, 16, 17, 17, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 28, 22, 601), new TimeSpan(0, 0, 25, 20, 119), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 14, 2, "1996-04-16 - Driving to Grandpa's House", 14, 5, new DateTime(1996, 4, 17, 11, 46, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 4, 17, 11, 37, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 33, 42, 521), new TimeSpan(0, 0, 28, 22, 634), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 15, 2, "1996-04-17 - Dennis, Beth & Drew Explore Grandpa's Cabin", 15, 5, new DateTime(1996, 4, 17, 12, 25, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 4, 17, 12, 2, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 40, 33, 464), new TimeSpan(0, 0, 33, 42, 554), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 9, 12, "1996-04-12 - Dennis' & Beth's Wedding Reception", 9, 5, new DateTime(1996, 4, 12, 14, 31, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 4, 12, 14, 18, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 20, 29, 795), new TimeSpan(0, 0, 8, 6, 286), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 139, 2, "1998-03-31 - Drew, Zachary & Matt Eat Breakfast, Play", 33, 5, new DateTime(1998, 3, 31, 19, 33, 0, 0, DateTimeKind.Unspecified), new DateTime(1998, 3, 31, 7, 11, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 57, 48, 28), new TimeSpan(0, 1, 53, 15, 122), 4, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 35, 2, "1996-07-06 - Dennis Plays With Drew, Austin & Ronnie", 35, 5, new DateTime(1996, 7, 6, 10, 55, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 7, 6, 10, 44, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 34, 8, 543), new TimeSpan(0, 1, 29, 9, 478), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 37, 2, "1996-08-03 - Dennis & Beth Play With Drew", 37, 5, new DateTime(1996, 8, 3, 21, 52, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 8, 3, 21, 49, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 37, 40, 822), new TimeSpan(0, 1, 36, 15, 703), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 56, 2, "1996-12-08 - Dennis & Beth Play With Drew While He Eats", 56, 5, new DateTime(1996, 12, 8, 18, 37, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 12, 8, 18, 37, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 4, 54, 261), new TimeSpan(0, 0, 4, 35, 8), 1, 5 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 57, 2, "1996-12-09 - Drew Plays Around the Christmas Tree", 57, 5, new DateTime(1996, 12, 9, 18, 37, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 12, 9, 18, 32, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 9, 8, 715), new TimeSpan(0, 0, 4, 54, 294), 1, 5 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 58, 2, "1996-12-13 - Dennis & Beth Play With Drew", 58, 5, new DateTime(1996, 12, 13, 16, 42, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 12, 13, 16, 39, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 10, 12, 312), new TimeSpan(0, 0, 9, 8, 748), 1, 5 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 59, 2, "1996-12-16 - Drew Watches TV", 59, 5, new DateTime(1996, 12, 16, 16, 24, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 12, 16, 16, 22, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 12, 9, 963), new TimeSpan(0, 0, 10, 12, 345), 1, 5 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 60, 2, "1996-12-19 - Beth Surveys the Christmas Decorations", 60, 5, new DateTime(1996, 12, 19, 12, 22, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 12, 19, 12, 20, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 13, 47, 860), new TimeSpan(0, 0, 12, 9, 996), 1, 5 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 61, 2, "1996-12-20 - Dennis & Beth Play With Drew", 61, 5, new DateTime(1996, 12, 20, 22, 5, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 12, 20, 21, 58, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 20, 20, 519), new TimeSpan(0, 0, 13, 53, 866), 1, 5 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 55, 2, "1996-12-07 - Drew Watches TV", 55, 5, new DateTime(1996, 12, 7, 17, 39, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 12, 7, 17, 39, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 4, 34, 975), new TimeSpan(0, 0, 4, 8, 782), 1, 5 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 62, 2, "Christmas Eve, 1996 - Dennis Plays With Drew", 62, 5, new DateTime(1996, 12, 24, 12, 20, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 12, 24, 12, 20, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 20, 23, 189), new TimeSpan(0, 0, 20, 20, 519), 1, 5 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 64, 2, "Christmas Eve, 1996 - Ed & Hilda's Christmas Eve Party", 2, 5, new DateTime(1996, 12, 24, 22, 51, 40, 407, DateTimeKind.Unspecified), new DateTime(1996, 12, 24, 17, 37, 56, 197, DateTimeKind.Unspecified), new TimeSpan(0, 0, 18, 30, 409), new TimeSpan(0, 0, 7, 3, 14), 2, 6 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 65, 2, "Christmas Eve, 1996 - Midnight", 3, 5, new DateTime(1996, 12, 25, 0, 41, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 12, 25, 0, 18, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 18, 51, 497), new TimeSpan(0, 0, 18, 30, 442), 2, 6 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 66, 2, "Christmas Day, 1996 - Morning, Opening Gifts", 4, 5, new DateTime(1996, 12, 25, 8, 46, 18, 150, DateTimeKind.Unspecified), new DateTime(1996, 12, 25, 8, 13, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 49, 3, 840), new TimeSpan(0, 0, 18, 51, 530), 2, 6 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 67, 2, "Christmas Day, 1996 - Ed & Hilda's House", 5, 5, new DateTime(1996, 12, 25, 12, 38, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 12, 25, 11, 55, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 8, 34, 679), new TimeSpan(0, 0, 0, 9, 609), 2, 7 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 68, 2, "1997-01-03 - Dennis Holds Zach, Drew Finds Wipes", 6, 5, new DateTime(1997, 1, 3, 22, 5, 5, 873, DateTimeKind.Unspecified), new DateTime(1997, 1, 3, 22, 4, 30, 757, DateTimeKind.Unspecified), new TimeSpan(0, 0, 9, 25, 197), new TimeSpan(0, 0, 8, 50, 82), 2, 7 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 69, 2, "1997-01-05 - Dennis & Beth Play With Drew, Zach Naps", 7, 5, new DateTime(1997, 1, 5, 19, 19, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 1, 5, 19, 2, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 20, 14, 112), new TimeSpan(0, 0, 9, 34, 607), 2, 7 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 63, 2, "Christmas Eve, 1996 - Dennis Plays With Drew", 1, 5, new DateTime(1996, 12, 24, 12, 29, 22, 613, DateTimeKind.Unspecified), new DateTime(1996, 12, 24, 12, 20, 50, 87, DateTimeKind.Unspecified), new TimeSpan(0, 0, 6, 54, 981), new TimeSpan(0, 0, 0, 0, 0), 2, 6 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 36, 2, "1996-08-03 - Ed's Birthday Party", 36, 5, new DateTime(1996, 8, 3, 19, 18, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 8, 3, 19, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 36, 15, 670), new TimeSpan(0, 1, 34, 14, 849), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 54, 2, "1996-11-29 - Dennis & Beth Dance With Drew", 54, 5, new DateTime(1996, 11, 29, 18, 37, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 11, 29, 18, 32, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 4, 3, 210), new TimeSpan(0, 0, 2, 39, 660), 1, 5 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 52, 2, "Halloween, 1996 - Drew Wears His Costume, Plays With Dennis & Beth", 52, 5, new DateTime(1996, 10, 31, 19, 58, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 10, 31, 17, 58, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 2, 11, 98), new TimeSpan(0, 0, 0, 7, 508), 1, 5 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 38, 2, "1996-08-04 - Drew Takes a Bath", 38, 5, new DateTime(1996, 8, 4, 21, 29, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 8, 4, 21, 28, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 38, 39, 380), new TimeSpan(0, 1, 37, 40, 855), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 39, 2, "1996-08-21 - Beth Plays With Drew", 39, 5, new DateTime(1996, 8, 21, 15, 19, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 8, 21, 15, 8, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 45, 34, 662), new TimeSpan(0, 1, 38, 44, 752), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 40, 2, "1996-08-22 - Dennis Plays With Drew on the Driveway", 40, 5, new DateTime(1996, 8, 22, 18, 36, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 8, 22, 18, 29, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 52, 10, 924), new TimeSpan(0, 1, 45, 34, 695), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 41, 2, "1996-09-17 - Beth Plays With Drew", 41, 5, new DateTime(1996, 9, 17, 16, 51, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 9, 17, 15, 52, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 56, 52, 339), new TimeSpan(0, 1, 52, 16, 396), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 42, 2, "1996-09-20 - Drew Walks Around the Living Room", 42, 5, new DateTime(1996, 9, 20, 21, 22, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 9, 20, 21, 21, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 57, 21, 234), new TimeSpan(0, 1, 56, 57, 911), 1, 2 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 43, 2, "1996-09-26 - Drew Dances To The Radio", 43, 5, new DateTime(1996, 9, 26, 15, 1, 29, 0, DateTimeKind.Unspecified), new DateTime(1996, 9, 26, 15, 0, 51, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 44, 278), new TimeSpan(0, 0, 0, 5, 806), 1, 3 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 53, 2, "1996-11-15 - Beth Plays With Drew", 53, 5, new DateTime(1996, 11, 15, 15, 52, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 11, 15, 15, 50, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 2, 34, 321), new TimeSpan(0, 0, 2, 16, 403), 1, 5 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 44, 2, "1996-09-27 - Drew Plays With Austin & Ronnie", 44, 5, new DateTime(1996, 9, 27, 22, 23, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 9, 27, 22, 19, 3, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 4, 7, 347), new TimeSpan(0, 0, 0, 44, 311), 1, 3 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 46, 2, "1996-06-15 - Chelsea Williams' Birthday Party", 46, 5, new DateTime(1996, 6, 15, 19, 37, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 6, 15, 17, 23, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 17, 47, 833), new TimeSpan(0, 0, 0, 10, 911), 1, 4 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 47, 2, "1996-08-24 - Kim's Baby Shower", 47, 5, new DateTime(1996, 8, 24, 14, 49, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 8, 24, 13, 20, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 34, 33, 171), new TimeSpan(0, 0, 17, 53, 940), 1, 4 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 48, 2, "1996-09-28 - Drew & Matthew Play", 48, 5, new DateTime(1996, 9, 28, 17, 47, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 9, 28, 17, 46, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 35, 38, 636), new TimeSpan(0, 0, 34, 39, 110), 1, 4 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 49, 2, "1996-09-29 - Drew's Birthday Party", 49, 5, new DateTime(1996, 9, 29, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 9, 29, 13, 28, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 44, 48, 853), new TimeSpan(0, 0, 35, 38, 670), 1, 4 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 50, 2, "1996-10-02 - Dennis & Beth Play With Drew", 50, 5, new DateTime(1996, 10, 2, 20, 51, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 10, 2, 20, 37, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 52, 13, 597), new TimeSpan(0, 0, 44, 54, 292), 1, 4 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 51, 2, "1996-10-03 - Dennis & Beth Play With Drew", 51, 5, new DateTime(1996, 10, 3, 19, 22, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 10, 3, 19, 14, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 54, 11, 48), new TimeSpan(0, 0, 52, 13, 631), 1, 4 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 45, 2, "Christmas, 1996 - Dennis, Beth & Drew Open Gifts On Christmas Morning", 45, 5, new DateTime(1996, 12, 25, 8, 51, 19, 0, DateTimeKind.Unspecified), new DateTime(1996, 12, 25, 8, 49, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 6, 14, 140), new TimeSpan(0, 0, 4, 7, 381), 1, 3 });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "ClipId", "ClipCameraOperatorId", "ClipName", "ClipNumber", "ClipReviewerId", "ClipTimeEnd", "ClipTimeStart", "ClipVidTimeEnd", "ClipVidTimeStart", "SourceId", "SourceSegment" },
                values: new object[] { 142, 2, "Dennis & Beth Play With Drew & Zach", 1, 5, new DateTime(1999, 2, 5, 21, 4, 24, 727, DateTimeKind.Unspecified), new DateTime(1999, 2, 5, 20, 59, 2, 310, DateTimeKind.Unspecified), new TimeSpan(0, 0, 3, 3, 617), new TimeSpan(0, 0, 0, 0, 0), 19, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 1, 4 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 88, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 88, 14 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 88, 12 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 88, 10 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 88, 7 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 87, 23 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 87, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 87, 14 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 87, 12 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 86, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 86, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 85, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 85, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 84, 20 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 84, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 84, 8 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 84, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 83, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 83, 8 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 82, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 81, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 81, 8 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 81, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 88, 22 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 80, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 88, 23 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 89, 7 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 98, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 97, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 97, 14 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 96, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 96, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 95, 20 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 95, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 95, 8 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 95, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 94, 20 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 94, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 94, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 93, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 93, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 92, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 92, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 91, 2 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 90, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 89, 24 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 89, 23 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 89, 14 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 89, 12 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 89, 10 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 88, 24 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 99, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 80, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 79, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 63, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 63, 4 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 62, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 62, 4 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 61, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 61, 4 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 60, 4 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 59, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 58, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 57, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 57, 4 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 56, 4 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 55, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 54, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 53, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 52, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 52, 11 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 51, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 50, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 49, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 49, 9 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 49, 8 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 49, 2 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 64, 4 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 79, 20 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 64, 8 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 65, 4 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 79, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 78, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 77, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 76, 3 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 76, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 75, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 75, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 74, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 74, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 73, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 72, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 71, 20 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 71, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 70, 20 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 70, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 70, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 69, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 69, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 68, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 67, 9 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 67, 8 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 67, 4 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 66, 4 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 64, 9 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 48, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 99, 14 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 100, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 128, 23 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 128, 22 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 128, 12 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 128, 10 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 128, 2 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 127, 24 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 127, 23 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 127, 22 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 127, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 127, 12 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 127, 10 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 127, 4 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 126, 24 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 126, 23 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 126, 22 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 126, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 126, 12 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 126, 4 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 125, 23 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 125, 22 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 125, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 125, 12 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 125, 10 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 128, 24 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 124, 9 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 129, 10 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 129, 14 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 139, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 139, 8 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 138, 8 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 137, 20 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 137, 14 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 137, 8 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 136, 20 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 136, 14 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 135, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 135, 8 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 134, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 134, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 133, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 133, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 132, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 131, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 131, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 130, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 130, 9 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 130, 8 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 130, 2 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 129, 23 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 129, 22 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 129, 12 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 99, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 124, 8 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 123, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 111, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 110, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 110, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 109, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 109, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 108, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 107, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 141, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 140, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 106, 9 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 106, 2 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 105, 9 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 105, 2 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 104, 19 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 104, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 104, 14 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 103, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 103, 8 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 102, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 101, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 101, 14 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 100, 20 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 100, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 111, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 124, 4 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 112, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 113, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 123, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 122, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 122, 8 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 122, 4 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 121, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 121, 4 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 120, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 120, 4 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 120, 1 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 119, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 119, 9 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 119, 8 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 119, 4 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 118, 23 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 118, 9 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 118, 8 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 117, 23 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 117, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 117, 2 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 116, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 115, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 114, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 113, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 112, 21 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 48, 8 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 82, 8 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 12, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 13, 23 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 36, 9 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 13, 12 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 12, 12 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 12, 10 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 11, 23 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 37, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 38, 3 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 11, 12 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 11, 10 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 10, 23 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 39, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 10, 14 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 10, 12 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 10, 10 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 40, 14 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 41, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 9, 9 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 9, 8 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 9, 5 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 42, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 8, 9 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 8, 8 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 8, 5 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 43, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 36, 8 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 36, 2 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 14, 12 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 35, 23 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 21, 20 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 21, 14 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 24, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 20, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 25, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 20, 14 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 20, 8 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 27, 8 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 27, 9 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 27, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 19, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 28, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 44, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 17, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 30, 14 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 30, 20 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 31, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 16, 23 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 32, 14 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 16, 14 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 16, 12 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 34, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 15, 14 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 15, 12 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 35, 10 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 35, 12 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 29, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 44, 23 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 22, 3 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 1, 9 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 47, 9 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 45, 4 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 1, 8 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 46, 8 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 3, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 47, 8 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 2, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 46, 9 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 46, 2 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 5, 16 });

            migrationBuilder.InsertData(
                table: "ClipCollections",
                columns: new[] { "ClipId", "CollectionId" },
                values: new object[] { 46, 16 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 136, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 113, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 114, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 1, 7 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 114, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 114, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 16, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 1, 6 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 114, 6 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 113, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 16, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 16, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 112, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 112, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 112, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 1, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 17, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 111, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 111, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 111, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 113, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 114, 7 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 5, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 136, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 118, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 118, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 118, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 135, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 135, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 135, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 15, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 117, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 117, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 117, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 114, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 15, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 2, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 116, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 116, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 116, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 115, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 115, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 136, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 115, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 115, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 136, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 15, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 111, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 17, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 137, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 106, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 106, 8 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 106, 7 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 106, 6 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 106, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 106, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 106, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 20, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 105, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 105, 8 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 105, 7 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 140, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 105, 6 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 105, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 105, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 20, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 139, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 104, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 104, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 104, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 139, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 21, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 21, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 103, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 105, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 140, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 140, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 138, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 118, 6 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 110, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 110, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 110, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 17, 6 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 17, 7 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 109, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 109, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 109, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 109, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 18, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 137, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 108, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 138, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 138, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 108, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 108, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 19, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 107, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 107, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 19, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 141, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 141, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 141, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 17, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 118, 7 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 135, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 118, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 127, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 127, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 127, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 8, 8 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 130, 8 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 130, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 4, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 4, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 9, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 9, 3 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 9, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 127, 9 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 126, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 126, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 126, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 126, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 126, 1 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 9, 6 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 9, 7 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 131, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 9, 8 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 131, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 131, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 131, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 126, 10 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 4, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 127, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 8, 7 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 130, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 5, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 5, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 129, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 130, 3 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 129, 9 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 129, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 129, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 129, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 6, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 130, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 130, 7 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 7, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 130, 6 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 128, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 128, 9 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 128, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 128, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 128, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 8, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 8, 3 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 8, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 8, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 8, 6 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 130, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 118, 8 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 125, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 125, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 121, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 121, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 12, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 120, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 120, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 120, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 3, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 120, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 134, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 13, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 13, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 121, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 119, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 119, 7 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 119, 6 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 119, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 119, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 119, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 134, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 14, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 14, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 2, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 2, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 14, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 119, 8 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 125, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 12, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 103, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 125, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 132, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 10, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 10, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 132, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 124, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 3, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 124, 8 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 124, 7 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 124, 6 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 124, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 12, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 124, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 124, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 3, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 11, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 11, 9 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 123, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 123, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 133, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 133, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 122, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 122, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 122, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 124, 3 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 47, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 24, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 102, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 69, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 40, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 40, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 68, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 68, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 68, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 68, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 67, 8 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 67, 7 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 67, 6 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 67, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 67, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 67, 3 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 67, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 41, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 41, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 66, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 66, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 66, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 42, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 65, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 65, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 42, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 64, 8 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 64, 7 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 64, 6 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 64, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 69, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 69, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 69, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 40, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 77, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 77, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 36, 7 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 76, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 76, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 76, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 36, 8 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 75, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 75, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 37, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 37, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 74, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 74, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 64, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 37, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 73, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 38, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 72, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 72, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 38, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 71, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 71, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 71, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 71, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 39, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 70, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 70, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 39, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 73, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 64, 3 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 64, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 42, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 54, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 54, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 53, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 53, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 46, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 52, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 52, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 52, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 46, 3 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 46, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 51, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 51, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 51, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 54, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 46, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 50, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 50, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 46, 6 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 49, 7 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 49, 6 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 49, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 49, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 49, 3 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 49, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 46, 7 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 47, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 48, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 48, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 50, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 77, 6 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 55, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 56, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 43, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 63, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 63, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 63, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 43, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 62, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 62, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 62, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 44, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 61, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 61, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 61, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 44, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 55, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 44, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 59, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 59, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 45, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 58, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 58, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 58, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 45, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 57, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 57, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 57, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 45, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 56, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 56, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 60, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 77, 7 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 77, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 36, 6 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 96, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 26, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 26, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 95, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 95, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 95, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 26, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 94, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 94, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 94, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 27, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 27, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 27, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 96, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 93, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 27, 6 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 27, 7 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 92, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 92, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 92, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 28, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 91, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 91, 3 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 91, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 28, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 90, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 90, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 90, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 93, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 28, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 25, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 97, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 102, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 102, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 102, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 22, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 101, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 101, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 101, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 101, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 22, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 23, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 100, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 100, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 23, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 25, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 47, 7 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 99, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 99, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 99, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 24, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 24, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 98, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 98, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 98, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 98, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 25, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 97, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 97, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 97, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 99, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 21, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 89, 9 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 89, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 84, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 84, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 84, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 34, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 83, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 83, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 35, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 35, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 82, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 82, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 35, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 139, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 81, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 34, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 81, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 35, 9 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 80, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 80, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 36, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 79, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 79, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 36, 3 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 36, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 36, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 78, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 78, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 78, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 78, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 81, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 89, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 34, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 85, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 89, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 29, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 29, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 30, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 88, 9 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 88, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 88, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 88, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 30, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 30, 6 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 31, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 31, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 31, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 85, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 32, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 87, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 87, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 87, 1 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 32, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 32, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 33, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 33, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 86, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 86, 4 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 86, 2 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 33, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 85, 11 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 85, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 87, 5 });

            migrationBuilder.InsertData(
                table: "ClipPersons",
                columns: new[] { "ClipId", "PersonId" },
                values: new object[] { 139, 11 });

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
                name: "FileFileCategories");

            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.DropTable(
                name: "FileCategories");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Clips");

            migrationBuilder.DropTable(
                name: "FileFormats");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Sources");

            migrationBuilder.DropTable(
                name: "SourceFormats");
        }
    }
}
