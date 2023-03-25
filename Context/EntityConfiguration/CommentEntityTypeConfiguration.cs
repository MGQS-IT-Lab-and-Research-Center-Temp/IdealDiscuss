using IdealDiscuss.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdealDiscuss.Context.EntityConfiguration
{
    public class CommentEntityTypeConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Question)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(c => c.CommentText)
                .IsRequired()
                .HasColumnType("text");
            builder.Property(c => c.User)
                .IsRequired();
        }
    }
}
