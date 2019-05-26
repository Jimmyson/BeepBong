using BeepBong.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeepBong.DataAccess.Configurations
{
    public class ProgrammeTrackListConfiguration : IEntityTypeConfiguration<ProgrammeTrackList>
    {
        public void Configure(EntityTypeBuilder<ProgrammeTrackList> builder)
        {
            builder.HasKey(ptl => new { ptl.ProgrammeId, ptl.TrackListId });

            builder.HasOne(ptl => ptl.TrackList)
                .WithMany(tl => tl.ProgrammeTrackLists)
                .HasForeignKey(ptl => ptl.TrackListId);

            builder.HasOne(ptl => ptl.Programme)
                .WithMany(p => p.ProgrammeTrackLists)
                .HasForeignKey(ptl => ptl.ProgrammeId);
        }
    }
}