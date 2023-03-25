using IdealDiscuss.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdealDiscuss.Context
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Name)
				.IsRequired()
				.HasMaxLength(15)
				.HasColumnName("CategoryName");
		}
    }
}
