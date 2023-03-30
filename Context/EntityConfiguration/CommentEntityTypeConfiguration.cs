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

            builder.Property(c => c.CommentText)
                   .IsRequired()
                   .HasColumnType("text");

            builder.HasOne(c => c.User)
                   .WithMany(u => u.Comments)
                   .HasForeignKey(c => c.UserId)
                   .IsRequired();

            builder.HasOne(c => c.Question)
                   .WithMany(q => q.Comments)
                   .HasForeignKey(c => c.QuestionId)
                   .IsRequired();

            builder.HasMany(c => c.CommentReports)
                   .WithOne(cr => cr.Comment)
                   .IsRequired();
        }
    }
}
