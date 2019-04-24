using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeepBong.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Libraries",
                columns: table => new
                {
                    LibraryId = table.Column<Guid>(nullable: false),
                    AlbumName = table.Column<string>(nullable: true),
                    Label = table.Column<string>(nullable: true),
                    Catalog = table.Column<string>(nullable: true),
                    MBID = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libraries", x => x.LibraryId);
                });

            migrationBuilder.CreateTable(
                name: "Programmes",
                columns: table => new
                {
                    ProgrammeId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Year = table.Column<string>(maxLength: 4, nullable: true),
                    Channel = table.Column<string>(nullable: true),
                    AudioComposer = table.Column<string>(nullable: true),
                    Logo = table.Column<string>(nullable: true),
                    IsLibraryMusic = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programmes", x => x.ProgrammeId);
                });

            migrationBuilder.CreateTable(
                name: "LibraryProgrammes",
                columns: table => new
                {
                    ProgrammeId = table.Column<Guid>(nullable: false),
                    LibraryId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryProgrammes", x => new { x.ProgrammeId, x.LibraryId });
                    table.ForeignKey(
                        name: "FK_LibraryProgrammes_Libraries_LibraryId",
                        column: x => x.LibraryId,
                        principalTable: "Libraries",
                        principalColumn: "LibraryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LibraryProgrammes_Programmes_ProgrammeId",
                        column: x => x.ProgrammeId,
                        principalTable: "Programmes",
                        principalColumn: "ProgrammeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    TrackId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Subtitle = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ProgrammeId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.TrackId);
                    table.ForeignKey(
                        name: "FK_Tracks_Programmes_ProgrammeId",
                        column: x => x.ProgrammeId,
                        principalTable: "Programmes",
                        principalColumn: "ProgrammeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Samples",
                columns: table => new
                {
                    SampleId = table.Column<Guid>(nullable: false),
                    Duration = table.Column<string>(nullable: true),
                    SampleRate = table.Column<int>(nullable: false),
                    SampleCount = table.Column<int>(nullable: false),
                    Channels = table.Column<int>(nullable: false),
                    BitRate = table.Column<int>(nullable: false),
                    BitRateMode = table.Column<string>(nullable: false),
                    BitDepth = table.Column<int>(nullable: false),
                    Codec = table.Column<string>(nullable: true),
                    Compression = table.Column<string>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    Waveform = table.Column<string>(nullable: true),
                    Spectrograph = table.Column<string>(nullable: true),
                    TrackId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Samples", x => x.SampleId);
                    table.ForeignKey(
                        name: "FK_Samples_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "TrackId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LibraryProgrammes_LibraryId",
                table: "LibraryProgrammes",
                column: "LibraryId");

            migrationBuilder.CreateIndex(
                name: "IX_Samples_TrackId",
                table: "Samples",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_ProgrammeId",
                table: "Tracks",
                column: "ProgrammeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LibraryProgrammes");

            migrationBuilder.DropTable(
                name: "Samples");

            migrationBuilder.DropTable(
                name: "Libraries");

            migrationBuilder.DropTable(
                name: "Tracks");

            migrationBuilder.DropTable(
                name: "Programmes");
        }
    }
}
