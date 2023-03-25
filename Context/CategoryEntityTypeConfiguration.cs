using IdealDiscuss.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdealDiscuss.Context
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            /*builder.ToTable("Category");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnName("CategoryName");*/
          /*  builder.HasData(
                    new Category
                    {
                        Id = 6,
                        Name = "Sport"

                    }
                );*/

        }
    }
}
