using System;
using BeepBong.Models;
using Microsoft.EntityFrameworkCore;

namespace BeepBong
{
	public class BeepBongContext : DbContext
	{
		public BeepBongContext(DbContextOptions<BeepBongContext> options) : base(options)
		{
			
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<Sample>()
				.Property(e => e.Compression)
				.HasConversion(
					v => v.ToString(),
					v => (CompressionEnum)Enum.Parse(typeof(CompressionEnum), v));
					
			modelBuilder
				.Entity<Sample>()
				.Property(e => e.BitRateMode)
				.HasConversion(
					v => v.ToString(),
					v => (BitRateModeEnum)Enum.Parse(typeof(BitRateModeEnum), v));
		}

		public DbSet<Programme> Programmes { get; set; }
		public DbSet<Track> Tracks { get; set; }
		public DbSet<Sample> Samples { get; set; }
	}
}