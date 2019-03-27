using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
				modelBuilder.Entity(entityType.Name).Property<DateTime?>("LastModified");
			}
		}

		public DbSet<Programme> Programmes { get; set; }
		public DbSet<Library> Library { get; set; }
		public DbSet<Track> Tracks { get; set; }
		public DbSet<Sample> Samples { get; set; }
		public DbSet<LibraryProgramme> LibraryProgrammes { get; set; }

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken)) {
			ShadowPropertyUpdate();

			return await base.SaveChangesAsync(cancellationToken);
		}

		public override int SaveChanges() {
			ShadowPropertyUpdate();

			return base.SaveChanges();
		}

		private void ShadowPropertyUpdate() {
			foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)) {
				if (entry.State == EntityState.Added)
					entry.Property("Created").CurrentValue = DateTime.Now;
				else
					entry.Property("LastModified").CurrentValue = DateTime.Now;
			}
		}
	}
}