using IdealDiscuss.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace IdealDiscuss.Context.EntityConfiguration
{
    public class FlagEntityConfiguration : IEntityTypeConfiguration<Flag>
    {
        public void Configure(EntityTypeBuilder<Flag> builder)
		{
			builder.ToTable("Flags");

			builder.HasKey(f => f.Id);

            builder.Property(f => f.FlagName)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(f => f.FlagName)
             .IsUnique();
		}
    }
}