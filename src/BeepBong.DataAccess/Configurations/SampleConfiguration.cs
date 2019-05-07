using System;
using BeepBong.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeepBong.DataAccess.Configurations
{
    public class SampleConfiguration : IEntityTypeConfiguration<Sample>
    {
        public void Configure(EntityTypeBuilder<Sample> builder)
        {
            builder.Property(e => e.Compression)
                .HasConversion(
                    v => v.ToString(),
                    v => (CompressionEnum)Enum.Parse(typeof(CompressionEnum), v));
            
            builder.Property(e => e.BitRateMode)
                .HasConversion(
                    v => v.ToString(),
                    v => (BitRateModeEnum)Enum.Parse(typeof(BitRateModeEnum), v));

        }
    }
}