using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeepBong.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Broadcasters",
                columns: table => new
                {
                    BroadcasterId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Broadcasters", x => x.BroadcasterId);
                });

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
                name: "TrackLists",
                columns: table => new
                {
                    TrackListId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Library = table.Column<bool>(nullable: false),
                    Composer = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackLists", x => x.TrackListId);
                });

            migrationBuilder.CreateTable(
                name: "Channels",
                columns: table => new
                {
                    ChannelId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Commencement = table.Column<DateTime>(nullable: true),
                    Closed = table.Column<DateTime>(nullable: true),
                    BroadcasterId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channels", x => x.ChannelId);
                    table.ForeignKey(
                        name: "FK_Channels_Broadcasters_BroadcasterId",
                        column: x => x.BroadcasterId,
                        principalTable: "Broadcasters",
                        principalColumn: "BroadcasterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    TrackId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Variant = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    TrackListId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.TrackId);
                    table.ForeignKey(
                        name: "FK_Tracks_TrackLists_TrackListId",
                        column: x => x.TrackListId,
                        principalTable: "TrackLists",
                        principalColumn: "TrackListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Programmes",
                columns: table => new
                {
                    ProgrammeId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    AirDate = table.Column<DateTime>(nullable: true),
                    LogoLocation = table.Column<string>(nullable: true),
                    ChannelId = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programmes", x => x.ProgrammeId);
                    table.ForeignKey(
                        name: "FK_Programmes_Channels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channels",
                        principalColumn: "ChannelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Samples",
                columns: table => new
                {
                    SampleId = table.Column<Guid>(nullable: false),
                    SampleRate = table.Column<int>(nullable: false),
                    SampleCount = table.Column<int>(nullable: false),
                    AudioChannelCount = table.Column<int>(nullable: false),
                    BitRate = table.Column<int>(nullable: false),
                    BitRateMode = table.Column<string>(nullable: false),
                    BitDepth = table.Column<int>(nullable: false),
                    Codec = table.Column<string>(nullable: true),
                    Compression = table.Column<string>(nullable: false),
                    Fingerprint = table.Column<string>(nullable: true),
                    OtherAttributes = table.Column<string>(nullable: true),
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

            migrationBuilder.CreateTable(
                name: "ProgrammeTrackLists",
                columns: table => new
                {
                    ProgrammeId = table.Column<Guid>(nullable: false),
                    TrackListId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammeTrackLists", x => new { x.ProgrammeId, x.TrackListId });
                    table.ForeignKey(
                        name: "FK_ProgrammeTrackLists_Programmes_ProgrammeId",
                        column: x => x.ProgrammeId,
                        principalTable: "Programmes",
                        principalColumn: "ProgrammeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgrammeTrackLists_TrackLists_TrackListId",
                        column: x => x.TrackListId,
                        principalTable: "TrackLists",
                        principalColumn: "TrackListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Channels_BroadcasterId",
                table: "Channels",
                column: "BroadcasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Programmes_ChannelId",
                table: "Programmes",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgrammeTrackLists_TrackListId",
                table: "ProgrammeTrackLists",
                column: "TrackListId");

            migrationBuilder.CreateIndex(
                name: "IX_Samples_Fingerprint",
                table: "Samples",
                column: "Fingerprint",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Samples_TrackId",
                table: "Samples",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_TrackListId",
                table: "Tracks",
                column: "TrackListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Libraries");

            migrationBuilder.DropTable(
                name: "ProgrammeTrackLists");

            migrationBuilder.DropTable(
                name: "Samples");

            migrationBuilder.DropTable(
                name: "Programmes");

            migrationBuilder.DropTable(
                name: "Tracks");

            migrationBuilder.DropTable(
                name: "Channels");

            migrationBuilder.DropTable(
                name: "TrackLists");

            migrationBuilder.DropTable(
                name: "Broadcasters");
        }
    }
}
