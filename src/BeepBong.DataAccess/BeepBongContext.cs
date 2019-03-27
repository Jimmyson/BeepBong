using System;
using BeepBong.DataAccess.Configurations;
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
			modelBuilder.ApplyConfiguration(new ProgrammeConfiguration());
			modelBuilder.ApplyConfiguration(new LibraryProgrammeConfiguration());
			modelBuilder.ApplyConfiguration(new SampleConfiguration());

			foreach (var entityType in modelBuilder.Model.GetEntityTypes()) {
				modelBuilder.Entity(entityType.Name).Property<DateTime>("Created");
				modelBuilder.Entity(entityType.Name).Property<DateTime>("LastModified");
			}
		}

		public DbSet<Programme> Programmes { get; set; }
		public DbSet<Library> Library { get; set; }
		public DbSet<Track> Tracks { get; set; }
		public DbSet<Sample> Samples { get; set; }
		public DbSet<LibraryProgramme> LibraryProgrammes { get; set; }
	}
}