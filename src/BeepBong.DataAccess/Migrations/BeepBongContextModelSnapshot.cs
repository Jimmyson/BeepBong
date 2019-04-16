﻿// <auto-generated />
using System;
using BeepBong.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BeepBong.DataAccess.Migrations
{
    [DbContext(typeof(BeepBongContext))]
    partial class BeepBongContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity("BeepBong.Domain.Models.Library", b =>
                {
                    b.Property<Guid>("LibraryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AlbumName");

                    b.Property<string>("Catalog");

                    b.Property<DateTime>("Created");

                    b.Property<string>("Label");

                    b.Property<DateTime?>("LastModified");

                    b.Property<string>("MBID");

                    b.HasKey("LibraryId");

                    b.ToTable("Libraries");
                });

            modelBuilder.Entity("BeepBong.Domain.Models.LibraryProgramme", b =>
                {
                    b.Property<Guid>("ProgrammeId");

                    b.Property<Guid>("LibraryId");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("LastModified");

                    b.HasKey("ProgrammeId", "LibraryId");

                    b.HasIndex("LibraryId");

                    b.ToTable("LibraryProgrammes");
                });

            modelBuilder.Entity("BeepBong.Domain.Models.Programme", b =>
                {
                    b.Property<Guid>("ProgrammeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AudioComposer");

                    b.Property<string>("Channel");

                    b.Property<DateTime>("Created");

                    b.Property<bool>("IsLibraryMusic");

                    b.Property<DateTime?>("LastModified");

                    b.Property<string>("Name");

                    b.Property<string>("Year")
                        .HasMaxLength(4);

                    b.HasKey("ProgrammeId");

                    b.ToTable("Programmes");
                });

            modelBuilder.Entity("BeepBong.Domain.Models.Sample", b =>
                {
                    b.Property<Guid>("SampleId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BitRate");

                    b.Property<string>("BitRateMode")
                        .IsRequired();

                    b.Property<int>("Channels");

                    b.Property<string>("Codec");

                    b.Property<string>("Compression")
                        .IsRequired();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Duration");

                    b.Property<DateTime?>("LastModified");

                    b.Property<string>("Notes");

                    b.Property<int>("SampleCount");

                    b.Property<int>("SampleRate");

                    b.Property<Guid>("TrackId");

                    b.HasKey("SampleId");

                    b.HasIndex("TrackId");

                    b.ToTable("Samples");
                });

            modelBuilder.Entity("BeepBong.Domain.Models.Track", b =>
                {
                    b.Property<Guid>("TrackId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description");

                    b.Property<DateTime?>("LastModified");

                    b.Property<string>("Name");

                    b.Property<Guid>("ProgrammeId");

                    b.Property<string>("Subtitle");

                    b.HasKey("TrackId");

                    b.HasIndex("ProgrammeId");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("BeepBong.Domain.Models.LibraryProgramme", b =>
                {
                    b.HasOne("BeepBong.Domain.Models.Library", "Library")
                        .WithMany("LibraryProgrammes")
                        .HasForeignKey("LibraryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BeepBong.Domain.Models.Programme", "Programme")
                        .WithMany("LibraryProgrammes")
                        .HasForeignKey("ProgrammeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BeepBong.Domain.Models.Sample", b =>
                {
                    b.HasOne("BeepBong.Domain.Models.Track", "Track")
                        .WithMany("Samples")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BeepBong.Domain.Models.Track", b =>
                {
                    b.HasOne("BeepBong.Domain.Models.Programme", "Programme")
                        .WithMany("Tracks")
                        .HasForeignKey("ProgrammeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
