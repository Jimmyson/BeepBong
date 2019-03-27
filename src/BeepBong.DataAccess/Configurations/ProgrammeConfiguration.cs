using System;
using BeepBong.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeepBong.DataAccess.Configurations
{
	public class ProgrammeConfiguration : IEntityTypeConfiguration<Programme>
	{
		public void Configure(EntityTypeBuilder<Programme> builder)
		{
			builder.Property(p => p.Year)
				.HasMaxLength(4);
		}
	}
}