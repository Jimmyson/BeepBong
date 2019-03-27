using BeepBong.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeepBong.DataAccess.Configurations
{
	public class LibraryProgrammeConfiguration : IEntityTypeConfiguration<LibraryProgramme>
	{
		public void Configure(EntityTypeBuilder<LibraryProgramme> builder)
		{
			builder.HasKey(lp => new { lp.ProgrammeId, lp.LibraryId });

			builder.HasOne(lp => lp.Library)
				.WithMany(l => l.LibraryProgrammes)
				.HasForeignKey(lp => lp.LibraryId);

			builder.HasOne(lp => lp.Programme)
				.WithMany(p => p.LibraryProgrammes)
				.HasForeignKey(lp => lp.ProgrammeId);
		}
	}
}