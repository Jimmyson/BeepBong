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
            modelBuilder.ApplyConfiguration(new ProgrammeTrackListConfiguration());
            modelBuilder.ApplyConfiguration(new SampleConfiguration());

            foreach (var entityType in modelBuilder.Model.GetEntityTypes()) {
                modelBuilder.Entity(entityType.Name).Property<DateTime>("Created");
                modelBuilder.Entity(entityType.Name).Property<DateTime?>("LastModified");
            }

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Broadcaster> Broadcasters { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Programme> Programmes { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<TrackList> TrackLists { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Sample> Samples { get; set; }
        public DbSet<ProgrammeTrackList> ProgrammeTrackLists { get; set; }
        public DbSet<Image> Images { get; set; }

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