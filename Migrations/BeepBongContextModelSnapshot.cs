﻿// <auto-generated />
using System;
using BeepBong;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BeepBong.Migrations
{
    [DbContext(typeof(BeepBongContext))]
    partial class BeepBongContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity("BeepBong.Models.Programme", b =>
                {
                    b.Property<Guid>("ProgrammeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AudioComposer");

                    b.Property<string>("Channel");

                    b.Property<string>("Name");

                    b.Property<string>("Year");

                    b.HasKey("ProgrammeId");

                    b.ToTable("Programmes");
                });

            modelBuilder.Entity("BeepBong.Models.Sample", b =>
                {
                    b.Property<Guid>("SampleId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BitRate");

                    b.Property<string>("BitRateMode");

                    b.Property<int>("Channels");

                    b.Property<string>("Checksum");

                    b.Property<string>("Codec");

                    b.Property<string>("Compression");

                    b.Property<string>("Notes");

                    b.Property<int>("SampleCount");

                    b.Property<int>("SampleRate");

                    b.Property<Guid>("TrackId");

                    b.HasKey("SampleId");

                    b.HasIndex("TrackId");

                    b.ToTable("Samples");
                });

            modelBuilder.Entity("BeepBong.Models.Track", b =>
                {
                    b.Property<Guid>("TrackId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<Guid>("ProgrammeId");

                    b.Property<string>("Subtitle");

                    b.HasKey("TrackId");

                    b.HasIndex("ProgrammeId");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("BeepBong.Models.Sample", b =>
                {
                    b.HasOne("BeepBong.Models.Track", "Track")
                        .WithMany("Samples")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BeepBong.Models.Track", b =>
                {
                    b.HasOne("BeepBong.Models.Programme", "Programme")
                        .WithMany("Tracks")
                        .HasForeignKey("ProgrammeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
