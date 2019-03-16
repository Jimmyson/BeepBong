using BeepBong.Models;
using Microsoft.EntityFrameworkCore;

namespace BeepBong
{
	public class BeepBongContext : DbContext
	{
		public BeepBongContext(DbContextOptions<BeepBongContext> options) : base(options)
		{
			
		}

		public DbSet<Programme> Programmes { get; set; }
		public DbSet<Track> Tracks { get; set; }
		public DbSet<Sample> Samples { get; set; }
	}
}