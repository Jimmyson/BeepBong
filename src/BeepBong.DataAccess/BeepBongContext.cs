using System;
using BeepBong.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.DataAccess
{
	public class BeepBongContext : DbContext
	{
		public BeepBongContext(DbContextOptions<BeepBongContext> options) : base(options)
		{
			
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Enum Conversion
			modelBuilder.Entity<Sample>()
				.Property(e => e.Compression)
				.HasConversion(
					v => v.ToString(),
					v => (CompressionEnum)Enum.Parse(typeof(CompressionEnum), v));
			
			modelBuilder.Entity<Sample>()
				.Property(e => e.BitRateMode)
				.HasConversion(
					v => v.ToString(),
					v => (BitRateModeEnum)Enum.Parse(typeof(BitRateModeEnum), v));

			// Model Properties
			modelBuilder.Entity<Programme>()
				.Property(p => p.Year)
				.HasMaxLength(4);

			// Library Programme
			modelBuilder.Entity<LibraryProgramme>()
				.HasKey(lp => new { lp.ProgrammeId, lp.LibraryId });

			modelBuilder.Entity<LibraryProgramme>()
				.HasOne(lp => lp.Library)
				.WithMany(l => l.LibraryProgrammes)
				.HasForeignKey(lp => lp.LibraryId);

			modelBuilder.Entity<LibraryProgramme>()
				.HasOne(lp => lp.Programme)
				.WithMany(p => p.LibraryProgrammes)
				.HasForeignKey(lp => lp.ProgrammeId);
		}

		public DbSet<Programme> Programmes { get; set; }
		public DbSet<Library> Library { get; set; }
		public DbSet<Track> Tracks { get; set; }
		public DbSet<Sample> Samples { get; set; }
		public DbSet<LibraryProgramme> LibraryProgrammes { get; set; }
	}
}